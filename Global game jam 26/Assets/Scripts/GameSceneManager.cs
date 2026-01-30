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

    [Header("Fade Settings")]
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

    private void OnEnable() => SceneManager.sceneLoaded += OnLevelFinishedLoading;
    private void OnDisable() => SceneManager.sceneLoaded -= OnLevelFinishedLoading;

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        HandlePlayerLockState();
    }

    public void StartGame()
    {
        if (PlayerPrefs.GetInt("TutorialDone", 0) == 0)
        {
            SceneManager.LoadScene(tutorialSceneName);
        }
        else
        {
            currentWorld = 1;
            currentLevelInWorld = 0;
            MoveToNextLocation();
        }
    }

    public void FinishTutorial()
    {
        PlayerPrefs.SetInt("TutorialDone", 1);
        PlayerPrefs.Save();
        
        // After Tutorial -> GO TO SHOP
        currentWorld = 1;
        currentLevelInWorld = 0; 
        SceneManager.LoadScene(shopSceneName);
    }

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

        // Logic for Tutorial exit
        if (SceneManager.GetActiveScene().name == tutorialSceneName)
        {
            FinishTutorial();
        }
        else
        {
            MoveToNextLocation();
        }

        yield return new WaitForSeconds(0.3f);
        HandlePlayerLockState();

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

    private void HandlePlayerLockState()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) return;

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        // Change "PlayerController" to the exact name of your movement script
        MonoBehaviour moveScript = player.GetComponent("PlayerController") as MonoBehaviour;

        if (SceneManager.GetActiveScene().name == shopSceneName)
        {
            // Lock Player
            player.transform.position = new Vector2(0, 100);
            if (rb != null) 
            {
                rb.linearVelocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Static;
            }
            if (moveScript != null) moveScript.enabled = false;
        }
        else if (SceneManager.GetActiveScene().name != mainMenuSceneName)
        {
            // Unlock Player
            if (player.transform.position.y == 100) player.transform.position = Vector2.zero;
            
            if (rb != null) rb.bodyType = RigidbodyType2D.Dynamic;
            if (moveScript != null) moveScript.enabled = true;
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
}