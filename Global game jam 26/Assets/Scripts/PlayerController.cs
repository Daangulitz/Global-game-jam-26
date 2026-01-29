using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    // --- SINGLETON & PERSISTENCE ---
    public static PlayerController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            // If a player already exists, kill this new one immediately
            Destroy(gameObject);
            return;
        }

        Instance = this;
        
        // This command tells Unity NOT to destroy this object when loading a new scene
        DontDestroyOnLoad(gameObject);

        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true; 
    }
    // -------------------------------

    [Header("Movement Settings")]
    public float constantForwardSpeed = 5f;
    public float maxSpeed;
    public float jumpForce = 12f;

    [Header("Detection")]
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Input References")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;

    private Rigidbody2D rb;
    private float steerInput;
    public bool isGrounded;
    private float horizontalVelocity;

    private Animator _animator;
    

    private void OnEnable()
    {
        // Make sure actions are enabled
        if (moveAction != null) moveAction.action.Enable();
        if (jumpAction != null) 
        {
            jumpAction.action.Enable();
            jumpAction.action.performed += OnJump;
        }
    }

    private void OnDisable()
    {
        // Unsubscribe to avoid memory leaks
        if (jumpAction != null)
        {
            jumpAction.action.performed -= OnJump;
        }
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        steerInput = moveAction.action.ReadValue<Vector2>().x;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
        if (rb.linearVelocity.magnitude > maxSpeed)
            rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxSpeed);


        if (isGrounded)
        {
            _animator.SetBool("Jump", false);
        }
    }

    private void FixedUpdate()
    {
        // Always moving logic
        horizontalVelocity = (steerInput < 0) ? -constantForwardSpeed : constantForwardSpeed;
        if (steerInput != 0)
        {
            _animator.SetFloat("Speed", 1f);
            rb.linearVelocity = new Vector2(rb.linearVelocityX, rb.linearVelocity.y);
            rb.AddForce(Vector3.right * steerInput * constantForwardSpeed);
        }
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce);
            //rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            _animator.SetBool("Jump", true);
        }
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = isGrounded ? Color.green : Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
        }
    }
}