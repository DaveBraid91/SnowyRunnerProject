using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingState : BaseState
{
    public float slideDuration = 1.0f;

    //Collider logic
    private Vector3 initialCenter;
    private float initialSize;
    private float slideStart;

    public override void Construct()
    {
        motor.anim?.SetTrigger("Slide");
        slideStart = Time.time;

        initialSize = motor.controller.height;
        initialCenter = motor.controller.center;

        motor.controller.height = initialSize * 0.5f;
        motor.controller.center = initialCenter * 0.5f;
    }

    public override void Destruct()
    {
        motor.controller.height = initialSize;
        motor.controller.center = initialCenter;
    }

    public override void Transition()
    {
        //Changing lanes is possible while sliding.
        if (InputManager.Instance.SwipeLeft)
        {
            motor.ChangeLane(-1);
        }
        if (InputManager.Instance.SwipeRight)
        {
            motor.ChangeLane(1);
        }
        if (InputManager.Instance.SwipeUp && motor.isGrounded)
        {
            motor.ChangeState(GetComponent<JumpingState>());
        }

        if ((Time.time - slideStart) > slideDuration)
        {
            motor.ChangeState(GetComponent<RunningState>());
            motor.anim?.SetTrigger("Running");
        }
            
        if (!motor.isGrounded)
            motor.ChangeState(GetComponent<FallingState>());
    }

    public override Vector3 ProcessMotion()
    {
        Vector3 movement = Vector3.zero;

        movement = new Vector3(motor.SnapToLane(), -1, motor.baseRunSpeed);

        return movement;
    }
}
