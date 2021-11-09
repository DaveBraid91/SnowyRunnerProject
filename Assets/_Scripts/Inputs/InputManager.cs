using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //There should be only one InputManager in the scene
    private static InputManager instance;
    public static InputManager Instance { get { return instance; } }

    //Action schemes
    private RunnerInputAction actionScheme;

    //Configuration
    [SerializeField] private float sqrSwipeDeadZone;

    #region Public Properties
    public bool Tap 
    { 
        get 
        {
            tapCount++;
            return tap; 
        } 
    }
    public Vector2 TouchPosition { get { return touchPosition; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }
    #endregion

    #region Private Properties
    private int tapCount = 0;
    private bool tap;
    private Vector2 touchPosition;
    private Vector2 startDrag;
    private bool swipeLeft;
    private bool swipeRight;
    private bool swipeUp;
    private bool swipeDown;
    #endregion

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        SetupControl();
    }

    private void LateUpdate()
    {
        ResetInputs();
    }

    private void ResetInputs()
    {
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;
    }

    private void SetupControl()
    {
        actionScheme = new RunnerInputAction();

        //Register different actions
        actionScheme.Gameplay.Tap.performed += ctx => OnTap(ctx);
        actionScheme.Gameplay.TouchPosition.performed += ctx => OnPosition(ctx);
        actionScheme.Gameplay.StartDrag.performed += ctx => OnStartDrag(ctx);
        actionScheme.Gameplay.EndDrag.performed += ctx => OnEndDrag(ctx);
    }

    private void OnTap(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        tap = true;
    }
    private void OnPosition(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        touchPosition = ctx.ReadValue<Vector2>();
    }
    private void OnStartDrag(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        startDrag = touchPosition;
    }
    private void OnEndDrag(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        Vector2 delta = touchPosition - startDrag;
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
                    swipeRight = true;
                }
                else
                {
                    swipeLeft = true;
                }
            }
            else //y > x -> up/down
            {
                if (delta.y > 0)
                {
                    swipeUp = true;
                }
                else
                {
                    swipeDown = true;
                }
            }
        }

        startDrag = Vector2.zero;
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
