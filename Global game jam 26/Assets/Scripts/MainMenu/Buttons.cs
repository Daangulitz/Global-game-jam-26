using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    private GameManager gameManager;
    
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene("Shop");
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
