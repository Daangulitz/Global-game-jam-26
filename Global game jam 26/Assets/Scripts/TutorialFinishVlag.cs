using UnityEngine;

public class TutorialFinishVlag : MonoBehaviour
{
    private GameObject player;
    private GameSceneManager gsm;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gsm = FindObjectOfType<GameSceneManager>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.transform.position = new Vector2(0,0);
            gsm.FinishTutorial();
            Debug.Log(player.transform.position);
        }
    }
}
