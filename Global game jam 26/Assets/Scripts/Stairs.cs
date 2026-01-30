using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stairs : MonoBehaviour
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
            gsm.MoveToNextLocation();
            Debug.Log(player.transform.position);
        }
    }
}
