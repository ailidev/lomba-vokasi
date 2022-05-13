using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGame : MonoBehaviour
{
    [SerializeField] public bool m_ResetGame;
    GameObject[] m_Player, m_Script;

    void Awake()
    {

        if (GameManager.Instance.m_GameData.m_FirstTimePlaying)
        {
            m_ResetGame = true;
        }
        else
        {
            m_ResetGame = false;
        }

        m_Player = GameObject.FindGameObjectsWithTag("Player");
        m_Script = GameObject.FindGameObjectsWithTag("Script");

        if (m_ResetGame)
        {
            DestroyGame();
        }
    }

    public void DestroyGame()
    {
        foreach(GameObject obj in m_Player)
        {
            Destroy(obj);
        }

        foreach(GameObject obj in m_Script)
        {
            Destroy(obj);
        }
    }

    public void FirstGame()
    {
        GameManager.Instance.m_GameData.m_FirstTimePlaying = true;
    }
}
