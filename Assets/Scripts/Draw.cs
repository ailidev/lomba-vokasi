using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    [SerializeField] protected Camera m_Camera;
    [SerializeField] protected GameObject m_Brush;
    [SerializeField] protected Transform m_Brushes;

    LineRenderer m_CurrentLineRenderer;
    Vector2 m_MousePos ,m_LastPos;

    // void Update()
    // {
    //     Drawing();
    // }

    void OnMouseOver()
    {
        Drawing();
    }

    void Drawing()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CreateBrush();
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            MousePosition();

            if (m_MousePos != m_LastPos)
            {
                AddPoint(m_MousePos);
                m_LastPos = m_MousePos;
            }
        }
        else
        {
            m_CurrentLineRenderer = null;
        }
    }

    void CreateBrush()
    {
        if (m_Brush != null)
        {
            if (m_Brushes != null)
            {
                GameObject brushInstance = Instantiate(m_Brush);
                m_CurrentLineRenderer = brushInstance.GetComponent<LineRenderer>();

                MousePosition();

                m_CurrentLineRenderer.SetPosition(0, m_MousePos);
                m_CurrentLineRenderer.SetPosition(1, m_MousePos);

                brushInstance.transform.SetParent(m_Brushes);
            }
        }
    }

    void AddPoint(Vector2 pointPos)
    {
        if (m_CurrentLineRenderer != null)
        {
            m_CurrentLineRenderer.positionCount++;
            int positionIndex = m_CurrentLineRenderer.positionCount - 1;
            m_CurrentLineRenderer.SetPosition(positionIndex, pointPos);
        }
    }

    void MousePosition()
    {
        if (m_Camera != null)
        {
            m_MousePos = m_Camera.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    public void ResetDraw(bool deleteAll)
    {
        // Delete lines one by one
        if (!deleteAll)
        {
            int brushCount = m_Brushes.transform.childCount;

            if (brushCount > 0)
            {
                Destroy(m_Brushes.transform.GetChild(brushCount - 1).gameObject);
            }
        }
        else{
            // Delete all lines
            foreach (Transform brush in m_Brushes)
            {
                Destroy(brush.gameObject);
            }
        }
    }
}
