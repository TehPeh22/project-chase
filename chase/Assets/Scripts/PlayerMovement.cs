using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    // Player movement vars
    private float horizontal;
    private float speed = 9f;
    private float jumpPower = 12f;
    private bool isFaceRight = true;


    // Dashing vars
    private bool canDash = true;
    private bool isDashing;
    private float dashTime = .2f;
    private float dashPower = 30f;
    private float dashCooldown = 1f;

    // Updraft vars
    private bool isUpdraft = true;
    private float updraftPower = 20f;
    private float updraftCooldown = 2f;


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer dashTrail;
    [SerializeField] private TrailRenderer updraftTrail;
    [SerializeField] private Animator animator;

    [SerializeField] private Transform shootingPoint;
    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;

        if (mainCam == null)
        {
            Debug.LogError("Main cam not found");
        }
    }


    void Update()
    {
        // Prevent player from flipping or moving when dash
        if (isDashing)
        {
            return;
        }

        // Movement left, right and jump
        horizontal = Input.GetAxisRaw("Horizontal"); // Returns -1, 1, 0 depending on direction moving 
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
        }

        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f); // Allows player to jump higher when holding space
        }

        if (horizontal != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else {
            animator.SetBool("isRunning", false);
        }

        if (Input.GetKeyDown(KeyCode.E) && canDash)
        {
            StartCoroutine(Dash());
        }

        if (Input.GetKeyDown(KeyCode.Q) && isUpdraft)
        {
            StartCoroutine(Updraft());
        }

        if (Input.GetButtonDown("Fire1"))
        {
            FaceFirePoint();
        }

        Flip();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }

    // Creates an invisible circle at the feet of player. When feet collide with ground, enable jump 
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundLayer);
    }

    private void Flip()
    {
        //flip char based on movement direction 
        if (isFaceRight && horizontal < 0f || !isFaceRight && horizontal > 0f)
        {
            isFaceRight = !isFaceRight;

            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            // transform.localScale = localScale;

            // Rotate player and fire point
            transform.Rotate(0f, 180f, 0f);
        }
    }

    void FaceFirePoint()
    {
        float directionToShootPoint = shootingPoint.position.x - transform.position.x; // Get direction of the shootingPoint relative to player

        if (directionToShootPoint > 0 && !isFaceRight)
        {
            FlipChar();
        }
        else if (directionToShootPoint < 0 && isFaceRight)
        {
            FlipChar();
        }
    }

    void FlipChar()
    {
        isFaceRight = !isFaceRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        
        float originalGravity = rb.gravityScale; // Dash not affected by gravity
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2((isFaceRight ? 1 : -1) * dashPower, 0f);
        dashTrail.emitting = true;

        yield return new WaitForSeconds(dashTime); // Wait .5sec after dash
        dashTrail.emitting = false;
        rb.gravityScale = originalGravity; // Revert to original gravity
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private IEnumerator Updraft()
    {
        isUpdraft = false;
        updraftTrail.emitting = true;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, updraftPower);
        yield return new WaitForSeconds(.2f);

        updraftTrail.emitting = false;
        yield return new WaitForSeconds(updraftCooldown);
        isUpdraft = true;
    }
}
