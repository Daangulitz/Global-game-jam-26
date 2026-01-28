using System;
using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    private GameObject Player;
    private PlayerHealth playerHealth;
    [SerializeField] private int damageAmount = 1;
    [SerializeField] private Transform targetPostiion;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealth.TakeDamage(damageAmount);
            Player.transform.position = targetPostiion.position;
        }
    }
}
