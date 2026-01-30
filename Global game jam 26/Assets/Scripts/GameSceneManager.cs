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
        currentWorld = 1;
        currentLevelInWorld = 0;
        MoveToNextLocation();
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

        if (SceneManager.GetActiveScene().name == tutorialSceneName)
        {
            FinishTutorial();
        }
        else
        {
            MoveToNextLocation();
        }

        yield return new WaitForSeconds(0.3f);

        // --- NEW: LOCK PLAYER CHECK ---
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

    // --- NEW: PLAYER LOCKING LOGIC ---
    private void HandlePlayerLockState()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) return;

        // Reset position based on scene
        if (SceneManager.GetActiveScene().name == shopSceneName)
        {
            player.transform.position = new Vector2(0, 100);
            
            // LOCK: Disable movement script (replace 'PlayerController' with your script name)
            if (player.GetComponent<MonoBehaviour>() != null) 
            {
                // Disable whatever script handles your WASD/Joystick movement
                var moveScript = player.GetComponent<PlayerController>(); 
                if (moveScript != null) moveScript.enabled = false;
            }

            // OPTIONAL LOCK: Stop all physics movement
            var rb = player.GetComponent<Rigidbody2D>();
            if (rb != null) 
            {
                rb.linearVelocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Static; // This makes them unmovable
            }
        }
        else
        {
            // UNLOCK for normal levels
            player.transform.position = new Vector2(0, 0);
            
            var moveScript = player.GetComponent<PlayerController>();
            if (moveScript != null) moveScript.enabled = true;

            var rb = player.GetComponent<Rigidbody2D>();
            if (rb != null) rb.bodyType = RigidbodyType2D.Dynamic; // Back to normal physics
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