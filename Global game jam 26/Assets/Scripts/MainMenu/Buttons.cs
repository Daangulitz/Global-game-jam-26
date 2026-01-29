using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [SerializeField] private Mask trymask;
    private GameManager gameManager;
    
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    
    public void StartGame()
    {
        gameManager.AddMask(trymask);
        SceneManager.LoadScene("Daan");
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
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
