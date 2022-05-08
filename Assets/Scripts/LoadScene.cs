using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public static LoadScene Instance { get; private set; }
    [SerializeField] public Animator m_LoadingScreen;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this);
    }

    private IEnumerator LoadAsynchronously(string name) {
        Time.timeScale = 1;
        AsyncOperation loading = SceneManager.LoadSceneAsync(name);
        loading.allowSceneActivation = false;
        // GameObject.FindGameObjectWithTag("bgm").GetComponent<AudioSource>().volume -= .7f;

        while (!loading.isDone) {
            // m_LoadingScreen.SetActive(true);
            m_LoadingScreen.SetBool("IsLoading", true);

            if (loading.progress >= .9f) {
                loading.allowSceneActivation = true;
                m_LoadingScreen.SetBool("IsLoading", false);
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
