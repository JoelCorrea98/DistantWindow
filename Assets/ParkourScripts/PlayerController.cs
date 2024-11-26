﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
public class PlayerController : MonoBehaviour
{
    public float drag_grounded;
    public float drag_inair;

    public DetectObs detectVaultObject; //checks for vault object
    public DetectObs detectVaultObstruction; //checks if theres somthing in front of the object e.g walls that will not allow the player to vault
    public DetectObs detectClimbObject; //checks for climb object
    public DetectObs detectClimbObstruction; //checks if theres somthing in front of the object e.g walls that will not allow the player to climb

    public DetectObs DetectWallL; //detects for a wall on the left
    public DetectObs DetectWallR; //detects for a wall on the right

    public GameObject SpeedFX;
    public float SpeedForFX = 12;

    public Animator cameraAnimator;

    public float WallRunUpForce;
    public float WallRunUpForce_DecreaseRate;
    public float WallRunStraightForce;

    private float upforce;
    private float straightforce;

    public float TrampolineImpulse;
    public float WallJumpUpVelocity;
    public float WallJumpForwardVelocity;
    public float WallJumpSideVelocity;
    public float drag_wallrun;
    public bool WallRunning;
    public bool WallrunningLeft;
    public bool WallrunningRight;
    public bool CantWallrunLeft;
    public bool CantWallrunRight;
    private bool canwallrun; // ensure that player can only wallrun once before needing to hit the ground again, can be modified for double wallruns
    
    public bool IsParkour;
    private float t_parkour;
    private float chosenParkourMoveTime;

    private bool CanVault;
    public float VaultTime; //how long the vault takes
    public Transform VaultEndPoint;

    private bool CanClimb;
    public float ClimbTime; //how long the vault takes
    public Transform ClimbEndPoint;

    private RigidbodyFirstPersonController rbfps;
    private Rigidbody rb;
    private Vector3 RecordedMoveToPosition; //the position of the vault end point in world space to move the player to
    private Vector3 RecordedStartPosition; // position of player right before vault
    // Start is called before the first frame update
    void Start()
    {
        rbfps = GetComponent<RigidbodyFirstPersonController>();
        rb = GetComponent<Rigidbody>();
        Transform sp = LevelManager.Instance.spawnPoint;
        rb.transform.position = sp.position;
        rb.transform.rotation = sp.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (rbfps.Grounded)
        {
            rb.drag = drag_grounded;
            canwallrun = true;
            CantWallrunLeft = false;
            CantWallrunRight = false;
        }
        else
        {
            rb.drag = drag_inair;
        }
        if(WallRunning)
        {
            rb.drag = drag_wallrun;

        }
        //vault
        if (detectVaultObject.Obstruction && !detectVaultObstruction.Obstruction && !CanVault && !IsParkour && !WallRunning
            && (Input.GetKey(KeyCode.Space) || !rbfps.Grounded) && Input.GetAxisRaw("Vertical") > 0f)
        // if detects a vault object and there is no wall in front then player can pressing space or in air and pressing forward
        {
            CanVault = true;
        }

        if (CanVault)
        {
            CanVault = false; // so this is only called once
            rb.isKinematic = true; //ensure physics do not interrupt the vault
            RecordedMoveToPosition = VaultEndPoint.position;
            RecordedStartPosition = transform.position;
            IsParkour = true;
            chosenParkourMoveTime = VaultTime;

            cameraAnimator.CrossFade("Vault",0.1f);
        }


        //Parkour movement
        if (IsParkour && t_parkour < 1f)
        {
            t_parkour += Time.deltaTime / chosenParkourMoveTime;
            transform.position = Vector3.Lerp(RecordedStartPosition, RecordedMoveToPosition, t_parkour);

            if (t_parkour >= 1f)
            {
                IsParkour = false;
                t_parkour = 0f;
                rb.isKinematic = false;

            }
        }

        if (rb.velocity.magnitude > SpeedForFX)
        {
            SpeedFX.SetActive(true);
        } else
        {
            SpeedFX.SetActive(false);
        }

        //Wallrun
        if (DetectWallL.Obstruction && !rbfps.Grounded && !IsParkour && canwallrun && !CantWallrunLeft) // if detect wall on the left and is not on the ground and not doing parkour(climb/vault)
        {
            WallrunningLeft = true;
            canwallrun = false;
            upforce = WallRunUpForce; //refer to line 186
            straightforce = WallRunStraightForce;
            CantWallrunLeft = false;
            CantWallrunRight = false;
        }

        if (DetectWallR.Obstruction && !rbfps.Grounded && !IsParkour && canwallrun && !CantWallrunRight) // if detect wall on thr right and is not on the ground
        {
            WallrunningRight = true;
            canwallrun = false;
            upforce = WallRunUpForce;
            straightforce = WallRunStraightForce;
            CantWallrunLeft = false;
            CantWallrunRight = false;
        }
        if (WallrunningLeft && !DetectWallL.Obstruction) // if there is no wall on the lef tor pressing forward or forward speed < 1 (refer to fpscontroller script)
        {
            WallrunningLeft = false;
            WallrunningRight = false;
        }
        if (WallrunningRight && !DetectWallR.Obstruction) // same as above
        {
            WallrunningLeft = false;
            WallrunningRight = false;
        }

        if (WallrunningLeft || WallrunningRight) 
        {
            WallRunning = true;
            rbfps.Wallrunning = true; // this stops the playermovement (refer to fpscontroller script)
        }
        else
        {
            WallRunning = false;
            rbfps.Wallrunning = false;
            canwallrun = true;
        }

        if (WallrunningRight)
        {
            cameraAnimator.SetBool("WallRight", true);
           
            LevelManager.Instance.auM.PlayWallrun();
        }
        else
        {
            cameraAnimator.SetBool("WallRight", false);
            //LevelManager.Instance.auM.StopWallrun();

        }
        if (WallrunningLeft)
        {
           cameraAnimator.SetBool("WallLeft", true); //Wallrun camera tilt
            LevelManager.Instance.auM.PlayWallrun();
        }
        else
        {
            cameraAnimator.SetBool("WallLeft", false);
            //LevelManager.Instance.auM.StopWallrun();
        }

        

        if (WallRunning)
        {
            //rb.isKinematic = true;
            rb.velocity = (transform.forward * straightforce) + (transform.up * upforce); //set the y velocity while wallrunning
            upforce -= WallRunUpForce_DecreaseRate * Time.deltaTime; //so the player will have a curve like wallrun, upforce from line 136
            straightforce -= WallRunUpForce_DecreaseRate * Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                RaycastHit hit;
                float rayLength = 1.0f; // Adjust this value as needed

                if (Physics.Raycast(transform.position, transform.right * (WallrunningLeft ? -1 : 1), out hit, rayLength))
                {
                    // Get the normal vector at the collision point
                    Vector3 collisionNormal = hit.normal;
                    rb.velocity = Vector3.zero;
                    rb.AddForce((collisionNormal * WallJumpSideVelocity) + (transform.forward * WallJumpForwardVelocity) + (transform.up * WallJumpUpVelocity), ForceMode.Impulse); //walljump
                    CantWallrunLeft = WallrunningLeft;
                    CantWallrunRight = WallrunningRight;
                    WallrunningLeft = false;
                    WallrunningRight = false;
                }
            }
            if(rbfps.Grounded)
            {
                WallrunningLeft = false;
                WallrunningRight = false;
            }
        }
        else
        {
            LevelManager.Instance.auM.StopWallrun();
        }
    }
}
