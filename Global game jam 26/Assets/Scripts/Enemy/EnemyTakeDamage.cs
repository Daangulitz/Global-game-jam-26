using System;
using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    private PlayerController Player;
    [SerializeField] private GameObject DS;
    
    private void Start()
    {
        Player = FindObjectOfType<PlayerController>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && Player.isGrounded == false) 
        {
            Die();
        }
    }


    public void Die()
    {
        Destroy(DS);
    }
}
