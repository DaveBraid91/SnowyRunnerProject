using UnityEngine;

namespace _Scripts.Inputs
{
    public class InputManager : MonoBehaviour
    {
        //There should be only one InputManager in the scene
        private static InputManager _instance;
        public static InputManager Instance { get { return _instance; } }

        //Action schemes
        private RunnerInputAction actionScheme;

        //Configuration
        [SerializeField] private float sqrSwipeDeadZone;

        #region Public Properties
        public bool Tap 
        { 
            get 
            {
                _tapCount++;
                return _tap; 
            } 
        }
        public Vector2 TouchPosition { get { return _touchPosition; } }
        public bool SwipeLeft { get { return _swipeLeft; } }
        public bool SwipeRight { get { return _swipeRight; } }
        public bool SwipeUp { get { return _swipeUp; } }
        public bool SwipeDown { get { return _swipeDown; } }
        #endregion

        #region Private Properties
        private int _tapCount = 0;
        private bool _tap;
        private Vector2 _touchPosition;
        private Vector2 _startDrag;
        private bool _swipeLeft;
        private bool _swipeRight;
        private bool _swipeUp;
        private bool _swipeDown;
        #endregion

        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);            
            SetupControl();
        }

        private void LateUpdate()
        {
            ResetInputs();
        }
        /// <summary>
        /// Resets every bool input property to false
        /// </summary>
        private void ResetInputs()
        {
            _tap = _swipeLeft = _swipeRight = _swipeUp = _swipeDown = false;
        }
        //Subscribes to the actions in the action scheme of the input manager
        private void SetupControl()
        {
            actionScheme = new RunnerInputAction();

            //Register different actions
            actionScheme.Gameplay.Tap.performed += ctx => OnTap(ctx);
            actionScheme.Gameplay.TouchPosition.performed += ctx => OnPosition(ctx);
            actionScheme.Gameplay.StartDrag.performed += ctx => OnStartDrag(ctx);
            actionScheme.Gameplay.EndDrag.performed += ctx => OnEndDrag(ctx);
        }
        /// <summary>
        /// Sets _tap to true when the screen is taped 
        /// </summary>
        /// <param name="ctx">Context of the input action (possible input values)</param>
        private void OnTap(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
        {
            _tap = true;
        }
        /// <summary>
        /// Reads the position of the screen where it has been taped
        /// </summary>
        /// <param name="ctx">Context of the input action (possible input values)</param>
        private void OnPosition(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
        {
            _touchPosition = ctx.ReadValue<Vector2>();
        }
        /// <summary>
        /// Saves the position where the drag started
        /// </summary>
        /// <param name="ctx">Context of the input action (possible input values)</param>
        private void OnStartDrag(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
        {
            _startDrag = _touchPosition;
        }
        /// <summary>
        /// Compares the position where the drag ended with the position where the drag started to establish
        /// if it was unintended (sqrDistance less than sqrSwipeDeadZone) or if it was a swipe, and the direction
        /// of said swipe
        /// </summary>
        /// <param name="ctx">Context of the input action (possible input values)</param>
        private void OnEndDrag(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
        {
            Vector2 delta = _touchPosition - _startDrag;
            float sqrDistance = delta.sqrMagnitude;
            //Confirmed swipe
            if(sqrDistance > sqrSwipeDeadZone)
            {
                float x = Mathf.Abs(delta.x);
                float y = Mathf.Abs(delta.y);
            
                if(x > y) //x > y -> left/right
                {
                    if(delta.x > 0)
                    {
                        _swipeRight = true;
                    }
                    else
                    {
                        _swipeLeft = true;
                    }
                }
                else //y > x -> up/down
                {
                    if (delta.y > 0)
                    {
                        _swipeUp = true;
                    }
                    else
                    {
                        _swipeDown = true;
                    }
                }
            }

            _startDrag = Vector2.zero;
        }
        
        public void OnEnable()
        {
            actionScheme.Enable();
        }

        public void OnDisable()
        {
            actionScheme.Disable();
        }

    }
}
