using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private Animator elevatorAnimator;
    [SerializeField] private float entryAnimationTime = 2.0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 1. Disable Player input and physics
            var rb = other.GetComponent<Rigidbody2D>();
            if (rb != null) rb.linearVelocity = Vector2.zero;
            
            var pc = other.GetComponent<PlayerController>();
            if (pc != null) pc.enabled = false;

            // 2. Hide Player (simulates them being inside)
            var sr = other.GetComponent<SpriteRenderer>();
            if (sr != null) sr.enabled = false;

            // 3. Play the door closing / entry animation
            if (elevatorAnimator != null)
            {
                elevatorAnimator.SetTrigger("OpenDoors");
            }

            // 4. Tell the manager to wait for the animation, then fade
            GameSceneManager.Instance.StartElevatorSequence(entryAnimationTime);
        }
    }
}