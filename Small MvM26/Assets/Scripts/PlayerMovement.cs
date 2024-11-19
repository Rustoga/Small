using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmallGame.Input;
using UnityEngine;

namespace SmallGame.Player
{
    [RequireComponent(typeof(PlayerController))]
    public class PlayerMovement : MonoBehaviour
    {
        PlayerController _controller;
        InputHandler _input;
        Vector2 _moveInput = Vector2.zero; 
        public float RunSpeed = 25f;
        float _dashDuration = 0.25f;

        bool _isDashing;
        bool _isJumping;
        bool _grounded;
        void Awake()
        {
            _controller = GetComponent<PlayerController>();
        }
        void Start()
        {
            _input = InputHandler.Instance;
            _input.OnMovement.AddListener(UpdateMovement);
            _input.OnDash.AddListener(UpdateDashing);
            _input.OnJump.AddListener(UpdateJumping);
        }


        void Update()
        {

        }
        void FixedUpdate()
        {
            _grounded = CheckGrounded();

            if(_grounded && _isJumping)
            {
                _controller.Jump();
            }

            if (_isDashing)
            {
                _controller.Dash();
            }
            else
            {
                float horizontalSpeed = _moveInput.x * RunSpeed;
                _controller.Move(horizontalSpeed);
            }


        }

        void UpdateMovement(Vector2 dir)
        {
            _moveInput = dir;

            if (!_isDashing)
            {
                if (_moveInput.x == -1) UpdateFacing(FacingDirection.Left);
                if (_moveInput.x == 1) UpdateFacing(FacingDirection.Right);
            }
        }

        async void UpdateDashing(float pressed)
        {
            if (!_isDashing)
            {
                _controller.Dash();
                _isDashing = true;
                await Task.Delay((int)(_dashDuration * 1000));
                print("dash duration over");
                _isDashing = false;
                if (_moveInput.x == -1) UpdateFacing(FacingDirection.Left);
                else if (_moveInput.x == 1) UpdateFacing(FacingDirection.Right);
            }

        }
        void UpdateJumping(float pressed)
        {
            _isJumping = pressed == 1;
        }



        void UpdateFacing(FacingDirection direction)
        {
            var scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * (int)direction;
            transform.localScale = scale;
        }

        [SerializeField] Transform _groundCheckHelper;
        [SerializeField] LayerMask _groundLayers;
        const float k_groundedRadius = 0.15f;
        bool CheckGrounded()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheckHelper.position, k_groundedRadius, _groundLayers);
            for (int i = 0; i < colliders.Length; i++)
            {
                if(colliders[i].gameObject != gameObject)
                {
                    print("grounded");
                    return true;
                }
            }
            print("not grounded");
            return false;
        }


    }
}