using System;
using UnityEngine;

public class CheeseDeathHit : MonoBehaviour
{
    private PlayerHealth ph;

    private void Start()
    {
        ph = FindObjectOfType<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ph.TakeDamage();
        }
    }
}
