using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    private GameManager gameManager;
    private GameSceneManager gsm;
    [SerializeField] private GameObject canvas;
    private bool CanvasActive = false;
    
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gsm = FindObjectOfType<GameSceneManager>();
    }
    
    public void StartGame()
    { 
        gsm.StartGame();
    }

    public void Settings()
    {
        if (!CanvasActive)
        {
            canvas.SetActive(true);
            CanvasActive = true;
        }
        else
        {
            canvas.SetActive(false);
            CanvasActive = false;
        }
    }
    
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game has been closed.");
    }
    
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
