using UnityEngine;

namespace Game._Scripts.Player
{
    public class Movement : MonoBehaviour
    {
        public PlayerConfig config;
        
        private Rigidbody _rb;
        private Animator _animator;
        
        [Header("Check Variables")]
        public bool isWalking;
        public bool isRunning;
        public bool isAiming;
        public bool isMovingBackWard;

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
            isAiming = Input.GetMouseButton(1);
            
            if (isAiming) 
                HandleAimRotation();
            else
                UpdateRotation();

            isMovingBackWard = CheckIsBackWard();

            HandleAnimation();
        }

        private void FixedUpdate()
        {
            HandlePhysic();
        }

        public void Initialize()
        {
            _rb = GetComponent<Rigidbody>();
            _animator = GetComponentInChildren<Animator>();
            _rb.constraints = RigidbodyConstraints.FreezeRotationX 
                             | RigidbodyConstraints.FreezeRotationZ 
                             | RigidbodyConstraints.FreezePositionZ;
            targetRotation = transform.rotation;
            AnimationManager.Instance.InitializeAnimator(_animator, "isWalking", "isRunning", "isBackWard");
        }

        private void HandlePhysic()
        {
            HorizontalMovement();
        }

        private void HandleAnimation()
        {
            
            isWalking = Mathf.Abs(_rb.velocity.x) > 0.1f && !isRunning;
            isRunning = Input.GetKey(KeyCode.LeftShift) && Mathf.Abs(_rb.velocity.x) > 0.1f;
           
            AnimationManager.Instance.SetBool(_animator, "isWalking", isWalking);
            AnimationManager.Instance.SetBool(_animator, "isRunning", isRunning);
            AnimationManager.Instance.SetBool(_animator, "isBackWard", isMovingBackWard);
        }

        private void HorizontalMovement()
        {
            var inputX = Input.GetAxis("Horizontal");
            var angleDiff = Quaternion.Angle(transform.rotation, targetRotation);
            isRunning = Input.GetKey(KeyCode.LeftShift);
            var currentSpeed = isRunning ? config.runSpeed : config.moveSpeed;

            if (angleDiff > config.rotationThreshold)
                currentSpeed *= 0.2f;
            
            _rb.velocity = new Vector3(inputX * currentSpeed, _rb.velocity.y, _rb.velocity.z);
            
            if (!isAiming)
            {
                if (inputX > 0f && !_isFacingRight)
                    Flip();
                else if (inputX < 0f && _isFacingRight)
                    Flip();
            }
            
        }
  
        private void HandleAimRotation()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, transform.position);

            if (plane.Raycast(ray, out float enter))
            {
                Vector3 hit = ray.GetPoint(enter);
                if (hit.x > transform.position.x)
                {
                    targetRotation = Quaternion.Euler(0, 90, 0);
                    _isFacingRight = true;
                }
                else
                {
                    targetRotation = Quaternion.Euler(0, -90, 0);
                    _isFacingRight = false;
                }
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, config.rotationSpeed * Time.deltaTime);
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
               
        private bool CheckIsBackWard()
        {
            float inputX = Input.GetAxisRaw("Horizontal");
            

            if (Mathf.Abs(inputX) > 0.1f)
            {
                bool movingRight = inputX > 0;
                return movingRight != _isFacingRight;
            }
            return false;
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
