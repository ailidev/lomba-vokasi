using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickupObjectManager : MonoBehaviour
{
    public static PickupObjectManager Instance { get; set; }

    [SerializeField] public TMP_Text m_TotalScoreContainer, m_TotalCorrectContainer, m_TotalWrongContainer;

    public PickupObjectData m_PickupObjectData;

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
    }

    public void StartSearching(bool value)
    {
        m_PickupObjectData.m_IsSearchingObject = value;
    }

    public void SaveTotalScore()
    {
        GameManager.Instance.m_GameData.m_Score += m_PickupObjectData.m_TotalScore;
    }

}
