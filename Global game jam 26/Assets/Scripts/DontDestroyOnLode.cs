using UnityEngine;

public class DontDestroyOnLode : MonoBehaviour
{
    public static DontDestroyOnLode Instance { get; private set; }

    private void Awake()
    {
        // If an instance already exists and it's not this one, destroy the new one
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Set this as the instance and make it persist
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
