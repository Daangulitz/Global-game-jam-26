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
    
    private PlayerHealth ph;
    private GameManager gm;
    private IAstarAI ai;
    
    private bool MaxSpeedIsSet = false;
    private bool MaxRangeIsSet = false;

    void Start()
    {
        ai = GetComponent<IAstarAI>();
        player = GameObject.FindWithTag("Player").transform;
        ph = FindObjectOfType<PlayerHealth>();
        gm = FindObjectOfType<GameManager>();
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
        }
        else
        {
            ai.isStopped = true;
        }

        if (!MaxSpeedIsSet && gm.masks.Any(m => m.id == 2))
        {
            ai.maxSpeed = ai.maxSpeed / MaxSpeedDelenDoor;
            MaxSpeedIsSet = true;
        }

        if (!MaxRangeIsSet && gm.masks.Any(m => m.id == 1))
        {
            
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
            ph.TakeDamage();
        }
    }

    // Visualizes the detection range in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}