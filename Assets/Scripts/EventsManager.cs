using UnityEngine;

[RequireComponent(typeof(ActivateDeactivateObject))]
public class EventsManager : MonoBehaviour {
    [SerializeField] protected AudioSource m_Player;
    [SerializeField] protected AudioClip[] m_PlaySound;
    protected ActivateDeactivateObject m_Event;

    protected void Start() {
    //     player = FindObjectOfType<Player>();
        m_Event = GetComponent<ActivateDeactivateObject>();
    }

    protected void OnTriggerEnter(Collider collider) {
        if (collider.CompareTag("Player")) {
            for (int i = 0; i < m_PlaySound.Length; i++) {
                if (m_PlaySound[i] != null) {
                    m_Player.PlayOneShot(m_PlaySound[i]);
                }
            }

            m_Event.ActivateObject();
            m_Event.DeactivateObject();
        }
    }
}
