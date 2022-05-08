using UnityEngine;

public class InputListener : MonoBehaviour
{
    [SerializeField] public GameObject m_PauseMenu;

    MyCursor cursor;
    bool OpenSomething;

    protected void Start() {
        cursor = FindObjectOfType<MyCursor>();
    }

    protected void Update() {
        // Show pause menu
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (m_PauseMenu != null) {
                m_PauseMenu.SetActive(true);
                cursor.UnlockCursor();
            }
        }
    }
}
