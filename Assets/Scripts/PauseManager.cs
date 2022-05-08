using UnityEngine;

public class PauseManager : MonoBehaviour
{
    MyCursor cursor;

    void Awake()
    {
        cursor = FindObjectOfType<MyCursor>();
    }

    public void PauseTime(bool paused)
    {
        if (paused)
        {
            Player.Instance.canMove = false;
            Time.timeScale = 0;
            cursor.UnlockCursor();
        }
        else
        {
            Player.Instance.canMove = true;
            Player.Instance.m_Crosshair.enabled = true;
            Time.timeScale = 1;
            cursor.LockCursor();
        }
    }
}
