using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBk : MonoBehaviour
{
    float playerHeight = 2f;

    //[SerializeField] Transform orientation;

    [Header("Movement")]
    public float moveSpeed = 2f;
    public float movementMultiplier = 10f;
    [SerializeField] float airMultiplier = 0.2f;

    [Header("Jumping")]
    public float jumpForce = 5f;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpkey = KeyCode.Space;

    [Header("Drag")]
    float groundDrag = 6f;
    float airDrag = 1f;

    float horizontalMovement;
    float verticalMovement;

    Vector3 moveDirection;

    Rigidbody rb;

    [Header("Ground Detection")]
    [SerializeField] LayerMask groundMask;
    bool isGrounded;
    float groundDistance = 0.4f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

    }

    // Update is called once per frame
    void Update()
    {
        //isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight / 2 + 0.1f);
        isGrounded = Physics.CheckSphere(transform.position - new Vector3(0, 1, 0), groundDistance, groundMask);

        MyInput();

        if (Input.GetKeyDown(jumpkey) && isGrounded)
        {
            Jump();
            LevelManager.Instance.auM.PlayIsGrounded();
        }

        ControlDrag();
    }

    public void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;

    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (isGrounded)
        {

            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }
    }

    void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }

    }

    void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 12)
        {
            rb.AddForce(other.gameObject.transform.forward * 26, ForceMode.Impulse);
        }

        if (other.gameObject.layer == 15)
        {
            rb.AddForce(other.gameObject.transform.up * 26, ForceMode.Impulse);
        }
    }
}


