using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    // [SerializeField] bool m_StartQuiz;
    [SerializeField] public List<QuestionAndAnswer> m_QuestionAndAnswer;
    [SerializeField] public Button[] m_Options;
    [SerializeField] public TextMeshProUGUI m_QuestionContainer, m_QuizProgressContainer, m_TotalScoreContainer, m_CorrectAnswerContainer, m_WrongAnswerContainer;
    [SerializeField] public int m_CurrentQuestionIndex, m_QuizProgress, m_CorrectPoint, m_TotalScore, m_CorrectAnswerCount, m_WrongAnswerCount;
    [SerializeField] public Animator m_Animator;
    public bool m_IsQuizOver;

    void Start()
    {
        // Start quiz
        GenerateQuestion();
    }

    void GenerateQuestion()
    {
        // Set quiz progress
        m_QuizProgress++;
        m_QuizProgressContainer.text = m_QuizProgress + "/" + m_QuestionAndAnswer.Capacity;

        // Randomize question
        m_CurrentQuestionIndex = Random.Range(0, m_QuestionAndAnswer.Count);
        m_QuestionContainer.text = m_QuestionAndAnswer[m_CurrentQuestionIndex].m_Question;
        SetAnswer();
    }

    void SetAnswer()
    {
        for (int i = 0; i < m_Options.Length; i++)
        {
            m_Options[i].GetComponent<QuizAnswer>().m_IsCorrect = false;
            m_Options[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = m_QuestionAndAnswer[m_CurrentQuestionIndex].m_Answer[i];

            if (m_QuestionAndAnswer[m_CurrentQuestionIndex].m_CorrectAnswer == i+1)
            {
                m_Options[i].GetComponent<QuizAnswer>().m_IsCorrect = true;
            }
        }
    }

    public void CorrectAnswer()
    {
        m_QuestionAndAnswer.RemoveAt(m_CurrentQuestionIndex);

        if (m_QuestionAndAnswer.Count > 0)
        {
            StartCoroutine(LoadQuestion(1f));
        }
        else
        {
            m_IsQuizOver = true;
            for (int i = 0; i < m_Options.Length; i++)
            {
                m_Options[i].interactable = false;
            }

            // Show result panel
            m_Animator.SetBool("IsQuizOver", true);
            StartCoroutine(QuizResult());
            Debug.Log("Quiz Over");
        }
    }

    IEnumerator LoadQuestion(float delay)
    {
        Debug.Log("Loading...");
        for (int i = 0; i < m_Options.Length; i++)
        {
            m_Options[i].interactable = false;
        }
        yield return new WaitForSeconds(delay);
        GenerateQuestion();
        for (int i = 0; i < m_Options.Length; i++)
        {
            m_Options[i].interactable = true;
        }
    }

    IEnumerator QuizResult()
    {
        yield return new WaitForSeconds(m_Animator.GetCurrentAnimatorStateInfo(0).length);
        m_TotalScoreContainer.text += StartCoroutine(CountIt(.2f, 0, m_TotalScore)).ToString();
        m_CorrectAnswerContainer.text = m_CorrectAnswerCount.ToString();
        m_WrongAnswerContainer.text = m_WrongAnswerCount.ToString();
    }

    IEnumerator CountIt(float delay, int MinValue, int MaxValue)
    {
        if (MinValue <= MaxValue)
        {
            MinValue++;
            yield return new WaitForSeconds(delay);
        }
    }
}
