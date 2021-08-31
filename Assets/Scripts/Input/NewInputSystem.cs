// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input/InputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @NewInputSystem : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @NewInputSystem()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions"",
    ""maps"": [
        {
            ""name"": ""Camera"",
            ""id"": ""dcd696a7-4b27-4045-896c-df54ea8e2fcf"",
            ""actions"": [
                {
                    ""name"": ""Moving"",
                    ""type"": ""Value"",
                    ""id"": ""5e37eff3-5213-49bb-8d35-f750de3b33eb"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotation"",
                    ""type"": ""Value"",
                    ""id"": ""e06c237f-926e-4d80-b59f-6b8f37140e7f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Lock"",
                    ""type"": ""Button"",
                    ""id"": ""4a082b2c-9b8c-48ac-9bb7-af23c8dde9cb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""054b9885-0371-4e44-b1d5-d162bbf32c07"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Moving"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""7ca87852-a87e-4d88-8b8d-93fc62c5853e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Moving"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""b569827c-da94-438a-8404-b7c090b1ff0c"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Moving"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""6e186be4-d0bb-4cf0-84c2-36eff5cb2ee9"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Moving"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ef9ad379-03fb-4929-a109-4342baa4d2a6"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Moving"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""3b8d5801-ed5e-485a-a416-9167a9657252"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""29ae7ca3-6877-4c86-b566-3b37fb424491"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Lock"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Camera
        m_Camera = asset.FindActionMap("Camera", throwIfNotFound: true);
        m_Camera_Moving = m_Camera.FindAction("Moving", throwIfNotFound: true);
        m_Camera_Rotation = m_Camera.FindAction("Rotation", throwIfNotFound: true);
        m_Camera_Lock = m_Camera.FindAction("Lock", throwIfNotFound: true);
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

    // Camera
    private readonly InputActionMap m_Camera;
    private ICameraActions m_CameraActionsCallbackInterface;
    private readonly InputAction m_Camera_Moving;
    private readonly InputAction m_Camera_Rotation;
    private readonly InputAction m_Camera_Lock;
    public struct CameraActions
    {
        private @NewInputSystem m_Wrapper;
        public CameraActions(@NewInputSystem wrapper) { m_Wrapper = wrapper; }
        public InputAction @Moving => m_Wrapper.m_Camera_Moving;
        public InputAction @Rotation => m_Wrapper.m_Camera_Rotation;
        public InputAction @Lock => m_Wrapper.m_Camera_Lock;
        public InputActionMap Get() { return m_Wrapper.m_Camera; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraActions set) { return set.Get(); }
        public void SetCallbacks(ICameraActions instance)
        {
            if (m_Wrapper.m_CameraActionsCallbackInterface != null)
            {
                @Moving.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnMoving;
                @Moving.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnMoving;
                @Moving.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnMoving;
                @Rotation.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnRotation;
                @Rotation.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnRotation;
                @Rotation.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnRotation;
                @Lock.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnLock;
                @Lock.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnLock;
                @Lock.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnLock;
            }
            m_Wrapper.m_CameraActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Moving.started += instance.OnMoving;
                @Moving.performed += instance.OnMoving;
                @Moving.canceled += instance.OnMoving;
                @Rotation.started += instance.OnRotation;
                @Rotation.performed += instance.OnRotation;
                @Rotation.canceled += instance.OnRotation;
                @Lock.started += instance.OnLock;
                @Lock.performed += instance.OnLock;
                @Lock.canceled += instance.OnLock;
            }
        }
    }
    public CameraActions @Camera => new CameraActions(this);
    public interface ICameraActions
    {
        void OnMoving(InputAction.CallbackContext context);
        void OnRotation(InputAction.CallbackContext context);
        void OnLock(InputAction.CallbackContext context);
    }
}
