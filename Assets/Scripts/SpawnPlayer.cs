using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public static SpawnPlayer Instance { get; set; }
    public SpawnPoint m_SpawnData;
    [SerializeField] public Transform[] m_SpawnPoint;

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

        m_SpawnData.m_Point = new Vector3[m_SpawnPoint.Length];

        for (int i = 0; i < m_SpawnPoint.Length; i++)
        {
            if (m_SpawnPoint[i] != null)
            {
                m_SpawnData.m_Point[i] = m_SpawnPoint[i].position;
            }
        }
    }
}
