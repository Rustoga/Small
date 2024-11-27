using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace SmallGame.Input
{
    public class InputHandler : MonoBehaviour
    {
        static InputHandler _instance;
        float _deadZoneMin => InputSystem.settings.defaultDeadzoneMin;

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

        public UnityEvent<Vector2> OnMovement = new();
        public UnityEvent OnAttackStart = new();
        public UnityEvent OnDashStart = new();
        public UnityEvent OnJumpStart = new();
        public UnityEvent OnScreenShot = new();

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
            _controls.attack.started += AttackStarted;
            _controls.dash.started += DashStarted;
            _controls.jump.started += JumpStarted;

            _controls.takeScreenShot.started += TakeScreenShot;

        }

        void OnDisable()
        {
            _playerInput.Disable();

            _controls.movement.performed -= UpdateMovement;
            _controls.movement.canceled -= UpdateMovement;
            _controls.attack.started -= AttackStarted;
            _controls.dash.started -= DashStarted;
            _controls.jump.started -= JumpStarted;

            _controls.takeScreenShot.started -= TakeScreenShot;

        }
        void OnDestroy() {
            OnMovement.RemoveAllListeners();
            OnAttackStart.RemoveAllListeners();
            OnDashStart.RemoveAllListeners();
            OnJumpStart.RemoveAllListeners();
            OnScreenShot.RemoveAllListeners();
        }

        void UpdateMovement(InputAction.CallbackContext c)
        {
            var direction = c.ReadValue<Vector2>();
            
            if(Mathf.Abs(direction.x) < _deadZoneMin) direction.x = 0;
            if(Mathf.Abs(direction.y) < _deadZoneMin) direction.y = 0;

            OnMovement.Invoke(direction);
        }
        
        void AttackStarted(InputAction.CallbackContext c) => OnAttackStart.Invoke();
        void DashStarted(InputAction.CallbackContext c) => OnDashStart.Invoke();
        void JumpStarted(InputAction.CallbackContext c) => OnJumpStart.Invoke();
        void TakeScreenShot(InputAction.CallbackContext c) => OnScreenShot.Invoke();


    }
}
