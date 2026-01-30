using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance { get; private set; }

    [Header("Progression Tracking")]
    public int currentWorld = 1;
    public int currentLevelInWorld = 0; // Started at 0 to handle first shop properly
    public int levelsBeforeShop = 3;
    public int maxWorlds = 3;

    [Header("Scene Names")]
    public string tutorialSceneName = "Tutorial";
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

    // Call this to start the game from a menu button
    public void StartGame()
    {
        // Check if tutorial was ever finished
        if (PlayerPrefs.GetInt("TutorialDone", 0) == 0)
        {
            SceneManager.LoadScene("Tutorial");
        }
        else
        {
            // If tutorial is done, go straight to the first Shop
            currentWorld = 1;
            currentLevelInWorld = 0;
            LoadShop();
        }
    }

    // Call this specifically at the end of the Tutorial level
    public void FinishTutorial()
    {
        PlayerPrefs.SetInt("TutorialDone", 1);
        PlayerPrefs.Save();
        
        // After tutorial, always go to Shop first
        currentWorld = 1;
        currentLevelInWorld = 0;
        LoadShop();
    }

    public void MoveToNextLocation()
    {
        // If we are currently in a level, check if we hit the shop interval
        if (currentLevelInWorld > 0 && currentLevelInWorld % levelsBeforeShop == 0)
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
        
        // Your logic for 2 levels per world
        if (currentLevelInWorld > 3) 
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

    public void ExitShop()
    {
        LoadNextLevel();
    }

    // Optional: Call this if you want to force the tutorial to play again (for testing)
    public void ResetTutorialStatus()
    {
        PlayerPrefs.SetInt("TutorialDone", 0);
    }
}