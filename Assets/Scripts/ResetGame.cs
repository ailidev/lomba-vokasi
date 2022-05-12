using UnityEngine;

public class ResetGame : MonoBehaviour
{
    [SerializeField] public bool m_ResetGame;

    void Awake()
    {
        if (m_ResetGame)
        {
            DestroyGame();
        }
    }

    public void DestroyGame()
    {
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        Destroy(GameObject.FindGameObjectWithTag("Script"));
    }
}
