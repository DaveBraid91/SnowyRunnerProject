using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnState : BaseState
{
    [SerializeField] private float verticalDistance = 25.0f;

    public override void Construct()
    {
        motor.controller.enabled = false;
        motor.transform.position = new Vector3(0, verticalDistance, motor.transform.position.z);
        motor.controller.enabled = true;
        motor.verticalVelocity = 0.0f;
        motor.currentLane = 0;
        motor.anim?.SetTrigger("Respawn");

        GameManager.Instance.ChangeCamera(CameraState.Respawn);
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
        if (InputManager.Instance.SwipeLeft)
        {
            motor.ChangeLane(-1);
        }
        if (InputManager.Instance.SwipeRight)
        {
            motor.ChangeLane(1);
        }

        if (motor.isGrounded && motor.verticalVelocity < -2.0f)
            motor.ChangeState(GetComponent<RunningState>());
    }

    public override void Destruct()
    {
        GameManager.Instance.ChangeCamera(CameraState.Game);
    }
}
