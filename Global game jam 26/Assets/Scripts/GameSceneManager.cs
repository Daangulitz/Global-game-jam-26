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

    // Call this from the Main Menu Button
    public void StartGame()
    {
        if (PlayerPrefs.GetInt("TutorialDone", 0) == 0)
        {
            SceneManager.LoadScene(tutorialSceneName);
        }
        else
        {
            currentWorld = 1;
            currentLevelInWorld = 1;
            SceneManager.LoadScene("World1_Level1");
        }
    }

    // --- CALLED BY ELEVATOR TRIGGER ---
    public void StartElevatorSequence(float dummy) 
    {
        string currentScene = SceneManager.GetActiveScene().name;

        // 1. If we are in the Tutorial, go to the SHOP
        if (currentScene == tutorialSceneName)
        {
            PlayerPrefs.SetInt("TutorialDone", 1);
            PlayerPrefs.Save(); // Force the save immediately
            
            currentWorld = 1;
            currentLevelInWorld = 0; // Set to 0 so LoadNextLevel makes it 1
            SceneManager.LoadScene(shopSceneName);
        }
        // 2. If we are in the Shop, go to WORLD 1 LEVEL 1
        else if (currentScene == shopSceneName)
        {
            currentWorld = 1;
            currentLevelInWorld = 1;
            SceneManager.LoadScene("World1_Level1");
        }
        // 3. Otherwise, use normal level progression
        else
        {
            MoveToNextLocation();
        }
    }

    public void MoveToNextLocation()
    {
        // Go to shop every X levels
        if (currentLevelInWorld % levelsBeforeShop == 0)
        {
            SceneManager.LoadScene(shopSceneName);
        }
        else
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        currentLevelInWorld++;
        
        if (currentLevelInWorld > 6) 
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
            SceneManager.LoadScene("World" + currentWorld + "_Level" + currentLevelInWorld);
        }
    }
}