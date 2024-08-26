using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [Header("Movement Settings:")]
    public float moveSpeed;
    public float groundDrag;

    public float jumpForce;
    public float jumpCoolDown;
    public float airMultiplier;
    bool readyToJump;

    [Space(5)]

    [Header("Keybinds:")]
    public KeyCode jumpKey = KeyCode.Space;
    [Space(5)]

    [Header("Ground Settings")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;
    [Space(5)]
    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDir;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        Debug.Log("Grounded é: " + grounded);

        PlayerInput();
        SpeedControl();

        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Debug.Log("Pulou");
            Jump();

            Invoke(nameof(ResetJump), jumpCoolDown);
        }
    }

    public void PlayerMove()
    {
        moveDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
        {
            rb.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded)
        {
            rb.AddForce(moveDir.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }

    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitVel.x, rb.velocity.y, limitVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}
