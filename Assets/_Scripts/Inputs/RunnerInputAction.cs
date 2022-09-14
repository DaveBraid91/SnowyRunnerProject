// GENERATED AUTOMATICALLY FROM 'Assets/_Scripts/Inputs/RunnerInputAction.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace _Scripts.Inputs
{
    public class @RunnerInputAction : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @RunnerInputAction()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""RunnerInputAction"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""bfa0ceeb-fcc9-4b4f-8975-7b050e39f527"",
            ""actions"": [
                {
                    ""name"": ""Tap"",
                    ""type"": ""Button"",
                    ""id"": ""90f896d3-c01f-456c-866f-2999f1d43617"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""TouchPosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""801d3880-3e51-4fba-bc8e-590d218e332e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""StartDrag"",
                    ""type"": ""PassThrough"",
                    ""id"": ""46d5d0b9-666e-4f9c-b1b1-4d61b385273b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""EndDrag"",
                    ""type"": ""PassThrough"",
                    ""id"": ""65e82ba8-56dd-43c3-a999-7352922a49cc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6aec6fa1-2c70-4cce-b17d-fc2cb994b5cc"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Computer"",
                    ""action"": ""Tap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""db971644-ddc0-4b76-b94c-36f30dbfeeb4"",
                    ""path"": ""<Touchscreen>/touch*/tap"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""Tap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dc37a6fd-5516-4ff0-9753-4f2d11b3245a"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Computer"",
                    ""action"": ""TouchPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9d374955-4547-4677-bbc2-7bfb39d9e466"",
                    ""path"": ""<Touchscreen>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""TouchPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c7c7b707-4095-4abd-a835-409efdd49e49"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Computer"",
                    ""action"": ""StartDrag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c628cc4a-70f0-4358-b0f1-a853774cc785"",
                    ""path"": ""<Touchscreen>/touch*/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""StartDrag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e09b8572-dbb2-426f-8da0-54c2faff0b1f"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Computer"",
                    ""action"": ""EndDrag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""603e5a52-d9e7-4d10-9da5-b136d2be2a4d"",
                    ""path"": ""<Touchscreen>/touch*/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""EndDrag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Computer"",
            ""bindingGroup"": ""Computer"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": true,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Mobile"",
            ""bindingGroup"": ""Mobile"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // Gameplay
            m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
            m_Gameplay_Tap = m_Gameplay.FindAction("Tap", throwIfNotFound: true);
            m_Gameplay_TouchPosition = m_Gameplay.FindAction("TouchPosition", throwIfNotFound: true);
            m_Gameplay_StartDrag = m_Gameplay.FindAction("StartDrag", throwIfNotFound: true);
            m_Gameplay_EndDrag = m_Gameplay.FindAction("EndDrag", throwIfNotFound: true);
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

        // Gameplay
        private readonly InputActionMap m_Gameplay;
        private IGameplayActions m_GameplayActionsCallbackInterface;
        private readonly InputAction m_Gameplay_Tap;
        private readonly InputAction m_Gameplay_TouchPosition;
        private readonly InputAction m_Gameplay_StartDrag;
        private readonly InputAction m_Gameplay_EndDrag;
        public struct GameplayActions
        {
            private @RunnerInputAction m_Wrapper;
            public GameplayActions(@RunnerInputAction wrapper) { m_Wrapper = wrapper; }
            public InputAction @Tap => m_Wrapper.m_Gameplay_Tap;
            public InputAction @TouchPosition => m_Wrapper.m_Gameplay_TouchPosition;
            public InputAction @StartDrag => m_Wrapper.m_Gameplay_StartDrag;
            public InputAction @EndDrag => m_Wrapper.m_Gameplay_EndDrag;
            public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
            public void SetCallbacks(IGameplayActions instance)
            {
                if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
                {
                    @Tap.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTap;
                    @Tap.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTap;
                    @Tap.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTap;
                    @TouchPosition.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTouchPosition;
                    @TouchPosition.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTouchPosition;
                    @TouchPosition.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTouchPosition;
                    @StartDrag.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnStartDrag;
                    @StartDrag.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnStartDrag;
                    @StartDrag.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnStartDrag;
                    @EndDrag.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEndDrag;
                    @EndDrag.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEndDrag;
                    @EndDrag.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEndDrag;
                }
                m_Wrapper.m_GameplayActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Tap.started += instance.OnTap;
                    @Tap.performed += instance.OnTap;
                    @Tap.canceled += instance.OnTap;
                    @TouchPosition.started += instance.OnTouchPosition;
                    @TouchPosition.performed += instance.OnTouchPosition;
                    @TouchPosition.canceled += instance.OnTouchPosition;
                    @StartDrag.started += instance.OnStartDrag;
                    @StartDrag.performed += instance.OnStartDrag;
                    @StartDrag.canceled += instance.OnStartDrag;
                    @EndDrag.started += instance.OnEndDrag;
                    @EndDrag.performed += instance.OnEndDrag;
                    @EndDrag.canceled += instance.OnEndDrag;
                }
            }
        }
        public GameplayActions @Gameplay => new GameplayActions(this);
        private int m_ComputerSchemeIndex = -1;
        public InputControlScheme ComputerScheme
        {
            get
            {
                if (m_ComputerSchemeIndex == -1) m_ComputerSchemeIndex = asset.FindControlSchemeIndex("Computer");
                return asset.controlSchemes[m_ComputerSchemeIndex];
            }
        }
        private int m_MobileSchemeIndex = -1;
        public InputControlScheme MobileScheme
        {
            get
            {
                if (m_MobileSchemeIndex == -1) m_MobileSchemeIndex = asset.FindControlSchemeIndex("Mobile");
                return asset.controlSchemes[m_MobileSchemeIndex];
            }
        }
        public interface IGameplayActions
        {
            void OnTap(InputAction.CallbackContext context);
            void OnTouchPosition(InputAction.CallbackContext context);
            void OnStartDrag(InputAction.CallbackContext context);
            void OnEndDrag(InputAction.CallbackContext context);
        }
    }
}
