using UnityEngine;
using TMPro;

public class LoginSimulation : MonoBehaviour
{
    [SerializeField] public TMP_InputField m_UsernameField;
    [SerializeField] public TMP_InputField m_PasswordField;
    LoadScene m_LoadScene;

    void Awake()
    {
        m_LoadScene = FindObjectOfType<LoadScene>();
    }

    public void Login(string sceneName) {
        if (m_UsernameField.text.Length > 0)
        {
            GameManager.Instance.m_GameData.m_PlayerName = m_UsernameField.text;
            m_LoadScene.LoadSceneName(sceneName);
        }
    }
}
