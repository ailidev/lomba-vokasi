using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SavePlayerPosition : MonoBehaviour
{
    BoxCollider m_Collider;
    Vector3 m_TeleportPosition;

    void Awake() {
        m_Collider = GetComponent<BoxCollider>();
        m_TeleportPosition = transform.position;

        DontDestroyOnLoad(gameObject);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            SavePosition();
            Debug.Log("Position saved");
        }
    }

    public void SavePosition()
    {
        GameManager.Instance.m_GameData.m_PlayerPosition = m_TeleportPosition;
    }
}
