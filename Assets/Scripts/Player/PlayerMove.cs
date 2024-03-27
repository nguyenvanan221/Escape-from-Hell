using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    PlayerControls controls;

    Rigidbody2D rb;
    Animator animator;

    // input value
    private float horizontalValue;
    private float verticalValue;

    [Header("Move")]
    [SerializeField] private float speed;

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform checkPos;
    [SerializeField] private float radius;
    public static bool isJumping;

    //climp ladder
    public static bool isClimbing = false;
    private bool hasTriggerClimp = false;
    private readonly HashSet<GameObject> ladders = new();

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        controls = new();
        controls.Enable();

        controls.Land.Move.performed += ctx =>
        {
            horizontalValue = ctx.ReadValue<float>();
        };

        controls.Land.Jump.performed += ctx => Jump();

        controls.Land.Climp.performed += ctx =>
        {
            verticalValue = ctx.ReadValue<float>();
        };
    }

    void Update()
    {
        if (UIManager.Instance.isPause) { return; }


        if (ladders.Count > 0 && Mathf.Abs(verticalValue) > 0f && !isJumping)
        {
            isClimbing = true;
        }
        else if (ladders.Count <= 0)
        {
            isClimbing = false;

        }

        if (!isClimbing)
        {
            Move();
        }
        
        animator.SetFloat("YInput", rb.velocity.y);
    }

    private void FixedUpdate()
    {
        // avoid press 2 button jump and climb
        if (IsGrounded())
        {
            isJumping = false;
        }
        else isJumping = true;

        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, verticalValue * speed);
            if (!hasTriggerClimp)
            {
                animator.SetTrigger("isClimp");
                hasTriggerClimp = true;
            }
        }
        else
        {
            rb.gravityScale = 1f;
        }

        if (!isClimbing)
        {
            animator.SetBool("isJumping", !IsGrounded());
        }

        if (isClimbing && IsGrounded() && Mathf.Abs(verticalValue) == 0f)
        {
            animator.Play("Move");
            isClimbing= false;
            hasTriggerClimp= false;
            Debug.Log("play move");
        }

    }

    public void Move()
    {
        rb.velocity = new Vector2(horizontalValue * speed, rb.velocity.y);

        if (horizontalValue > 0)
        {
            transform.localScale = new Vector2(5, 5);

        }
        else if (horizontalValue < 0)
        {
            transform.localScale = new Vector2(-5, 5);

        }

        animator.SetFloat("XInput", horizontalValue);

    }

    public void Jump()
    {
        if (!isJumping && !isClimbing)
        {
            isJumping = true;
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(checkPos.position, radius, groundLayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Ladder"))
        {

            ladders.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Ladder"))
        {
            ladders.Remove(collision.gameObject);

            hasTriggerClimp = false;
        }
    }

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
}
