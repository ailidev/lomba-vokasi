using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public static LoadScene Instance { get; set; }

    [SerializeField] public Animator m_LoadingScreen;
    [SerializeField] public float m_LoadingProgress;
    
    Slider m_Slider;

    // void Awake()
    // {
    //     if (Instance == null)
    //     {
    //         Instance = this;
    //         DontDestroyOnLoad(this);
    //     }
    //     else
    //     {
    //         Destroy(gameObject);
    //     }
    // }

    IEnumerator LoadAsynchronously(string name) {
        
        Time.timeScale = 1;
        AsyncOperation loading = SceneManager.LoadSceneAsync(name);
        loading.allowSceneActivation = false;

        while (!loading.isDone) {
            if (m_LoadingScreen != null)
            {
                m_LoadingScreen.SetBool("IsLoading", true);
                m_LoadingProgress = loading.progress;

                m_Slider = GetComponentInChildren<Slider>();

                if (m_Slider != null)
                {
                    m_Slider.value = m_LoadingProgress;
                }
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
