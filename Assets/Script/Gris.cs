using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gris : MonoBehaviour
{
    private float moveFactor;
    private float speed;
    private Rigidbody2D rb2D;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public float jumpForce;
    private bool isGrounded;
    private AudioClip jumpClip;
    private AudioClip moveClip;
    private float timer;
    private float timerVal;
    private bool lastIsGrounded;
    private AudioClip landClip;
    private Song song;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        song = GetComponentInChildren<Song>();

        speed = 5f;
        jumpForce = 1800;
        rb2D.gravityScale = 2f;
        isGrounded = true;

        jumpClip = Resources.Load<AudioClip>("Gris/Audioclips/Jump");
        moveClip = Resources.Load<AudioClip>("Gris/Audioclips/Move");
        landClip = Resources.Load<AudioClip>("Gris/Audioclips/Land");

        lastIsGrounded = isGrounded = true;
    }

    void Update()
    {
        if (song != null)
        {
            if (Input.GetKey(KeyCode.K))
            {
                animator.SetBool("Sing", true);
                song.SetSing(true);
                moveFactor = 0;
                return;
            }
            else
            {
                animator.SetBool("Sing", false);
                song.SetSing(false);
            }
        }
        moveFactor = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            AudioSource.PlayClipAtPoint(jumpClip, transform.position);
            rb2D.AddForce(Vector2.up * jumpForce);
            isGrounded = false;
            lastIsGrounded = isGrounded;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            timerVal = 0.8f;
            speed = 2f;
            animator.SetBool("Walk", true);
        }
        else
        {
            timerVal = 0.4f;
            speed = 5f;
            animator.SetBool("Walk", false);
        }
    }

    private void FixedUpdate()
    {
        if (rb2D.velocity.y < -5 && rb2D.velocity.y >= -7)
        {
            isGrounded = false;
            lastIsGrounded = isGrounded;
        }
        Move();
    }

    private void Move()
    {
        animator.SetBool("IsGrounded", isGrounded);
        if (moveFactor > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveFactor < 0)
        {
            spriteRenderer.flipX = false;
        }
        if (Mathf.Abs(moveFactor) > 0.1 && isGrounded)
        {
            if (timer >= timerVal)
            {
                timer = 0;
                AudioSource.PlayClipAtPoint(moveClip, transform.position);
            }
            else
            {
                timer += Time.fixedDeltaTime;
            }
        }
        else
        {
            timer = 0;
        }
        Vector2 moveDirection = Vector2.right * moveFactor;
        Vector2 moveVelocity = moveDirection * speed;
        Vector2 jumpVelocity = new Vector2(0, rb2D.velocity.y);
        animator.SetFloat("MoveY", rb2D.velocity.y);
        rb2D.velocity = moveVelocity + jumpVelocity;
        animator.SetFloat("MoveX", Mathf.Abs(rb2D.velocity.x));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.ClosestPoint(transform.position).y < transform.position.y)
        {
            isGrounded = collision.gameObject.CompareTag("Ground");
            if (isGrounded != lastIsGrounded)
            {
                if (isGrounded)
                {
                    AudioSource.PlayClipAtPoint(landClip, transform.position);
                }
            }
            lastIsGrounded = isGrounded;
        }
    }
}
