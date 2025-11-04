// GENERATED AUTOMATICALLY FROM 'Assets/Main/Scripts/Input/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Main.Scripts.Input
{
    public class @PlayerControls : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PlayerControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""a8f3c02d-95a2-4ca1-897e-f25051a2a3b2"",
            ""actions"": [
                {
                    ""name"": ""LBM"",
                    ""type"": ""Button"",
                    ""id"": ""45214712-4d8c-4e3f-b5e3-88a96fdd6b81"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Space"",
                    ""type"": ""Button"",
                    ""id"": ""5e32c6d9-0948-43df-b4cc-fe9757953389"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""0f3c4596-94a2-4d6f-80a6-467bace312e4"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LBM"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1b19e8bd-3ca0-426c-9c59-32b20f85c5ca"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Space"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Debug"",
            ""id"": ""8e575ae6-a6c2-425c-aa22-d05b6594706b"",
            ""actions"": [
                {
                    ""name"": ""SpeedUp"",
                    ""type"": ""Button"",
                    ""id"": ""a253d8b9-aa0e-4d26-b6b2-5f768b4b1639"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d282d657-2d18-4ce7-b63a-84d37b1d7085"",
                    ""path"": ""<Keyboard>/numpadPlus"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpeedUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Player
            m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
            m_Player_LBM = m_Player.FindAction("LBM", throwIfNotFound: true);
            m_Player_Space = m_Player.FindAction("Space", throwIfNotFound: true);
            // Debug
            m_Debug = asset.FindActionMap("Debug", throwIfNotFound: true);
            m_Debug_SpeedUp = m_Debug.FindAction("SpeedUp", throwIfNotFound: true);
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

        // Player
        private readonly InputActionMap m_Player;
        private IPlayerActions m_PlayerActionsCallbackInterface;
        private readonly InputAction m_Player_LBM;
        private readonly InputAction m_Player_Space;
        public struct PlayerActions
        {
            private @PlayerControls m_Wrapper;
            public PlayerActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @LBM => m_Wrapper.m_Player_LBM;
            public InputAction @Space => m_Wrapper.m_Player_Space;
            public InputActionMap Get() { return m_Wrapper.m_Player; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
            public void SetCallbacks(IPlayerActions instance)
            {
                if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
                {
                    @LBM.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLBM;
                    @LBM.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLBM;
                    @LBM.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLBM;
                    @Space.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpace;
                    @Space.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpace;
                    @Space.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpace;
                }
                m_Wrapper.m_PlayerActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @LBM.started += instance.OnLBM;
                    @LBM.performed += instance.OnLBM;
                    @LBM.canceled += instance.OnLBM;
                    @Space.started += instance.OnSpace;
                    @Space.performed += instance.OnSpace;
                    @Space.canceled += instance.OnSpace;
                }
            }
        }
        public PlayerActions @Player => new PlayerActions(this);

        // Debug
        private readonly InputActionMap m_Debug;
        private IDebugActions m_DebugActionsCallbackInterface;
        private readonly InputAction m_Debug_SpeedUp;
        public struct DebugActions
        {
            private @PlayerControls m_Wrapper;
            public DebugActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @SpeedUp => m_Wrapper.m_Debug_SpeedUp;
            public InputActionMap Get() { return m_Wrapper.m_Debug; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(DebugActions set) { return set.Get(); }
            public void SetCallbacks(IDebugActions instance)
            {
                if (m_Wrapper.m_DebugActionsCallbackInterface != null)
                {
                    @SpeedUp.started -= m_Wrapper.m_DebugActionsCallbackInterface.OnSpeedUp;
                    @SpeedUp.performed -= m_Wrapper.m_DebugActionsCallbackInterface.OnSpeedUp;
                    @SpeedUp.canceled -= m_Wrapper.m_DebugActionsCallbackInterface.OnSpeedUp;
                }
                m_Wrapper.m_DebugActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @SpeedUp.started += instance.OnSpeedUp;
                    @SpeedUp.performed += instance.OnSpeedUp;
                    @SpeedUp.canceled += instance.OnSpeedUp;
                }
            }
        }
        public DebugActions @Debug => new DebugActions(this);
        public interface IPlayerActions
        {
            void OnLBM(InputAction.CallbackContext context);
            void OnSpace(InputAction.CallbackContext context);
        }
        public interface IDebugActions
        {
            void OnSpeedUp(InputAction.CallbackContext context);
        }
    }
}
