using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance { get; private set; }

    [Header("Progression Tracking")]
    public int currentWorld = 1;
    public int currentLevelInWorld = 1;
    public int levelsBeforeShop = 3;
    public int maxWorlds = 3;

    [Header("Scene Names")]
    public string shopSceneName = "ShopScene";
    public string mainMenuSceneName = "MainMenu";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Call this whenever the player finishes a level
    public void MoveToNextLocation()
    {
        // Check if it's time for a shop
        if (currentLevelInWorld % levelsBeforeShop == 0)
        {
            LoadShop();
        }
        else
        {
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        currentLevelInWorld++;
        
        if (currentLevelInWorld > 9) 
        {
            currentWorld++;
            currentLevelInWorld = 1;
        }

        if (currentWorld > maxWorlds)
        {
            SceneManager.LoadScene(mainMenuSceneName);
        }
        else
        {
            string nextLevelName = "World" + currentWorld + "_Level" + currentLevelInWorld;
            SceneManager.LoadScene(nextLevelName);
        }
    }

    private void LoadShop()
    {
        SceneManager.LoadScene(shopSceneName);
    }

    // Call this from a "Continue" button inside the Shop scene
    public void ExitShop()
    {
        LoadNextLevel();
    }
}