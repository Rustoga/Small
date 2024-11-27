//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.11.2
//     from Assets/Scripts/Input System/Gameplay Controls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace SmallGame.Input
{
    public partial class @GameplayControls: IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @GameplayControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""Gameplay Controls"",
    ""maps"": [
        {
            ""name"": ""main"",
            ""id"": ""bb156ce1-6ef4-4ff1-8b08-654eeffb202b"",
            ""actions"": [
                {
                    ""name"": ""movement"",
                    ""type"": ""Value"",
                    ""id"": ""caa9eec5-a84f-413a-b9ac-ec0e0813bd01"",
                    ""expectedControlType"": """",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""jump"",
                    ""type"": ""Value"",
                    ""id"": ""1045df1d-bbca-44fb-bffc-970cd2208ee7"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""attack"",
                    ""type"": ""Button"",
                    ""id"": ""d92ba273-3131-4c05-aec9-a6dcbbc8ab09"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""dash"",
                    ""type"": ""Button"",
                    ""id"": ""67ee16f7-1312-4bd6-bffa-f5eba9203387"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""takeScreenShot"",
                    ""type"": ""Button"",
                    ""id"": ""00646e40-00b4-4b85-a199-26008a97b23a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""208450d4-46e6-4748-a8d5-27ac362ccac3"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""5d7612df-6fac-4103-bac2-345aaf0d67d0"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a1757d3e-6fd3-4ff1-8f19-f4abc5b1012a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ff5d6175-e516-45bf-9f98-c0d172ee2833"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ec6ccf22-7041-4365-aa95-818cd92800a2"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrow Keys"",
                    ""id"": ""1a92f8e8-963d-4715-beaf-dd36c1354420"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""7cd5e37c-25d6-444b-9036-256266b92035"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""fd1ebcd4-73c1-4702-8156-e04efd0acef3"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""89781fce-ea7e-4991-a9a8-c0472e2243b3"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""e0aa0497-a4d5-40b4-b044-6e4446a0e4bb"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""71e59d63-e015-464b-a003-78a3079c0b1e"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""de4c44b4-d8f7-4fe8-b573-7c058e20c1af"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": """",
                    ""action"": ""movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2b037c07-ba87-43e9-aa2d-4b83ef022ab5"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""37d7d21c-cf8c-43cf-bb6d-999d97a9d69b"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dfbb6386-9471-434e-943f-1c2620f2fc9d"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2b3901b7-2588-4165-bf40-799eb79a31a4"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""da5ad4bc-bf62-485f-a8ff-1a6ee3a94135"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3bfc6cbd-208d-42ce-82cb-efb5ea4a692e"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2289f0b4-55bd-491b-ad19-3b32bdac05d1"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""100c1e58-1787-4add-a73b-edcb89b1aebe"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""takeScreenShot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // main
            m_main = asset.FindActionMap("main", throwIfNotFound: true);
            m_main_movement = m_main.FindAction("movement", throwIfNotFound: true);
            m_main_jump = m_main.FindAction("jump", throwIfNotFound: true);
            m_main_attack = m_main.FindAction("attack", throwIfNotFound: true);
            m_main_dash = m_main.FindAction("dash", throwIfNotFound: true);
            m_main_takeScreenShot = m_main.FindAction("takeScreenShot", throwIfNotFound: true);
        }

        ~@GameplayControls()
        {
            UnityEngine.Debug.Assert(!m_main.enabled, "This will cause a leak and performance issues, GameplayControls.main.Disable() has not been called.");
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        public IEnumerable<InputBinding> bindings => asset.bindings;

        public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
        {
            return asset.FindAction(actionNameOrId, throwIfNotFound);
        }

        public int FindBinding(InputBinding bindingMask, out InputAction action)
        {
            return asset.FindBinding(bindingMask, out action);
        }

        // main
        private readonly InputActionMap m_main;
        private List<IMainActions> m_MainActionsCallbackInterfaces = new List<IMainActions>();
        private readonly InputAction m_main_movement;
        private readonly InputAction m_main_jump;
        private readonly InputAction m_main_attack;
        private readonly InputAction m_main_dash;
        private readonly InputAction m_main_takeScreenShot;
        public struct MainActions
        {
            private @GameplayControls m_Wrapper;
            public MainActions(@GameplayControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @movement => m_Wrapper.m_main_movement;
            public InputAction @jump => m_Wrapper.m_main_jump;
            public InputAction @attack => m_Wrapper.m_main_attack;
            public InputAction @dash => m_Wrapper.m_main_dash;
            public InputAction @takeScreenShot => m_Wrapper.m_main_takeScreenShot;
            public InputActionMap Get() { return m_Wrapper.m_main; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(MainActions set) { return set.Get(); }
            public void AddCallbacks(IMainActions instance)
            {
                if (instance == null || m_Wrapper.m_MainActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_MainActionsCallbackInterfaces.Add(instance);
                @movement.started += instance.OnMovement;
                @movement.performed += instance.OnMovement;
                @movement.canceled += instance.OnMovement;
                @jump.started += instance.OnJump;
                @jump.performed += instance.OnJump;
                @jump.canceled += instance.OnJump;
                @attack.started += instance.OnAttack;
                @attack.performed += instance.OnAttack;
                @attack.canceled += instance.OnAttack;
                @dash.started += instance.OnDash;
                @dash.performed += instance.OnDash;
                @dash.canceled += instance.OnDash;
                @takeScreenShot.started += instance.OnTakeScreenShot;
                @takeScreenShot.performed += instance.OnTakeScreenShot;
                @takeScreenShot.canceled += instance.OnTakeScreenShot;
            }

            private void UnregisterCallbacks(IMainActions instance)
            {
                @movement.started -= instance.OnMovement;
                @movement.performed -= instance.OnMovement;
                @movement.canceled -= instance.OnMovement;
                @jump.started -= instance.OnJump;
                @jump.performed -= instance.OnJump;
                @jump.canceled -= instance.OnJump;
                @attack.started -= instance.OnAttack;
                @attack.performed -= instance.OnAttack;
                @attack.canceled -= instance.OnAttack;
                @dash.started -= instance.OnDash;
                @dash.performed -= instance.OnDash;
                @dash.canceled -= instance.OnDash;
                @takeScreenShot.started -= instance.OnTakeScreenShot;
                @takeScreenShot.performed -= instance.OnTakeScreenShot;
                @takeScreenShot.canceled -= instance.OnTakeScreenShot;
            }

            public void RemoveCallbacks(IMainActions instance)
            {
                if (m_Wrapper.m_MainActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(IMainActions instance)
            {
                foreach (var item in m_Wrapper.m_MainActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_MainActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public MainActions @main => new MainActions(this);
        public interface IMainActions
        {
            void OnMovement(InputAction.CallbackContext context);
            void OnJump(InputAction.CallbackContext context);
            void OnAttack(InputAction.CallbackContext context);
            void OnDash(InputAction.CallbackContext context);
            void OnTakeScreenShot(InputAction.CallbackContext context);
        }
    }
}
