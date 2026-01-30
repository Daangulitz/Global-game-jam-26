using System;
using System.Linq;
using UnityEngine;
using Pathfinding; // Required for A* Pathfinding Project

public class EnemyBase : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform player;
    public float detectionRange = 10f;
    public LayerMask obstacleLayer;
    [SerializeField] private float MaxSpeedDelenDoor;
    [SerializeField] private float DetectionRangeDelenDoor;
    
    private PlayerHealth ph;
    private GameManager gm;
    private IAstarAI ai;
    private Animator anim;
    [SerializeField] private EnemyTakeDamage _enemyTakeDamageNormal;
    [SerializeField] private EnemyTakeDamage _enemyTakeDamagehorn;
    
    
    private bool MaxSpeedIsSet = false;
    private bool MaxRangeIsSetForComedy = false;
    private bool MaxRangeIsSetForBlueSpirit = false;

    [SerializeField] private GameObject NormaleAttack;
    [SerializeField] private GameObject HornAttackPos;

    void Start()
    {
        ai = GetComponent<IAstarAI>();
        player = GameObject.FindWithTag("Player").transform;
        ph = FindObjectOfType<PlayerHealth>();
        gm = FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // 1. Check if Player is in range AND we have Line of Sight
        if (distanceToPlayer <= detectionRange && HasLineOfSight())
        {
            // Follow player: Update the destination and resume moving
            ai.destination = player.position;
            ai.isStopped = false;
            anim.SetBool("SeeingPlayer", true);
        }
        else
        {
            ai.isStopped = true;
            anim.SetBool("SeeingPlayer", false);
        }

        if (!MaxSpeedIsSet && gm.masks.Any(m => m.id == 8))
        {
            ai.maxSpeed = ai.maxSpeed / MaxSpeedDelenDoor;
            MaxSpeedIsSet = true;
        }

        if (!MaxRangeIsSetForComedy && gm.masks.Any(m => m.id == 2))
        {
            detectionRange = detectionRange / DetectionRangeDelenDoor;
            MaxRangeIsSetForComedy = true;
        }

        if (!MaxRangeIsSetForBlueSpirit && gm.masks.Any(m => m.id == 1))
        {
            detectionRange = detectionRange / DetectionRangeDelenDoor;
            MaxRangeIsSetForBlueSpirit = true;
        }

        if (gm.masks.Any(m => m.id == 4))
        {
            HornAttackPos.SetActive(true);
            NormaleAttack.SetActive(false);
        }
        else
        {
            HornAttackPos.SetActive(false);
            NormaleAttack.SetActive(true);
        }
    }

    bool HasLineOfSight()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, player.position);

        // Shoot a ray from enemy to player
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, obstacleLayer);

        // If it hits nothing, the path is clear to the player
        return hit.collider == null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!_enemyTakeDamagehorn.DealDamage || !_enemyTakeDamageNormal.DealDamage)
            {
                ph.TakeDamage();
            }
        }
    }

    // Visualizes the detection range in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}