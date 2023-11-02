using System;
using System.Collections;
using UnityEngine;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof (Rigidbody))]
    [RequireComponent(typeof (CapsuleCollider))]
    public class RigidbodyFirstPersonController : MonoBehaviour
    {
        [Serializable]
        public class MovementSettings
        {
            public float ForwardSpeed = 8.0f;   // Speed when walking forward
            public float BackwardSpeed = 4.0f;  // Speed when walking backwards
            public float StrafeSpeed = 4.0f;    // Speed when walking sideways
            public float SpeedInAir = 8.0f;   // Speed when onair
            public float JumpForce = 30f;
            public float TrampolineForce = 40f;
            public float StopRigidbodyDelay = 0.3f;
            public float TrampolineStopRigidbodyDelay = 0.5f;
            public float FallMultiplier = 5f;
            public float StopJumpVelocity = 5f;
            public float SlideForce = 15f;

            [HideInInspector] public float CurrentTargetSpeed = 8f;
            
#if !MOBILE_INPUT
            private bool m_Running;
#endif

            public void UpdateDesiredTargetSpeed(Vector2 input)
            {
	            if (input == Vector2.zero) return;
				if (input.x > 0 || input.x < 0)
				{
					//strafe
					CurrentTargetSpeed = StrafeSpeed;
				}
				if (input.y < 0)
				{
					//backwards
					CurrentTargetSpeed = BackwardSpeed;
				}
				if (input.y > 0)
				{
					//forwards
					//handled last as if strafing and moving forward at the same time forwards speed should take precedence
					CurrentTargetSpeed = ForwardSpeed;
				}

            }

        }

        public float slideCD = 1;
        public bool canrotate;
        public Camera cam;
        public MovementSettings movementSettings = new MovementSettings();
        public MouseLook mouseLook = new MouseLook();
        public Vector3 relativevelocity;

        public DetectObs detectGround;


        public bool Wallrunning;



        private Rigidbody m_RigidBody;
        private CapsuleCollider m_Capsule;
        private float m_YRotation;
        private bool  m_IsGrounded;
        private bool m_IsSliding;
        private float m_slideCD;

        public Vector3 Velocity
        {
            get { return m_RigidBody.velocity; }
        }

        public bool Grounded
        {
            get { return m_IsGrounded; }
        }


        private void Start()
        {
            LevelManager.Instance.playerController = this;
        }

        private void Awake()
        {
            
            canrotate = true;
            m_RigidBody = GetComponent<Rigidbody>();
            m_Capsule = GetComponent<CapsuleCollider>();
            mouseLook.Init (transform, cam.transform);
        }


        private void Update()
        {
            relativevelocity = transform.InverseTransformDirection(m_RigidBody.velocity);
            if (m_IsGrounded)
            {

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    NormalJump();
                }

                if (Input.GetKeyDown(KeyCode.LeftShift) && !m_IsSliding)
                {
                    Slide();
                }

            }

            if (m_RigidBody.velocity.y < 0)
            {
                m_RigidBody.velocity += Vector3.up * Physics.gravity.y * (movementSettings.FallMultiplier - 1) * Time.deltaTime;
            }

            if (m_IsSliding && !Input.GetKey(KeyCode.LeftShift))
            {
                // Check for obstacles above before stop sliding
                if (!IsObstacleAbove())
                {
                    StopSlide();
                }
            }
        }


        private void LateUpdate()
        {
            if (canrotate)
            {
                RotateView();
            }
            else
            {
                mouseLook.LookOveride(transform, cam.transform);
            }
         

        }
        public void CamGoBack(float speed)
        {
            mouseLook.CamGoBack(transform, cam.transform, speed);

        }
        public void CamGoBackAll ()
        {
            mouseLook.CamGoBackAll(transform, cam.transform);

        }
        private void FixedUpdate()
        {
            GroundCheck();
            Vector2 input = GetInput();

            float h = input.x;
            float v = input.y;
            Vector3 inputVector = new Vector3(h, 0, v);
            inputVector = Vector3.ClampMagnitude(inputVector, 1);

            //grounded
            if ((Mathf.Abs(input.x) > float.Epsilon || Mathf.Abs(input.y) > float.Epsilon) && m_IsGrounded && !Wallrunning)
            {
                if (Input.GetAxisRaw("Vertical") > 0.3f)
                {
                    m_RigidBody.AddRelativeForce(0, 0, Time.deltaTime * 1000f * movementSettings.ForwardSpeed * Mathf.Abs(inputVector.z));
                }
                if (Input.GetAxisRaw("Vertical") < -0.3f)
                {
                    m_RigidBody.AddRelativeForce(0, 0, Time.deltaTime * 1000f * -movementSettings.BackwardSpeed * Mathf.Abs(inputVector.z));
                }
                if (Input.GetAxisRaw("Horizontal") > 0.5f)
                {
                    m_RigidBody.AddRelativeForce(Time.deltaTime * 1000f * movementSettings.StrafeSpeed * Mathf.Abs(inputVector.x), 0, 0);
                }
                if (Input.GetAxisRaw("Horizontal") < -0.5f)
                {
                    m_RigidBody.AddRelativeForce(Time.deltaTime * 1000f * -movementSettings.StrafeSpeed * Mathf.Abs(inputVector.x), 0, 0);
                }

            }
            //inair
            if ((Mathf.Abs(input.x) > float.Epsilon || Mathf.Abs(input.y) > float.Epsilon) && !m_IsGrounded  && !Wallrunning)
            {
                if (Input.GetAxisRaw("Vertical") > 0.3f)
                {
                    m_RigidBody.AddRelativeForce(0, 0, Time.deltaTime * 1000f * movementSettings.SpeedInAir * Mathf.Abs(inputVector.z));
                }
                if (Input.GetAxisRaw("Vertical") < -0.3f)
                {
                    m_RigidBody.AddRelativeForce(0, 0, Time.deltaTime * 1000f * -movementSettings.SpeedInAir * Mathf.Abs(inputVector.z));
                }
                if (Input.GetAxisRaw("Horizontal") > 0.5f)
                {
                    m_RigidBody.AddRelativeForce(Time.deltaTime * 1000f * movementSettings.SpeedInAir * Mathf.Abs(inputVector.x), 0, 0);
                }
                if (Input.GetAxisRaw("Horizontal") < -0.5f)
                {
                    m_RigidBody.AddRelativeForce(Time.deltaTime * 1000f * -movementSettings.SpeedInAir * Mathf.Abs(inputVector.x), 0, 0);
                }

            }

     
        }

        public void NormalJump()
        {
            m_RigidBody.velocity = new Vector3(m_RigidBody.velocity.x, 0f, m_RigidBody.velocity.z);
            m_RigidBody.AddForce(new Vector3(0f, movementSettings.JumpForce, 0f), ForceMode.Impulse);
            StartCoroutine(StopRigidbodyAfterDelay(movementSettings.StopRigidbodyDelay));
        }
        public void Slide()
        {
            gameObject.transform.localScale = new Vector3(1, 0.5f, 0);
            m_IsSliding = true;
            Debug.Log(m_RigidBody.velocity);
            if (m_slideCD > Time.time && (m_RigidBody.velocity.x > 10 || m_RigidBody.velocity.z > 10)) return;
            m_slideCD = Time.time + slideCD;
            m_RigidBody.AddRelativeForce(Vector3.forward * movementSettings.SlideForce, ForceMode.Impulse);
        }
        private bool IsObstacleAbove()
        {
            // Perform a raycast to check for obstacles above
            RaycastHit hit;
            Vector3 rayStart = transform.position + Vector3.up * 0.5f; // Adjust the starting position
            Vector3 rayDirection = Vector3.up;
            return !!Physics.Raycast(rayStart, rayDirection, out hit, 1);
        }
        public void StopSlide()
        {
            // Reset the player's scale to the original size
            gameObject.transform.localScale = Vector3.one;

            // Stop the sliding by setting m_IsSliding to false
            m_IsSliding = false;
        }
        public void SwitchDirectionJump()
        {
            m_RigidBody.velocity = transform.forward * m_RigidBody.velocity.magnitude;
            m_RigidBody.AddForce(new Vector3(0f, movementSettings.JumpForce, 0f), ForceMode.Impulse);
        }
  

      


        private Vector2 GetInput()
        {
            
            Vector2 input = new Vector2
                {
                    x = Input.GetAxisRaw("Horizontal"),
                    y = Input.GetAxisRaw("Vertical")
                };
			movementSettings.UpdateDesiredTargetSpeed(input);
            return input;
        }


        private void RotateView()
        {
            //avoids the mouse looking if the game is effectively paused
            if (Mathf.Abs(Time.timeScale) < float.Epsilon) return;

            // get the rotation before it's changed
            float oldYRotation = transform.eulerAngles.y;

            mouseLook.LookRotation (transform, cam.transform);

       
        }


        /// sphere cast down just beyond the bottom of the capsule to see if the capsule is colliding round the bottom
        private void GroundCheck()
        {
          if(detectGround.Obstruction)
            {
                m_IsGrounded = true;
            }
          else
            {
                m_IsGrounded = false;

            }
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == 12) // Trampoline
            {
                m_RigidBody.velocity = Vector3.zero;
                m_RigidBody.AddForce(collision.gameObject.transform.forward * movementSettings.TrampolineForce, ForceMode.Impulse);
                StartCoroutine(StopRigidbodyAfterDelay(movementSettings.TrampolineStopRigidbodyDelay));
            }
        }

        private IEnumerator StopRigidbodyAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            m_RigidBody.velocity = new Vector3(m_RigidBody.velocity.x, movementSettings.StopJumpVelocity, m_RigidBody.velocity.z);
        }
    }
}
