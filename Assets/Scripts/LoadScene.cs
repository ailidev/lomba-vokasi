using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public static LoadScene Instance { get; set; }
    [SerializeField] public Animator m_LoadingScreen;
    public float m_LoadingProgress;

    private IEnumerator LoadAsynchronously(string name) {
        Time.timeScale = 1;
        AsyncOperation loading = SceneManager.LoadSceneAsync(name);
        loading.allowSceneActivation = false;
        m_LoadingProgress = loading.progress;

        while (!loading.isDone) {
            // m_LoadingScreen.SetActive(true);
            if (m_LoadingScreen != null)
            {
                m_LoadingScreen.SetBool("IsLoading", true);
            }

            if (loading.progress >= .9f) {
                loading.allowSceneActivation = true;
                if (m_LoadingScreen != null)
                {
                    m_LoadingScreen.SetBool("IsLoading", false);
                }
            }

            yield return new WaitForSeconds(1f);
        }
    }

    public void LoadSceneName(string name) {
        StartCoroutine(LoadAsynchronously(name));
    }

    public void RestartScene() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
