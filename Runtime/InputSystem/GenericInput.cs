// GENERATED AUTOMATICALLY FROM 'Assets/Plugins/unity-utility/Settings/GenericInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Kellojo.InputSystem
{
    public class @GenericInput : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @GenericInput()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""GenericInput"",
    ""maps"": [
        {
            ""name"": ""User Interface"",
            ""id"": ""e396b1df-4e55-48be-970a-027a7b45046b"",
            ""actions"": [
                {
                    ""name"": ""Close"",
                    ""type"": ""Button"",
                    ""id"": ""4530bd47-8b22-4ec7-8939-3fce4d3bbdc8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9de8bd15-8e54-4a97-8f8f-8392d860e9cf"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Close"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // User Interface
            m_UserInterface = asset.FindActionMap("User Interface", throwIfNotFound: true);
            m_UserInterface_Close = m_UserInterface.FindAction("Close", throwIfNotFound: true);
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

        // User Interface
        private readonly InputActionMap m_UserInterface;
        private IUserInterfaceActions m_UserInterfaceActionsCallbackInterface;
        private readonly InputAction m_UserInterface_Close;
        public struct UserInterfaceActions
        {
            private @GenericInput m_Wrapper;
            public UserInterfaceActions(@GenericInput wrapper) { m_Wrapper = wrapper; }
            public InputAction @Close => m_Wrapper.m_UserInterface_Close;
            public InputActionMap Get() { return m_Wrapper.m_UserInterface; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(UserInterfaceActions set) { return set.Get(); }
            public void SetCallbacks(IUserInterfaceActions instance)
            {
                if (m_Wrapper.m_UserInterfaceActionsCallbackInterface != null)
                {
                    @Close.started -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnClose;
                    @Close.performed -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnClose;
                    @Close.canceled -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnClose;
                }
                m_Wrapper.m_UserInterfaceActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Close.started += instance.OnClose;
                    @Close.performed += instance.OnClose;
                    @Close.canceled += instance.OnClose;
                }
            }
        }
        public UserInterfaceActions @UserInterface => new UserInterfaceActions(this);
        public interface IUserInterfaceActions
        {
            void OnClose(InputAction.CallbackContext context);
        }
    }
}
