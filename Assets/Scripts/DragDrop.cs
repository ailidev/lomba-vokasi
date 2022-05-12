using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour
{
// public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
// {
    // RectTransform m_RectTransform;

    // void Awake()
    // {
    //     m_RectTransform = GetComponent<RectTransform>();
    // }

    // public void OnBeginDrag(PointerEventData eventData)
    // {
    //     m_RectTransform.anchoredPosition += eventData.delta;
    // }

    // public void OnDrag(PointerEventData eventData)
    // {
    // }

    // public void OnEndDrag(PointerEventData eventData)
    // {
    // }

    // public void OnPointerDown(PointerEventData eventData)
    // {
    // }


    /* Camera projection must Orthographic */
    // matchObjects = gameObject.transform.childCount;

    [SerializeField] protected SpriteRenderer[] m_Placeholder;
    [SerializeField, Range(.1f, 10f)] protected float m_SnapRadius = .5f;
    [SerializeField] protected Transform m_Items;
    protected Vector3 m_MousePosition, m_ResetPosition;
    protected bool m_IsMoving, m_IsMatch;
    protected float m_StartPositionX, m_StartPositionY;

    void Start()
    {
        m_ResetPosition = gameObject.transform.localPosition;
    }

    void Update()
    {
        if (!m_IsMatch)
        {
            if (m_IsMoving)
            {
                MousePoint();

                gameObject.transform.localPosition = new Vector3(m_MousePosition.x - m_StartPositionX, m_MousePosition.y - m_StartPositionY, gameObject.transform.localPosition.z);
            }
        }
    }

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MousePoint();

            m_StartPositionX = m_MousePosition.x - transform.localPosition.x;
            m_StartPositionY = m_MousePosition.y - transform.localPosition.y;

            m_IsMoving = true;
        }
    }

    void OnMouseUp()
    {
        m_IsMoving = false;

        for (int i = 0; i < m_Placeholder.Length; i++)
        {
            if (m_Placeholder[i] != null)
            {
                if (Mathf.Abs(gameObject.transform.localPosition.x - m_Placeholder[i].transform.localPosition.x) <= m_SnapRadius && Mathf.Abs(gameObject.transform.localPosition.y - m_Placeholder[i].transform.localPosition.y) <= m_SnapRadius)
                {
                    gameObject.transform.localPosition = new Vector3(m_Placeholder[i].transform.position.x, m_Placeholder[i].transform.position.y, m_Placeholder[i].transform.position.z);

                    m_IsMatch = true;
                }
                else
                {
                    gameObject.transform.localPosition = new Vector3(m_ResetPosition.x, m_ResetPosition.y, m_ResetPosition.z);
                }
            }
        }
        // foreach (SpriteRenderer placeholder in m_Placeholder)
        // {
            
        // }
    }

    void MousePoint()
    {
        m_MousePosition = Input.mousePosition;
        m_MousePosition = Camera.main.ScreenToWorldPoint(m_MousePosition);
    }
}
