using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // Static instance of the singleton. Statics can be accessed anywhere.
    public static T Instance { get; private set; }

    // Ensure only one instance exists and persists across scenes.
    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}