using UnityEngine;

namespace Game._Scripts.Player
{
    public class Movement : MonoBehaviour
    {
        public PlayerConfig config;
        
        private Rigidbody _rb;
        
        [Header("Check Variables")]
        public bool isGrounded;

        public float groundCheckRadius;
        public Transform groundCheck;
        public LayerMask whatIsGround;
        public Quaternion targetRotation;

        private bool _isFacingRight;
        
        private void Awake()
        {
            Initialize();
        }
        
        private void Update()
        {
            HandleInput();
            UpdateRotation();
        }

        private void FixedUpdate()
        {
            HandlePhysic();
        }

        public void Initialize()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.constraints = RigidbodyConstraints.FreezeRotationX 
                             | RigidbodyConstraints.FreezeRotationZ 
                             | RigidbodyConstraints.FreezePositionZ;
            targetRotation = transform.rotation;
        }

        private void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jump();
            }
        }

        private void HandlePhysic()
        {
            GroundCheck();
            HorizontalMovement();
            ApplyBetterJump();
        }

        private void HorizontalMovement()
        {
            var inputX = Input.GetAxis("Horizontal");
            var angleDiff = Quaternion.Angle(transform.rotation, targetRotation);
            var currentSpeed = config.moveSpeed;

            if (angleDiff > config.rotationThreshold)
                currentSpeed *= 0.2f;
            
            _rb.velocity = new Vector3(inputX * currentSpeed, _rb.velocity.y, _rb.velocity.z);
            
            if(inputX > 0f && !_isFacingRight)
                Flip();
            else if(inputX < 0f && _isFacingRight)
                Flip();
        }

        private void ApplyBetterJump()
        {
            if (_rb.velocity.y < 0f)
                _rb.AddForce(Vector3.up * (Physics.gravity.y * (config.fallGravityMultiplier - 1)),
                    ForceMode.Acceleration);
            else if (_rb.velocity.y > 0f)
                _rb.AddForce(Vector3.up * (Physics.gravity.y * (config.riseGravityMultiplier - 1)),
                    ForceMode.Acceleration);
        }

        private void Jump()
        {
            _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);
            _rb.AddForce(Vector3.up * config.jumpHeight, ForceMode.Impulse);
        }

        private void Flip()
        {
            _isFacingRight = !_isFacingRight;
            if (_isFacingRight)
            {
                targetRotation = Quaternion.Euler(0, 90, 0);
            }
            else
            {
                targetRotation = Quaternion.Euler(0, -90, 0);
            }
        }

        private void UpdateRotation()
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, config.rotationSpeed * Time.deltaTime);
        }
        
        private void GroundCheck()
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, whatIsGround);
        }

        private void OnDrawGizmosSelected()
        {
            if (groundCheck != null)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
            }
        }
    }
}
