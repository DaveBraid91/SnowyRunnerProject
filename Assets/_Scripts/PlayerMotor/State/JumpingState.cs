using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : BaseState
{
    public float jumpForce = 7.0f;

    public override void Construct()
    {
        motor.anim?.SetTrigger("Jump");
        motor.verticalVelocity = jumpForce;
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
        if (motor.verticalVelocity < 0)
            motor.ChangeState(GetComponent<FallingState>());
    }
}
