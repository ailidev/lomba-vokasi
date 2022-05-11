using UnityEngine;
using TMPro;

public class LoginSimulation : MonoBehaviour
{
    [SerializeField] public TMP_InputField m_UsernameField;
    [SerializeField] public TMP_InputField m_PasswordField;

    public void Login() {
        GameManager.Instance.m_GameData.m_PlayerName = m_UsernameField.text;
    }
}
