using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Follow,
        Attack,
        Dead
    }

    public EnemyState currentState;

    [SerializeField] protected float FollowDistance = 15f;
    [SerializeField] protected float AttackDistance = 2.5f;
    [SerializeField] protected float AttackRate = 1f;
    [SerializeField] private float waitTimeAtDestination = 2f;

    protected float lastAttackTime;
    protected NavMeshAgent agent;
    protected Transform targetPlayer;
    protected PlayerHealth playerHealth;

    private List<Transform> players = new List<Transform>();
    private HashSet<int> knownPlayerIds = new HashSet<int>();
    
    private bool isDead;
    protected Animator animator;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ChangeState(EnemyState.Idle);
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        UpdatePlayers();
        SetTargetPlayer();
        HandleState();
        SetState();
    }

    private void UpdatePlayers()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject obj in playerObjects)
        {
            Transform tf = obj.transform;
            if (tf != null && !knownPlayerIds.Contains(tf.GetInstanceID()))
            {
                players.Add(tf);
                knownPlayerIds.Add(tf.GetInstanceID());
            }
        }

        players.RemoveAll(p => p == null);
    }

    private void SetTargetPlayer()
    {
        float closestDistance = Mathf.Infinity;
        Transform closest = null;

        foreach (Transform player in players)
        {
            float dist = Vector3.Distance(transform.position, player.position);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                closest = player;
            }
        }

        targetPlayer = closest;

        // ✅ Make sure we get the correct health reference
        if (targetPlayer != null)
        {
            playerHealth = targetPlayer.GetComponent<PlayerHealth>();
        }
    }

    public void ChangeState(EnemyState newState)
    {
        currentState = newState;
    }

    protected virtual void SetState()
    {
        if (currentState == EnemyState.Dead || targetPlayer == null) return;

        float distance = Vector3.Distance(transform.position, targetPlayer.position);

        if (distance <= AttackDistance)
        {
            ChangeState(EnemyState.Attack);
        }
        else if (distance <= FollowDistance)
        {
            ChangeState(EnemyState.Follow);
        }
    }

    protected void HandleState()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                UpdateIdle();
                break;
            case EnemyState.Follow:
                UpdateFollow();
                break;
            case EnemyState.Attack:
                UpdateAttack();
                break;
            case EnemyState.Dead:
                if (!isDead) UpdateDead();
                break;
        }
    }

    protected virtual void UpdateIdle()
    {
        
    }


    protected virtual void UpdateFollow()
    {
        if (targetPlayer != null)
        {
            // Stop movement
            agent.isStopped = true;

            // Get direction towards player
            Vector3 direction = (targetPlayer.position - transform.position).normalized;

            // Ignore vertical rotation (keep enemy upright)
            direction.y = 0f;

            if (direction != Vector3.zero)
            {
                // Create target rotation
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                // Smoothly rotate towards player
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
            }
        }
    }

    protected virtual void UpdateAttack()
    {
        if (targetPlayer == null) return;

        agent.isStopped = true;

        Vector3 lookDirection = targetPlayer.position - transform.position;
        lookDirection.y = 0;
        if (lookDirection != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), Time.deltaTime * 5f);
    }

    protected virtual void UpdateDead()
    {
        isDead = true;

        Destroy(gameObject);
    }
}
