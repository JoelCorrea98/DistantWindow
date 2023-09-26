using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{
    [SerializeField] Transform orientation;

    [Header("Wall Running")]
    [SerializeField] float wallDistance = 0.6f;
    [SerializeField] float minimunJumpHeight = 1.5f;

    [Header("Wall Running")]
    [SerializeField] public float wallRunGravity;
    [SerializeField] public float wallRunJumpForce;

    bool wallLeft = false;
    bool wallRight = false;

    RaycastHit leftWallHit;
    RaycastHit RightWallHit;

    private Rigidbody rb;

    public LayerMask layerMask;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CheckWall();

        if (CanWallRun())
        {
            if (wallLeft)
            {
                StartWallRun();
                Debug.Log("Can run in left walls");
            }

            else if (wallRight)
            {
                StartWallRun();
                Debug.Log("Can run in right walls");
            }
            else
            {
                StopWallRun();
            }
        }
        else
        {
            StopWallRun();
        }

    }
    bool CanWallRun()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minimunJumpHeight);
    }
    void CheckWall()
    {
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallDistance, layerMask);

        if (wallLeft == true)
        {
            Debug.Log("se puede correr por la izquiewrda" + wallLeft);
        }

        wallRight = Physics.Raycast(transform.position, orientation.right, out RightWallHit, wallDistance, layerMask);
    }

    void StartWallRun()
    {
        rb.useGravity = false;

        rb.AddForce(Vector3.down * wallRunGravity, ForceMode.Force);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (wallLeft)
            {
                Vector3 wallRunJumpDirection = transform.up + leftWallHit.normal;
                rb.velocity = new Vector3(rb.velocity.x * 2, 0, rb.velocity.z *2);
                rb.AddForce(wallRunJumpDirection * wallRunJumpForce * 40, ForceMode.Force);
            }
            if (wallRight)
            {
                Vector3 wallRunJumpDirection = transform.up + RightWallHit.normal;
                rb.velocity = new Vector3(rb.velocity.x * 2, 0, rb.velocity.z * 2);
                rb.AddForce(wallRunJumpDirection * wallRunJumpForce * 40, ForceMode.Force);
            }
        }
    }

    void StopWallRun()
    {
        rb.useGravity = true;
    }

}
