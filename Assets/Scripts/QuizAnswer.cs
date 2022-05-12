using UnityEngine;
using UnityEngine.UI;

public class QuizAnswer : MonoBehaviour
{
    [SerializeField] public bool m_IsCorrect;
    QuizManager m_QuizManager;

    void Awake()
    {
        m_QuizManager = FindObjectOfType<QuizManager>();
    }

    public void Answer()
    {
        if (!m_QuizManager.m_IsQuizOver)
        {
            if (m_IsCorrect)
            {
                Debug.Log("Correct");
                m_QuizManager.CorrectAnswer();
                // GameManager.Instance.m_GameData.m_Score += m_QuizManager.m_CorrectPoint;
                m_QuizManager.m_TotalScore += m_QuizManager.m_CorrectPoint;
                m_QuizManager.m_CorrectAnswerCount++;
            }
            else
            {
                Debug.Log("Wrong");
                m_QuizManager.CorrectAnswer();
                m_QuizManager.m_WrongAnswerCount++;
            }
        }
    }
}
