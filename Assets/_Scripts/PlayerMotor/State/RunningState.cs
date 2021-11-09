using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : BaseState
{
    public override void Construct()
    {
        motor.verticalVelocity = 0.0f;
    }

    public override Vector3 ProcessMotion()
    {
        Vector3 movement = Vector3.zero;

        movement = new Vector3(motor.SnapToLane(), -1, motor.baseRunSpeed);

        return movement;
    }

    public override void Transition()
    {
        if(InputManager.Instance.SwipeLeft)
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
        if (InputManager.Instance.SwipeDown && motor.isGrounded)
        {
            motor.ChangeState(GetComponent<SlidingState>());
        }
        if (!motor.isGrounded)
        {
            motor.ChangeState(GetComponent<FallingState>());
        }
    }

}
