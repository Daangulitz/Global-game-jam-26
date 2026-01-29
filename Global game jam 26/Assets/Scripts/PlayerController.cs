using System;
using System.Collections.Generic;
using System.Linq;
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
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true; 
    }

    [Header("Movement Settings")]
    public float constantForwardSpeed = 5f;
    public float maxSpeed;
    public float jumpForce = 12f;
    public int baseJumpCount = 1;

    [Header("Detection")]
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Input References")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] private InputActionReference attackAction;

    [Header("MaskUpgradeSettings")] 
    [SerializeField] private float BlueSpiritMaskJumpUpgradeX;
    [SerializeField] private float TimeUntilRacingMaskBreaks;
    [SerializeField] private float UpgradeAmountRacingMaskSpeedX;

    private Rigidbody2D rb;
    private float steerInput;
    public bool isGrounded;
    public bool isFacingRight;
    private float horizontalVelocity;
    private float timeUntilRacingMaskBreaks;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private GameManager gm;

    private int jumpsRemaining;
    private bool BlueSpiritMaskActive;
    private bool RacingMaskActive;

    private void OnEnable()
    {
        if (moveAction != null) moveAction.action.Enable();
        if (jumpAction != null) 
        {
            jumpAction.action.Enable();
            jumpAction.action.performed += OnJump;
        }
        if (attackAction != null)
        {
            attackAction.action.Enable();
            attackAction.action.performed += OnAttack;
        }

    }

    private void OnDisable()
    {
        if (jumpAction != null)
        {
            jumpAction.action.performed -= OnJump;
        }
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        gm = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        steerInput = moveAction.action.ReadValue<Vector2>().x;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
        
        // Reset jumps and handle mask logic when grounded
        if (isGrounded && rb.linearVelocity.y <= 0.1f)
        {
            int maxJumps = baseJumpCount;
            
            if (gm.masks.Any(m => m.id == 9))
            {
                maxJumps += 1;
            }

            jumpsRemaining = maxJumps;
            _animator.SetBool("isGrounded", true);
        }
        else
        {
            _animator.SetBool("isGrounded", false);
        }

        if (rb.linearVelocity.magnitude > maxSpeed)
            rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxSpeed);
        
        if (gm.masks.Any(m => m.id == 1) && !BlueSpiritMaskActive)
        {
            jumpForce = jumpForce * BlueSpiritMaskJumpUpgradeX;
            BlueSpiritMaskActive = true;
        }

        if (gm.masks.Any(m => m.id == 3) && !RacingMaskActive)
        {
            constantForwardSpeed = constantForwardSpeed * UpgradeAmountRacingMaskSpeedX;
            RacingMaskActive = true;
        }

        if (steerInput == 0)
        {
            timeUntilRacingMaskBreaks =+ Time.deltaTime;
            if (timeUntilRacingMaskBreaks == TimeUntilRacingMaskBreaks)
            {
                RemoveSpecificMask(3);
                constantForwardSpeed = constantForwardSpeed / UpgradeAmountRacingMaskSpeedX;
            }
        } else if (steerInput != 0)
        {
            timeUntilRacingMaskBreaks = 0f;
        }
    }

    private void FixedUpdate()
    {
        horizontalVelocity = (steerInput < 0) ? -constantForwardSpeed : constantForwardSpeed;
        if (steerInput != 0)
        {
            _animator.SetFloat("Speed", 1f);
            rb.AddForce(Vector3.right * steerInput * constantForwardSpeed);
        }
        else
        {
            _animator.SetFloat("Speed", 0f);
        }

        if (steerInput > 0)
        {
            _spriteRenderer.flipX = false;
            isFacingRight = true;
        }
        else if (steerInput < 0)
        {
            _spriteRenderer.flipX = true;
            isFacingRight = false;
        }
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (jumpsRemaining > 0)
        {
            // Reset Y velocity for consistent multi-jump height
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            
            jumpsRemaining--;
            
            _animator.SetTrigger("Jump");
        }
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        //start attack animation

    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = isGrounded ? Color.green : Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
        }
    }
    
    private void RemoveSpecificMask(int idToRemove)
    {
        // Use a list to find the item
        List<Mask> temp = gm.masks.ToList();
        // Remove only the first instance of ID 0 found
        Mask toRemove = temp.FirstOrDefault(m => m.id == idToRemove);
        
        if (toRemove != null)
        {
            temp.Remove(toRemove);
            gm.masks.Clear();
            // Re-stack items in correct order
            temp.Reverse();
            foreach (var m in temp)
            {
                gm.masks.Push(m);
            }
        }
    }
}