using UnityEngine;

public class ActivateDeactivateObject : MonoBehaviour {
    [SerializeField] protected GameObject[] m_ActivateObject;
    [SerializeField] protected GameObject[] m_DeactivateObject;

    public void ActivateObject() {
        for (int i = 0; i < m_ActivateObject.Length; i++) {
            if (m_ActivateObject[i] != null) {
                m_ActivateObject[i].SetActive(true);
            }
        }
    }

    public void DeactivateObject() {
        for (int i = 0; i < m_DeactivateObject.Length; i++) {
            if (m_DeactivateObject[i] != null) {
                m_DeactivateObject[i].SetActive(false);
            }
        }
    }
}
