using System;
using UnityEngine;
using System.Collections;

public class EnemyTakeDamage : MonoBehaviour
{
    private PlayerController Player;
    [SerializeField] private GameObject DS;
    public bool DealDamage;
    [SerializeField]private Animator anim;
    private float TimeUntilDeath = 1f;
    
    private void Start()
    {
        DealDamage = false;
        Player = FindObjectOfType<PlayerController>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            anim.SetTrigger("IsDead");
            DealDamage = true;
            StartCoroutine(Die());
        }
    }
    
    public IEnumerator Die()
    {
        WaitForSeconds wait = new WaitForSeconds(TimeUntilDeath);
        yield return wait;
        Destroy(DS);
    }
}
