using System;
using SmallGame.Controls;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace SmallGame.Input
{
    public class InputHandler : MonoBehaviour
    {
        static InputHandler _instance;

        public static InputHandler Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindAnyObjectByType<InputHandler>();
                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject(typeof(InputHandler).ToString());
                        _instance = singleton.AddComponent<InputHandler>();
                        DontDestroyOnLoad(singleton);
                    }
                }
                return _instance;
            }
        }

        GameplayControls _playerInput;
        GameplayControls.MainActions _controls;

        public UnityEvent<Vector2> onMovement = new();
        public UnityEvent<float> onJump = new();
        public UnityEvent<float> onAttack = new();

        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }

            _playerInput = new();
            _playerInput.Enable();
            _controls = _playerInput.main;
        }

        void OnEnable()
        {
            _playerInput.Enable();

            _controls.movement.performed += UpdateMovement;
            _controls.movement.canceled += UpdateMovement;
            _controls.jump.performed += UpdateJump;
            _controls.jump.canceled += UpdateJump;
            _controls.attack.performed += UpdateAttack;
            _controls.attack.canceled += UpdateAttack;

        }


        void OnDisable()
        {
            _playerInput.Disable();

            _controls.movement.performed -= UpdateMovement;
            _controls.movement.canceled -= UpdateMovement;
            _controls.jump.performed -= UpdateJump;
            _controls.jump.canceled -= UpdateJump;
            _controls.attack.performed += UpdateAttack;
            _controls.attack.canceled += UpdateAttack;

        }

        void UpdateMovement(InputAction.CallbackContext c)
        {
            var direction = c.ReadValue<Vector2>();
            onMovement.Invoke(direction);
        }

        void UpdateJump(InputAction.CallbackContext c)
        {

            var pressed = c.ReadValue<float>();
            onJump.Invoke(pressed);
        }
        void UpdateAttack(InputAction.CallbackContext c)
        {
            var pressed = c.ReadValue<float>();
            onAttack.Invoke(pressed);
        }



    }
}
