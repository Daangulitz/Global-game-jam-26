using System.Collections;
using UnityEngine;

public class ElevatorExit : MonoBehaviour
{
    private GameObject Player;
    private Rigidbody2D elevatorRigidbody2D;
    private PlayerController playerController;
    private SpriteRenderer SpriteRenderer;
    private Animator elevatorAnimator;
    [SerializeField] private float TimeUntilStopAnimation = 1.04f;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        elevatorRigidbody2D = Player.GetComponent<Rigidbody2D>();
        playerController = Player.GetComponent<PlayerController>();
        SpriteRenderer = Player.GetComponent<SpriteRenderer>();
        elevatorAnimator = GetComponent<Animator>();
    }
    private void Start()
    {
        StartCoroutine(ElevatorSequence());
    }

    private IEnumerator ElevatorSequence()
    {
        elevatorRigidbody2D.linearVelocity = Vector2.zero;
        playerController.enabled = false;
        SpriteRenderer.enabled = false;
        elevatorAnimator.SetTrigger("FadeDone");
        
        yield return new WaitForSeconds(TimeUntilStopAnimation);
        playerController.enabled = true;
        SpriteRenderer.enabled = true;
    }
}
