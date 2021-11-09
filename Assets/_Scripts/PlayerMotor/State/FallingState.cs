using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : BaseState
{
    public override void Construct()
    {
        motor.anim?.SetTrigger("Fall");
    }

    public override Vector3 ProcessMotion()
    {
        // Apply gravity
        motor.ApplyGravity();

        //Create return Vector
        Vector3 movement = Vector3.zero;

        movement = new Vector3(motor.SnapToLane(), motor.verticalVelocity, motor.baseRunSpeed);

        return movement;
    }

    public override void Transition()
    {
        if (motor.isGrounded)
            motor.ChangeState(GetComponent<RunningState>());
    }
}
