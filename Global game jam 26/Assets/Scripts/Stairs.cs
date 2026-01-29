using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stairs : MonoBehaviour
{
    [SerializeField] private Transform targetPostion;
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
            player.transform.position = targetPostion.position;
            gsm.MoveToNextLocation();
        }
    }
}
