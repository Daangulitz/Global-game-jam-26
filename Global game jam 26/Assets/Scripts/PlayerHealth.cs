using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        FindGameManager();
    }
    
    private void FindGameManager()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    public void TakeDamage()
    {
        if (gameManager == null) FindGameManager();

        if (gameManager != null && gameManager.masks.Count > 0)
        {
            gameManager.RemoveMask();

            if (gameManager.masks.Count <= 0)
            {
                Death();
            }
        }
    }

    private void Death()
    {
        SceneManager.LoadScene("MainMenu");
    }
}