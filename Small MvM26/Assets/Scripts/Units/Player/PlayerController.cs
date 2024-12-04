using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using SmallGame.Input;
using System.Collections.Generic;
using SmallGame.Physics;

//after a wall jump set a new variable bool and vector2 turn on when wall jumping turn off when input recieved is the opposite direction

namespace SmallGame.Player
{
    enum FacingDirection
    {
        Left = -1,
        Right = 1
    }
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Player Stats")]
        [Tooltip("Life of the player")]
        public float life = 10f;

        [Tooltip("If player can die")]
        public bool invincible = false;

        [Header("Particle Effects")]
        [Tooltip("Trail particles")]
        public ParticleSystem particleJumpUp;

        [Tooltip("Explosion particles")]
        public ParticleSystem particleJumpDown;

        [Header("Movement Settings")]
        [SerializeField]
        [Tooltip("Running speed of the player")]
        float _runSpeed = 5.5f;

        [Header("Jump Settings")]
        [SerializeField]
        [Tooltip("Amount of force added when the player jumps")]
        float _JumpForce = 750f;
        [SerializeField] float _coyoteTime = 0.2f;
        [SerializeField, ReadOnly] float _coyoteTimer = 0f;
        [SerializeField] float _jumpCooldown = 0.14f;
        [SerializeField] float _doubleJumpDivisor = 1.2f;

        [Range(0, .3f)]
        [SerializeField]
        [Tooltip("How much to smooth out the movement")]
        float _movementSmoothing = 0.03f;

        [SerializeField]
        [Tooltip("Whether or not a player can steer while jumping")]
        bool _airControl = true;

        [Header("Ground Check Settings")]
        [SerializeField]
        [Tooltip("A mask determining what is ground to the character")]
        LayerMask _whatIsGround;

        [SerializeField]
        [Tooltip("A position marking where to check if the player is grounded")]
        Transform _groundCheck;

        [SerializeField]
        [Tooltip("A position marking where to check if the player is touching a wall")]
        Transform _wallCheck;

        [Header("Wall Sliding Settings")]
        [SerializeField, ReadOnly]
        [Tooltip("Direction of the wall relative to the player")]
        FacingDirection _wallDirection;

        [SerializeField, ReadOnly]
        [Tooltip("Direction you last wall jumped from")]
        FacingDirection _lastWallJumpDirection = 0;

        const float k_GroundedRadius = 0.15f; // Radius of the overlap circle to determine if grounded
        Rigidbody2D _rb;
        PlayerStateMachine _stateMachine;
        PlayerAnimationSystem _animationSystem;

        [Header("Player Abilities")]
        [SerializeField] bool _movementUnlocked = true;
        [SerializeField] bool _dashingUnlocked = true;
        [SerializeField] bool _doubleJumpUnlocked = true;
        [SerializeField] bool _wallSlideUnlocked = true;

        [Header("Facing Direction")]
        [SerializeField, PlayModeReadOnly]
        [Tooltip("Initial facing direction")]
        FacingDirection _startingDirection = FacingDirection.Right;

        [SerializeField, ReadOnly]
        [Tooltip("Current facing direction")]
        FacingDirection _facingDirection = FacingDirection.Right;

        [SerializeField, ReadOnly]
        [Tooltip("Current velocity of the player")]
        Vector3 _velocity = Vector3.zero;

        [Header("Fall Speed Settings")]
        [SerializeField]
        float _slidingFallSpeed = 3f;

        [SerializeField]
        float _normalFallSpeedLimit = 25;

        [SerializeField, ReadOnly]
        [Tooltip("Limit fall speed")]
        float _currentFallSpeedLimit;

        [Header("Dash Settings")]
        [SerializeField]
        [Tooltip("Dash force applied to the player")]
        float _DashForce = 25f;

        [SerializeField] float DashDuration = 0.1f;
        [SerializeField] float DashRecovery = 0.5f;


        [Header("State Flags")]
        [SerializeField, ReadOnly]
        [Tooltip("Whether or not the player is grounded")]
        bool _grounded;

        [SerializeField, ReadOnly]
        [Tooltip("If player can dash")]
        bool _canDash = true;

        [SerializeField, ReadOnly]
        [Tooltip("If player can double jump")]
        bool _canDoubleJump = true;

        [SerializeField, ReadOnly]
        [Tooltip("If player is dashing")]
        bool _isDashing = false;

        [SerializeField, ReadOnly]
        [Tooltip("If there is a wall in front of the player")]
        bool _isWall = false;

        [SerializeField, ReadOnly]
        [Tooltip("If player is sliding on a wall")]
        bool _isWallSliding = false;

        [SerializeField, ReadOnly]
        [Tooltip("If player was sliding on a wall in the previous frame")]
        bool _oldWallSlidding = false;

        [Header("Wall Jump Settings")]
        [SerializeField, ReadOnly]
        [Tooltip("Start X position for wall jump")]
        float _jumpWallStartX = 0;

        [SerializeField]
        [Tooltip("For limiting wall jump distance with low FPS")]
        bool _limitVelOnWallJump = false;

        [Header("External Forces")]
        [SerializeField] List<ExternalForce> _externalForces = new();

        [Header("Debug")]
        [Tooltip("Current state of the player for debugging purposes")]
        public string DisplayState;
        public void UpdateDisplayState(string display)
        {
            DisplayState = display;
        }

        InputHandler _inputHandler;
        bool _dashInput = false;
        bool _jumpInput = false;
        Vector2 _movementInput = Vector2.zero;
        void RecieveDashInput() => _dashInput = true;
        void RecieveJumpInput() => _jumpInput = true;

        void RecieveMovementInput(Vector2 direction) => _movementInput = direction;

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();

            UpdateFacingDirection((int)_startingDirection);
            _currentFallSpeedLimit = _normalFallSpeedLimit;

        }

        void Start()
        {
            _inputHandler = InputHandler.Instance;
            _inputHandler.OnDashStart.AddListener(RecieveDashInput);
            _inputHandler.OnJumpStart.AddListener(RecieveJumpInput);
            _inputHandler.OnMovement.AddListener(RecieveMovementInput);

            var soundSystem = GetComponent<PlayerSoundSystem>();
            _animationSystem = GetComponent<PlayerAnimationSystem>();
            _stateMachine = new PlayerStateMachine(this, _animationSystem, soundSystem);
            _animationSystem.HandleAwake(_stateMachine);
            _stateMachine.StartMachine(_stateMachine.IdleState);

        }

        void Update()
        {
            _stateMachine.HandleUpdate();

        }
        public void ApplyExternalForces(ExternalForce force) => _externalForces.Add(force);
        public void RemoveExternalForce(ExternalForce force) => _externalForces.Remove(force);
        public Vector2 GetFacingDirection()
        {
            return _facingDirection == FacingDirection.Right ? Vector2.right : Vector2.left;
        }
        void FixedUpdate()
        {
            ApplyExternalForces();
            Move(_movementInput.x, _jumpInput, _dashInput);
            ApplyGravity();
            HandleGroundCheck();
            HandleWallCheck();
            HandleWallJumpLimit();
            UpdateFallSpeedLimit();
            _stateMachine.HandleFixedUpdate();
            _jumpInput = false;
            _dashInput = false;
        }
        void ApplyExternalForces()
        {
            foreach (var force in _externalForces)
            {
                _rb.linearVelocity += force.Force;
            }
        }

        void ApplyGravity()
        {
            if (_rb.linearVelocity.y < -_currentFallSpeedLimit)
            {
                _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, -_currentFallSpeedLimit);
            }

        }

        void HandleGroundCheck()
        {
            bool wasGrounded = _grounded;
            _grounded = false;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.position, k_GroundedRadius, _whatIsGround);
            foreach (var collider in colliders)
            {
                if (collider.gameObject != gameObject)
                {
                    _grounded = true;
                    _coyoteTimer = _coyoteTime;
                    if (!wasGrounded)
                    {
                        _wallMovementOverride = false;
                        HandleLanding();
                        PlayLandingEffects();

                    }
                }
            }
            if (_grounded == false)
            {
                _coyoteTimer -= Time.deltaTime;
            }
        }

        void PlayLandingEffects()
        {
            if (!_isWall && !_isDashing && _canJump)
                particleJumpDown.Play();

            if (_rb.linearVelocity.y < 0f)
                _limitVelOnWallJump = false;
        }

        void UpdateFallSpeedLimit()
        {
            _currentFallSpeedLimit = _isWallSliding ? _slidingFallSpeed : _normalFallSpeedLimit;
        }

        void HandleWallCheck()
        {
            _isWall = false;

            if (!_grounded)
            {
                // OnFallEvent.Invoke();
                HandleFalling();
                Collider2D[] wallColliders = Physics2D.OverlapCircleAll(_wallCheck.position, k_GroundedRadius, _whatIsGround);
                foreach (var collider in wallColliders)
                {
                    if (collider.gameObject != null)
                    {
                        _isDashing = false;
                        _isWall = true;
                        _wallDirection = FindWallDirection(collider.transform);
                    }
                }

            }
        }

        void HandleWallJumpLimit()
        {
            if (!_limitVelOnWallJump) return;

            if (_rb.linearVelocity.y < -0.5f)
            {
                _limitVelOnWallJump = false;
                return;
            }

            var jumpWallDistX = (_jumpWallStartX - transform.position.x) * transform.localScale.x;

            var isInJumpRange = jumpWallDistX > 0 || jumpWallDistX < -2f;
            var isOutOfJumpRange = jumpWallDistX < -0.5f && jumpWallDistX >= -2f;

            if (isInJumpRange)
            {
                _limitVelOnWallJump = false;
                _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y);
            }
            else if (isOutOfJumpRange)
            {
                _movementUnlocked = true;
                _rb.linearVelocity = new Vector2(10f * transform.localScale.x, _rb.linearVelocity.y);
            }
            else
            {
                _movementUnlocked = true;
            }
        }



        FacingDirection FindWallDirection(Transform wall)
        {
            // Define the direction and distance for the raycast
            Vector2 rayDirection = Vector2.left;
            float rayDistance = k_GroundedRadius;

            // Perform raycast to the left from the WallCheck position
            RaycastHit2D[] leftHits = Physics2D.RaycastAll(_wallCheck.position, rayDirection, rayDistance);

            // Iterate through the raycast hits
            foreach (var hit in leftHits)
            {
                // Check if the hit transform matches the wall transform
                if (hit.transform == wall)
                {
                    return FacingDirection.Right; // If wall is on the left, player faces right
                }
            }

            // If no wall is found to the left, assume it's on the right
            return FacingDirection.Left; // If wall is not on the left, player faces left
        }


        public void Move(float move, bool jump, bool dash)
        {
            HandleMovement(move);
            HandleDash(dash);
            HandleJump(jump);
            HandleWallSliding(jump, dash);
        }

        void HandleDash(bool dash)
        {
            if (!_dashingUnlocked) return;

            if (dash && _canDash && !_isWallSliding)
            {
                _canDoubleJump = false;
                StartCoroutine(DashCooldown());
            }
            if (_isDashing)
            {
                _rb.linearVelocity = new Vector2(transform.localScale.x * _DashForce, 0);
            }
        }
        bool _wallMovementOverride;
        void HandleMovement(float move)
        {
            if (!_movementUnlocked) return;

            if (_grounded || _airControl)
            {
                Vector2 targetVelocity = new Vector2(move * _runSpeed, _rb.linearVelocity.y);

                if (_wallMovementOverride)
                {
                    targetVelocity = HandleWallJumpMovement(move, targetVelocity);
                }

                _rb.linearVelocity = Vector3.SmoothDamp(_rb.linearVelocity, targetVelocity, ref _velocity, _movementSmoothing);
                _animationSystem.AnimationVariables.Speed = Mathf.Abs(move);

                if (!_isWallSliding)
                {
                    FaceCorrectDirection(move);
                }
            }
        }

        Vector2 HandleWallJumpMovement(float move, Vector2 targetVelocity)
        {
            if (_lastWallJumpDirection == FacingDirection.Left)
            {
                return HandleLeftWallJump(move, targetVelocity);
            }
            else if (_lastWallJumpDirection == FacingDirection.Right)
            {
                return HandleRightWallJump(move, targetVelocity);
            }
            return targetVelocity;
        }

        Vector2 HandleLeftWallJump(float move, Vector2 targetVelocity)
        {
            if (move > 0)
            {
                _wallMovementOverride = false;
            }
            else
            {
                targetVelocity = new Vector2(-1 * _runSpeed, _rb.linearVelocityY);
            }
            return targetVelocity;
        }
        Vector2 HandleRightWallJump(float move, Vector2 targetVelocity)
        {
            if (move < 0)
            {
                _wallMovementOverride = false;
            }
            else
            {
                targetVelocity = new Vector2(1 * _runSpeed, _rb.linearVelocityY);
            }
            return targetVelocity;
        }


        bool _canJump = true;
        void HandleJump(bool jump)
        {
            if (!_canJump) return;

            if (_coyoteTimer > 0f && jump)
            {
                _coyoteTimer = 0f;
                PerformJump();
                _canDoubleJump = true;
            }
            else if (!_grounded && jump && _canDoubleJump && !_isWallSliding)
            {
                PerformDoubleJump();
            }
        }

        void PerformJump()
        {
            if (_isWallSliding) return;

            StartCoroutine(JumpCooldown());
            _grounded = false;
            _rb.linearVelocity = new Vector2(_rb.linearVelocityX, 0);
            _rb.AddForce(new Vector2(0f, _JumpForce));
            _animationSystem.AnimationVariables.IsJumping = true;
            _animationSystem.AnimationVariables.JumpUp = true;
            particleJumpUp.Play();
        }

        void PerformDoubleJump()
        {
            if (!_doubleJumpUnlocked) return;

            if (_isWall) return;

            _canDoubleJump = false;
            _rb.linearVelocity = new Vector2(_rb.linearVelocityX, 0);
            _rb.AddForce(new Vector2(0f, _JumpForce / _doubleJumpDivisor));
            _animationSystem.AnimationVariables.IsDoubleJumping = true;

        }
        void HandleWallSliding(bool jump, bool dash)
        {
            if (!_wallSlideUnlocked) return;

            if (_grounded)
            {
                _lastWallJumpDirection = 0;
            }

            if (_isWallSliding && !_isWall)
            {
                StopWallSliding();
            }
            else if (_isWall && !_grounded && _rb.linearVelocityY < 0)
            {
                StartWallSliding();
                if (jump)
                {
                    PerformWallJump();
                }
                else if (dash && _canDash && _dashingUnlocked)
                {
                    StartCoroutine(DashCooldown());
                    _rb.linearVelocity = new Vector2(transform.localScale.x * _DashForce, 0);
                }
            }
        }

        void StartWallSliding()
        {
            if (_lastWallJumpDirection == _wallDirection) return;

            if (!_isWallSliding)
            {
                _rb.linearVelocity = new Vector2(_rb.linearVelocityX, 0);
                _coyoteTimer = 0;
            }


            if (!_oldWallSlidding && _rb.linearVelocity.y <= 0 || _isDashing)
            {
                _isWallSliding = true;
                _wallCheck.localPosition = new Vector3(Mathf.Abs(_wallCheck.localPosition.x) * -1, _wallCheck.localPosition.y, 0);
                _canDoubleJump = false;
                _isDashing = false;
                StartCoroutine(WaitToEndSliding());
                _animationSystem.AnimationVariables.IsWallSliding = true;
                _animationSystem.AnimationVariables.IsDoubleJumping = false;
                _animationSystem.AnimationVariables.IsJumping = false;
                _animationSystem.AnimationVariables.IsDashing = false;
                FaceWallSlidingDirection();
                _wallMovementOverride = false;
            }
        }

        void StopWallSliding()
        {
            _isWallSliding = false;
            _wallCheck.localPosition = new Vector3(Mathf.Abs(_wallCheck.localPosition.x), _wallCheck.localPosition.y, 0);
            _canDoubleJump = false;
            _animationSystem.AnimationVariables.IsWallSliding = false;
        }

        void PerformWallJump()
        {
            if (_lastWallJumpDirection == _wallDirection) return;

            _rb.AddForce(new Vector2(transform.localScale.x * _JumpForce * 1.2f, _JumpForce));
            _canDoubleJump = false;
            StopWallSliding();
            _animationSystem.AnimationVariables.IsJumping = true;
            _animationSystem.AnimationVariables.JumpUp = true;

            _lastWallJumpDirection = _wallDirection;
            _wallMovementOverride = true;
        }


        void FaceCorrectDirection(float move)
        {
            if (move == 0 && _grounded)
                return;

            if (_isWallSliding)
            {
                FaceWallSlidingDirection();
            }
            else
            {
                UpdateFacingDirection(move);
                UpdateTransformScale();
            }

            UpdateTransformScale();
        }

        void UpdateFacingDirection(float move)
        {
            if (move < 0)
                _facingDirection = FacingDirection.Left;
            else if (move > 0)
                _facingDirection = FacingDirection.Right;
        }

        void FaceWallSlidingDirection()
        {
            _facingDirection = _wallDirection;
            UpdateTransformScale();
        }
        void UpdateTransformScale()
        {
            var scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * (int)_facingDirection;
            transform.localScale = scale;
        }

        public void ApplyDamage(float damage, Vector3 position)
        {
            if (!invincible)
            {
                _animationSystem.AnimationVariables.Hit = true;
                life -= damage;
                Vector2 damageDir = Vector3.Normalize(transform.position - position) * 40f;
                _rb.linearVelocity = Vector2.zero;
                _rb.AddForce(damageDir * 10);
                if (life <= 0)
                {
                    StartCoroutine(WaitToDead());
                }
                else
                {
                    StartCoroutine(Stun(0.25f));
                    StartCoroutine(MakeInvincible(1f));
                }
            }
        }

        void HandleLanding()
        {
            _animationSystem.AnimationVariables.IsJumping = false;
            _animationSystem.AnimationVariables.IsDoubleJumping = false;
            _animationSystem.AnimationVariables.IsLanding = true;

        }
        void HandleFalling()
        {
            _animationSystem.AnimationVariables.IsJumping = true;
        }

        IEnumerator DashCooldown()
        {
            _animationSystem.AnimationVariables.IsDashing = true;
            _animationSystem.AnimationVariables.IsJumping = false;
            _animationSystem.AnimationVariables.IsDoubleJumping = false;
            _isDashing = true;
            _canDash = false;
            yield return new WaitForSeconds(DashDuration);
            _isDashing = false;
            yield return new WaitForSeconds(DashRecovery);
            _canDash = true;
        }

        IEnumerator JumpCooldown()
        {
            _canJump = false;
            yield return new WaitForSeconds(_jumpCooldown);
            _canJump = true;
        }

        IEnumerator Stun(float time)
        {
            _movementUnlocked = false;
            yield return new WaitForSeconds(time);
            _movementUnlocked = true;
        }
        IEnumerator MakeInvincible(float time)
        {
            invincible = true;
            yield return new WaitForSeconds(time);
            invincible = false;
        }
        IEnumerator WaitToEndSliding()
        {
            yield return new WaitForSeconds(0.1f);
            _animationSystem.AnimationVariables.IsWallSliding = false;
            _oldWallSlidding = false;
            _wallCheck.localPosition = new Vector3(Mathf.Abs(_wallCheck.localPosition.x), _wallCheck.localPosition.y, 0);
        }

        IEnumerator WaitToDead()
        {
            _animationSystem.AnimationVariables.IsDead = true;
            _movementUnlocked = false;
            invincible = true;
            yield return new WaitForSeconds(0.4f);
            _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y);
            yield return new WaitForSeconds(1.1f);
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }
    }


}
