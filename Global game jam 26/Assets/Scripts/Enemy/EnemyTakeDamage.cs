using System;
using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    private PlayerController Player;
    [SerializeField] private GameObject DS;
    public bool DealDamage;
    
    private void Start()
    {
        DealDamage = false;
        Player = FindObjectOfType<PlayerController>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            DealDamage = true;
            Die();
        }
    }
    
    public void Die()
    {
        Destroy(DS);
    }
}
