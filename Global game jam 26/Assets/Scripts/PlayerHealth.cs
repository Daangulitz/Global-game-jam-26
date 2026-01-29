using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

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
            if (gameManager.masks.Any(m => m.id == 0))
            {
                //
                if (Random.value < 0.5f)
                {
                    gameManager.RemoveMask();
                }
                else
                {
                    Debug.LogError("Player Does not have taken a hit");
                }
            }
            else
            {
                gameManager.RemoveMask();
            }

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