using System;
using SmallGame.Input;
using UnityEngine;

namespace SmallGame
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        CharacterController _controller;
        InputHandler _input;
        public float RunSpeed = 2.5f;
        Vector2 _moveInput = Vector2.zero;


        void Awake() {
            _controller = GetComponent<CharacterController>();
        }
        void Start() {
            _input = InputHandler.Instance;
            _input.onMovement.AddListener(UpdateMovement);
        }


        void Update() {
            
        }

        void FixedUpdate() {
            float horizontalSpeed = _moveInput.x * RunSpeed; 
            _controller.Move(horizontalSpeed);
        }

        void UpdateMovement(Vector2 dir)
        {
               _moveInput = dir;
        }
        
    }
}
