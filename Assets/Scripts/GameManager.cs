using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] public GameData m_GameData;
    [SerializeField] public GameObject[] m_PersistentObjects;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }

        KeepObject();
    }

    public void LoadPosition()
    {
        Player.Instance.transform.position = m_GameData.m_PlayerPosition;
    }

    void KeepObject()
    {
        foreach (GameObject obj in m_PersistentObjects)
        {
            if (obj != null)
            {
                DontDestroyOnLoad(obj);
            }
            else
            {
                Destroy(obj);
            }
        }
    }
}
