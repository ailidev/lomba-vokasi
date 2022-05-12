using UnityEngine;
using TMPro;

public class GameResult : MonoBehaviour
{
    [SerializeField] public TMP_Text m_FinishedGame, m_PlayerName, m_Predikat, m_TotalScore;

    void Awake()
    {
        if (m_PlayerName != null)
        {
            m_PlayerName.text = GameManager.Instance.m_GameData.m_PlayerName;
        }

        if (m_FinishedGame != null)
        {
            m_FinishedGame.text = GameManager.Instance.m_GameData.m_FinishedGame;
        }

        if (m_TotalScore != null)
        {
            m_TotalScore.text = GameManager.Instance.m_GameData.m_Score.ToString();
        }

        if (m_Predikat != null)
        {
            if (GameManager.Instance.m_GameData.m_Score >= 600)
            {
                m_Predikat.text = "Kompeten";
            }
            else
            {
                m_Predikat.text = "Tidak Kompeten";
            }
        }
    }
}
