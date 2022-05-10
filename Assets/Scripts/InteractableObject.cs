using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(BoxCollider))]
public class InteractableObject : MonoBehaviour {
    [Header("Default")]
    [SerializeField, TextArea(2, 4), Tooltip("Show message when interacting with an object.")] public string m_InteractMessage;
    [SerializeField, TextArea(2, 4)] public string m_LoadScene;
    [SerializeField, Tooltip("Play a sound when interacting with an object.")] protected AudioClip m_NormalSound, m_LockedSound, m_UnlockedSound;
    [SerializeField] public int m_SpawnPoint;

    [Header("Locked Object")]
    [SerializeField, Tooltip("Is this object need a key to unlock?")] protected bool m_NeedKey;
    [SerializeField, Tooltip("Is this object has been unlocked?")] public bool m_IsUnlocked;

    [Header("Key")]
    [SerializeField, Tooltip("What object to unlock with this key?")] protected InteractableObject[] m_LockedObject;

    protected Animator m_Animator;
    protected AudioSource m_Audio;

    protected void Start() {
        m_Animator = GetComponent<Animator>();
        m_Audio = GetComponent<AudioSource>();
    }

    public void OpenObject() {
        IEnumerator DelayAnimation(string param, bool value, float time) {
            yield return new WaitForSeconds(time);
            m_Animator.SetBool(param, value);
        }

        if (m_NeedKey) {
            if (m_LockedSound != null) {
                m_Audio.PlayOneShot(m_LockedSound);
            }

            m_Animator.SetBool("IsLocked", true);
            StartCoroutine(DelayAnimation("IsLocked", false, .1f));
        } else {
            // Directly open the door
            if (m_NormalSound != null) {
                m_Audio.PlayOneShot(m_NormalSound);
            }

            if (!m_Animator.GetBool("IsOpen")) {
                m_Animator.SetBool("IsOpen", true);
            } else {
                m_Animator.SetBool("IsOpen", false);
            }
        }

        if (m_IsUnlocked) {
            if (m_UnlockedSound != null) {
                m_Audio.PlayOneShot(m_UnlockedSound);
            }

            StartCoroutine(DelayAnimation("IsUnlocked", true, 3));
            // m_Animator.SetBool();
        }
    }

    public void UnlockObject() {
        // SPECIAL FOR "KEY" GAME OBJECT
        if (m_NormalSound != null) {
            m_Audio.PlayOneShot(m_NormalSound);
        }

        for (int i = 0; i < m_LockedObject.Length; i++) {
            if (m_LockedObject[i] != null) {
                m_LockedObject[i].m_NeedKey = false;
                m_LockedObject[i].m_IsUnlocked = true;
            }
        }
    }
}
