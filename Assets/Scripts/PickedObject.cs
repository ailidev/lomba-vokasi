using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(AudioSource))]
public class PickedObject : MonoBehaviour
{
    [SerializeField, Tooltip("Ini objek berhubungan dengan tema?")] public bool m_ObjectValue;
    [SerializeField] public int m_CorrectPoint = 100;
    [SerializeField] public int m_WrongPoint = 125;
    [SerializeField, TextArea(3, 3)] public string m_InteractMessage;
    [SerializeField] public AudioClip m_PickedSound;
    
    AudioSource m_AudioSource;

    void Awake() {
        m_AudioSource = GetComponent<AudioSource>();
    }

    public void PickObject()
    {
        StartCoroutine(Pick(.3f));
    }

    IEnumerator Pick(float delay)
    {
        if (m_PickedSound != null)
        {
            m_AudioSource.PlayOneShot(m_PickedSound);
        }

        if (PickupObjectManager.Instance.m_PickupObjectData.m_IsSearchingObject)
        {
            if (m_ObjectValue)
            {
                PickupObjectManager.Instance.m_PickupObjectData.m_TotalScore += m_CorrectPoint;
                PickupObjectManager.Instance.m_PickupObjectData.m_TotalCorrectObject++;
                PickupObjectManager.Instance.m_TotalCorrectContainer.text = PickupObjectManager.Instance.m_PickupObjectData.m_TotalCorrectObject.ToString();
            }
            else
            {
                PickupObjectManager.Instance.m_PickupObjectData.m_TotalScore -= m_WrongPoint;
                PickupObjectManager.Instance.m_PickupObjectData.m_TotalWrongObject++;
                PickupObjectManager.Instance.m_TotalWrongContainer.text = PickupObjectManager.Instance.m_PickupObjectData.m_TotalWrongObject.ToString();
            }

            if (PickupObjectManager.Instance.m_PickupObjectData.m_TotalScore < 0)
            {
                PickupObjectManager.Instance.m_TotalScoreContainer.text = "0";
                PickupObjectManager.Instance.m_PickupObjectData.m_TotalScore = 0;
            }
            else
            {
                PickupObjectManager.Instance.m_TotalScoreContainer.text = PickupObjectManager.Instance.m_PickupObjectData.m_TotalScore.ToString();
            }

            yield return new WaitForSeconds(delay);

            Destroy(gameObject);
        }
    }
}
