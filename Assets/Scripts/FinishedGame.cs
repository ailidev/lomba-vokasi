using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedGame : MonoBehaviour
{
    public void CurrentGame(string name)
    {
        GameManager.Instance.m_GameData.m_FinishedGame = name;
    }
}
