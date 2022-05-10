/*
    Handles Player behaviour.
*/

using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour {
    public static Player Instance {get; set;}

    [Header("Movements")]
    [SerializeField] float m_MoveSpeed = 3f;
    [SerializeField] float m_Gravity = 30f;
    // [SerializeField] AudioSource m_Foot;
    // [SerializeField] AudioClip[] m_StepSounds;
    public bool m_CanMove = true;
    Animator m_Animator;
    CharacterController m_CharacterController;
    AudioSource m_Audio;
    Vector3 m_MoveDirection;
    Vector2 m_CurrentInput;

    [Header("Look")]
    [SerializeField] public Camera m_Camera;
    [SerializeField, Range(1, 10)] float m_LookSpeedX = 2f;
    [SerializeField, Range(1, 10)] float m_LookSpeedY = 2f;
    [SerializeField, Range(1, 180)] float m_UpperLookLimit = 80f;
    [SerializeField, Range(1, 180)] float m_LowerLookLimit = 80f;

    float m_RotationX, m_RotationY;

    // [Header("Head bob")]
    // [SerializeField] float bobbingSpeed = 0.18f;
    // [SerializeField] float bobbingAmount = 0.2f;
    // [SerializeField] float midpoint = 2.0f;

    //  float timer = 0.0f;

    // [Header("Flashlight")]
    // [SerializeField] GameObject m_Flashlight;
    // [SerializeField] AudioClip m_FlashlightOn, m_FlashlightOff;

    //  AudioSource m_FlashlightAudio;
    //  Light m_FlashlightLight;

    [Header("Raycast")]
    [SerializeField] float m_RayLength = 5;
    [SerializeField] LayerMask m_LayerMaskInteract;
    [SerializeField] public string m_ExcludeLayerName;
    [SerializeField] public string[] m_InteractableTag;
    [SerializeField] public KeyCode[] m_InteractKey;
    [SerializeField] public Image m_Crosshair;
    [SerializeField] public Color m_CrosshairNormalColor;
    [SerializeField] public Color m_CrosshairInteractColor;

    public RaycastHit m_RayHit;
    bool m_IsCrosshairActive, m_DoOnce;

    [Header("Tooltip")]
    // [SerializeField] Animator m_InteractPopupKey;
    [SerializeField] public TextMeshProUGUI m_InteractText;

    MyCursor m_Cursor;
    LoadScene m_LoadScene;

    void Awake() {
        m_Animator = GetComponent<Animator>();
        m_CharacterController = GetComponent<CharacterController>();
        m_Audio = GetComponent<AudioSource>();
        // m_FlashlightAudio = m_Flashlight.GetComponent<AudioSource>();
        // m_FlashlightLight = m_Flashlight.GetComponent<Light>();
        m_Cursor = FindObjectOfType<MyCursor>();
        m_LoadScene = FindObjectOfType<LoadScene>();

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start() {
        m_Cursor.LockCursor();

        transform.rotation = Quaternion.identity;

        // IMPORTANT! Initialize Game Object tag here to make interact function works perfectly
        m_InteractableTag = new string[5];
        m_InteractableTag[0] = "Door";
        m_InteractableTag[1] = "Key";
        m_InteractableTag[2] = "Book";
        m_InteractableTag[3] = "Drawer";
        m_InteractableTag[4] = "PickedObject";
    }

    void HandleMovementInput() {
        // Character movement using WASD or Arrow
        m_CurrentInput = new Vector2(m_MoveSpeed * Input.GetAxis("Vertical"), m_MoveSpeed * Input.GetAxis("Horizontal"));
        float m_MoveDirectionY = m_MoveDirection.y;

        m_MoveDirection = (transform.TransformDirection(m_Camera.transform.forward) * m_CurrentInput.x) + (transform.TransformDirection(m_Camera.transform.right) * m_CurrentInput.y);

        m_MoveDirection.y = m_MoveDirectionY;

        // if (m_CurrentInput.x != 0 || m_CurrentInput.y != 0) {
        //     m_Animator.SetBool("IsWalking", true);
        // } else {
        //     m_Animator.SetBool("IsWalking", false);
        // }

        // Head bob
        // float waveslice = 0f;
        // if (Mathf.Abs(m_CurrentInput.x) <= 0f && Mathf.Abs(m_CurrentInput.y) <= 0f) {
        //     timer = 0f;
        // } else {
        //     waveslice = Mathf.Sin(timer);
        //     timer = timer + bobbingSpeed;

        //     if (timer > Mathf.PI * 2) {
        //         timer = timer - (Mathf.PI * 2);
        //     }
        // }

        // Vector3 m_Position = transform.localPosition;
        // if (waveslice != 0) {
        //   float translateChange = waveslice * bobbingAmount;
        //   float totalAxes = Mathf.Abs(m_CurrentInput.x) + Mathf.Abs(m_CurrentInput.y);
        //   totalAxes = Mathf.Clamp (totalAxes, 0.0f, 1.0f);
        //   translateChange = totalAxes * translateChange;
        //   m_Position.y = midpoint + translateChange;
        // } else {
        //   m_Position.y = midpoint;
        // }

        // transform.localPosition = m_Position;
    }

    void HandleMouseLook() {
        // Looking using mouse
        m_RotationX -= Input.GetAxis("Mouse Y") * m_LookSpeedY;
        m_RotationY += Input.GetAxis("Mouse X") * m_LookSpeedX;
        m_RotationX = Mathf.Clamp(m_RotationX, -m_UpperLookLimit, m_LowerLookLimit);
        // transform.localEulerAngles = new Vector3(-m_RotationX, m_RotationY, 0);
        // transform.rotation = Quaternion.Euler(m_RotationX, m_RotationY, 0);
        m_Camera.transform.localRotation = Quaternion.Euler(m_RotationX, m_RotationY, 0);
    }

    void FinalMovements() {
        // Firing up the movements
        if (!m_CharacterController.isGrounded) {
            m_MoveDirection.y -= m_Gravity * Time.deltaTime;
        }

        m_CharacterController.Move(m_MoveDirection * Time.deltaTime);
    }

    //  void Footstep() {
    //     int clip = Random.Range(0, m_StepSounds.Length);
    //     m_Foot.PlayOneShot(m_StepSounds[clip]);
    // }

    //  void Flashlight() {
    //     // Right click to activate/deactivate flashlight.
    //     if (Input.GetMouseButtonDown(1)) {
    //         if (m_FlashlightLight.enabled) {
    //             if (m_FlashlightOff != null) {
    //                 m_FlashlightAudio.PlayOneShot(m_FlashlightOff);
    //             }

    //             m_FlashlightLight.enabled = false;
    //         } else {
    //             if (m_FlashlightOn != null) {
    //                 m_FlashlightAudio.PlayOneShot(m_FlashlightOn);
    //             }

    //             m_FlashlightLight.enabled = true;
    //         }

    //         m_Flashlight.transform.rotation = transform.rotation;
    //     }
    // }

    void ChangeCrosshair(bool on) {
        if (on && !m_DoOnce) {
            m_Crosshair.color = m_CrosshairInteractColor;
        } else {
            m_Crosshair.color = m_CrosshairNormalColor;
            m_IsCrosshairActive = false;
        }
    }

    //  void ShowPopupKey(bool value) {
    //     m_InteractPopupKey.SetBool("IsActive", value);
    // }

    void RaycastLogic() {
        Vector3 forward = transform.TransformDirection(m_Camera.transform.forward);
        int mask = 1 << LayerMask.NameToLayer(m_ExcludeLayerName) | m_LayerMaskInteract.value;
        Ray ray = new Ray(m_Camera.transform.position, forward);

        Debug.DrawRay(ray.origin, ray.direction * m_RayLength, Color.red);

        if (Physics.Raycast(ray, out m_RayHit, m_RayLength, mask)) {
            #region Door
            if (m_RayHit.collider.CompareTag(m_InteractableTag[0])) {
                InteractableObject door = m_RayHit.collider.gameObject.GetComponent<InteractableObject>();

                if (!m_DoOnce) {
                    ChangeCrosshair(true);
                }

                m_InteractText.enabled = true;
                m_InteractText.text = door.m_InteractMessage;
                m_IsCrosshairActive = true;
                m_DoOnce = true;

                if (Input.GetKeyDown(m_InteractKey[0]) || Input.GetKeyDown(m_InteractKey[1])) {
                    // door.OpenObject();
                    if (door.m_LoadScene != null)
                    {
                        m_LoadScene.LoadSceneName(door.m_LoadScene);
                    }
                    if (!door.m_IsUnlocked) {
                        // ShowPopupKey(true);
                    }
                }
            }
            #endregion

            #region Lock Key
            if (m_RayHit.collider.CompareTag(m_InteractableTag[1])) {
                GameObject lockKey = m_RayHit.collider.gameObject;

                if (!m_DoOnce) {
                    ChangeCrosshair(true);
                    m_InteractText.enabled = true;
                    m_InteractText.text = lockKey.GetComponent<InteractableObject>().m_InteractMessage;
                }

                m_IsCrosshairActive = true;
                m_DoOnce = true;

                if (Input.GetKeyDown(m_InteractKey[0]) || Input.GetKeyDown(m_InteractKey[1])) {
                    lockKey.GetComponent<InteractableObject>().UnlockObject();
                    lockKey.GetComponent<MeshRenderer>().enabled = false;
                    // RELATIVE
                    lockKey.GetComponent<SphereCollider>().enabled = false;
                }
            }
            #endregion

            #region Book
            if (m_RayHit.collider.CompareTag(m_InteractableTag[2])) {
                ActivateDeactivateObject activateDeactivateObj = m_RayHit.collider.gameObject.GetComponent<ActivateDeactivateObject>();
                InteractableObject book = m_RayHit.collider.gameObject.GetComponent<InteractableObject>();

                if (!m_DoOnce) {
                    ChangeCrosshair(true);
                    m_InteractText.enabled = true;
                    m_InteractText.text = book.m_InteractMessage;
                }

                m_IsCrosshairActive = true;
                m_DoOnce = true;

                if (Input.GetKeyDown(m_InteractKey[0]) || Input.GetKeyDown(m_InteractKey[1])) {
                    activateDeactivateObj.ActivateObject();
                    activateDeactivateObj.DeactivateObject();
                    m_Cursor.UnlockCursor();
                    m_Crosshair.enabled = false;
                    m_CanMove = false;
                }
            }
            #endregion

            #region Table Drawer
            if (m_RayHit.collider.CompareTag(m_InteractableTag[3])) {
                InteractableObject drawer = m_RayHit.collider.gameObject.GetComponent<InteractableObject>();

                if (!m_DoOnce) {
                    ChangeCrosshair(true);
                }

                m_IsCrosshairActive = true;
                m_DoOnce = true;

                if (Input.GetKeyDown(m_InteractKey[0]) || Input.GetKeyDown(m_InteractKey[1])) {
                    drawer.OpenObject();

                    if (!drawer.m_IsUnlocked) {
                        // ShowPopupKey(true);
                        m_InteractText.enabled = true;
                        m_InteractText.text = drawer.m_InteractMessage;
                    }
                }
            }
            #endregion

            #region Pick Object System
            if (m_RayHit.collider.CompareTag(m_InteractableTag[4])) {
                PickedObject obj = m_RayHit.collider.gameObject.GetComponent<PickedObject>();

                if (!m_DoOnce) {
                    ChangeCrosshair(true);
                    m_InteractText.enabled = true;
                    m_InteractText.text = obj.m_InteractMessage;
                }

                m_IsCrosshairActive = true;
                m_DoOnce = true;

                if (Input.GetKeyDown(m_InteractKey[0]) || Input.GetKeyDown(m_InteractKey[1])) {
                    obj.PickObject();
                }
            }
            #endregion
        } else if (m_IsCrosshairActive) {
            ChangeCrosshair(false);
            m_DoOnce = false;
            // ShowPopupKey(false);
            m_InteractText.enabled = false;
        }
    }

    void Update() {
        if (m_CanMove) {
            HandleMovementInput();
            HandleMouseLook();
            FinalMovements();
            // Flashlight();
            RaycastLogic();
        }
    }
}
