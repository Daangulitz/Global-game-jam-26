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
    private Animator animator;
    [SerializeField] private AudioClip DeathSound;
    [SerializeField] private AudioSource DamageSound;
    
    private void Start()
    {
        DealDamage = false;
        Player = FindObjectOfType<PlayerController>();
        animator = FindObjectOfType<Animator>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            if (animator != null)
            {
                anim.SetTrigger("IsDead");
            }
            DamageSound.PlayOneShot(DeathSound);
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
