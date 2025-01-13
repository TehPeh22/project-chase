using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 8f;
    private float horizontal;
    private float jumpPower;
    private bool isFaceRight = true;


    // Dashing vars
    private bool canDash = true;
    private bool isDashing;
    private float dashTime = .2f;
    private float dashPower = 25f;
    private float dashCooldown = 5f;

    // Updraft vars
    private bool isUpdraft = true;
    private float updraftPower = 20f;
    private float updraftCooldown = 5f;


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;


    void Update()
    {
        // Prevent player from flipping or moving when dash
        if (isDashing)
        {
            return;
        }

        // horizontal = Input.GetAxisRaw("Horizontal");

        // Move left and right
        rb.linearVelocity = new Vector2(Input.GetAxis("Horizontal"), rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.E) && isDashing)
        {
            StartCoroutine(Dash());
        }

        if (Input.GetKeyDown(KeyCode.Q) && isUpdraft)
        {
            Updraft();
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = false;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2((isFaceRight ? 1 : -1) * dashPower, 0f);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void Updraft()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, updraftPower);
        StartCoroutine(UpdraftCooldown());
    }

    private IEnumerator UpdraftCooldown()
    {
        isUpdraft = false;
        yield return new WaitForSeconds(updraftCooldown);
        isUpdraft = true;
    }
}
