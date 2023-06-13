using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputHandler _input;

    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public float dashDistance;
    public float dashDuration;
    public float dashCooldown;

    private bool readyToJump;
    public bool readyToDash;
    private bool isDashing;
    private Vector3 dashDirection;
    public float dashTimer;
    public float dashCooldownTimer;
    public AudioClip[] audioClips;

    public float playerHeight;
    public LayerMask whatIsGround;
    private bool grounded;

    public Transform orientation;

    private Vector3 moveDirection;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
        readyToDash = true;
    }

    private void Update()
    {
        // Ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        SpeedControl();

        if (_input.Jump && readyToJump && grounded && !GetComponent<PlayerHealth>().IsDead())
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
            PlayJumpSound();
        }

        if (_input.Dash && readyToDash && !GetComponent<PlayerHealth>().IsDead())
        {
            Dash();
            PlayDashSound();
        }

        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f)
            {
                StopDash();
            }
        }
        else if (!readyToDash)
        {
            dashCooldownTimer -= Time.deltaTime;
            if (dashCooldownTimer <= 0f)
            {
                readyToDash = true;
            }
        }

        // Handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        if (!GetComponent<PlayerHealth>().IsDead())
        {
            MovePlayer();
        }
    }

    private void MovePlayer()
    {
        // Calculate movement direction
        moveDirection = orientation.forward * _input.Move.y + orientation.right * _input.Move.x;

        // On ground
        if (grounded)
        {
            rb.AddForce(10f * SpeedScaling() * moveDirection.normalized, ForceMode.Force);
        }

        // In air
        else if (!grounded)
        {
            rb.AddForce((SpeedScaling() / 2) * 10f * airMultiplier * moveDirection.normalized, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new(rb.velocity.x, 0f, rb.velocity.z);

        // Limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // Reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

private void Dash()
{
    if (readyToDash)
    {
        readyToDash = false;
        isDashing = true;

        // Calculate the dash direction based on player orientation
        dashDirection = orientation.forward;

        // Preserve the current y velocity to maintain the player's vertical position
        float preservedYVelocity = rb.velocity.y;

        // Apply the dash velocity, preserving the y velocity
        rb.velocity = (dashDirection * (dashDistance / dashDuration)) + (Vector3.up * preservedYVelocity);

        dashTimer = dashDuration;
        dashCooldownTimer = dashCooldown;

        Invoke(nameof(StopDash), dashDuration);
    }
}


   private void StopDash()
{
    isDashing = false;

}

    public float SpeedScaling()
    {
        float speed = moveSpeed + (GetComponent<PlayerUpgrades>().speed * 3);
        return speed;
    }

    public void PlayJumpSound()
    {
        GetComponent<AudioSource>().clip = audioClips[0];
        GetComponent<AudioSource>().Play();
    }

    public void PlayDashSound()
    {
        GetComponent<AudioSource>().clip = audioClips[1];
        GetComponent<AudioSource>().Play();
    }
}
