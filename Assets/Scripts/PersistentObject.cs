using UnityEngine;

public class PersistentObject : MonoBehaviour
{
    public PersistentObject Instance { get; set; }
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
