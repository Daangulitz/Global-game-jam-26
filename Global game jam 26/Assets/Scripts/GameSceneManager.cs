using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance { get; private set; }

    [Header("Progression Tracking")]
    public int currentWorld = 1;
    public int currentLevelInWorld = 0; 
    public int levelsBeforeShop = 3;
    public int maxWorlds = 3;

    [Header("Scene Names")]
    public string tutorialSceneName = "Tutorial";
    public string shopSceneName = "ShopScene";
    public string mainMenuSceneName = "MainMenu";

    [Header("Fade Settings (Code Controlled)")]
    [SerializeField] private CanvasGroup fadeCanvasGroup; 
    [SerializeField] private float fadeSpeed = 2f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        if(fadeCanvasGroup != null) fadeCanvasGroup.alpha = 0;
    }

    // --- START GAME LOGIC ---
    // Call this from your Main Menu "Play" button
    public void StartGame()
    {
        // Check if tutorial has been completed before
        if (PlayerPrefs.GetInt("TutorialDone", 0) == 0)
        {
            SceneManager.LoadScene(tutorialSceneName);
        }
        else
        {
            // If tutorial is done, start the game loop
            currentWorld = 1;
            currentLevelInWorld = 0;
            MoveToNextLocation();
        }
    }

    // Call this specifically from the end of the Tutorial level
    public void FinishTutorial()
    {
        PlayerPrefs.SetInt("TutorialDone", 1);
        PlayerPrefs.Save();
        
        // After tutorial, reset counts and move to the first game location
        currentWorld = 1;
        currentLevelInWorld = 0;
        MoveToNextLocation();
    }

    // --- ELEVATOR SEQUENCE ---
    public void StartElevatorSequence(float animationDuration)
    {
        StartCoroutine(ElevatorToFadeSequence(animationDuration));
    }

    private IEnumerator ElevatorToFadeSequence(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        while (fadeCanvasGroup.alpha < 1)
        {
            fadeCanvasGroup.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }

        // Check if we are exiting the tutorial
        if (SceneManager.GetActiveScene().name == tutorialSceneName)
        {
            FinishTutorial();
        }
        else
        {
            MoveToNextLocation();
        }

        yield return new WaitForSeconds(0.3f);

        while (fadeCanvasGroup.alpha > 0)
        {
            fadeCanvasGroup.alpha -= Time.deltaTime * fadeSpeed;
            yield return null;
        }
    }

    public void MoveToNextLocation()
    {
        if (currentLevelInWorld > 0 && currentLevelInWorld % levelsBeforeShop == 0)
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
        if (currentLevelInWorld > 2) 
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

    // For testing: Call this to force the tutorial to play again
    public void ResetTutorialSave()
    {
        PlayerPrefs.DeleteKey("TutorialDone");
    }
}