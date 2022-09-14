using _Scripts.Inputs;
using UnityEngine;

namespace _Scripts.PlayerMotor.State
{
    public class RunningState : BaseState
    {
        public override void Construct()
        {
            motor.verticalVelocity = 0.0f;
        }

        public override Vector3 ProcessMotion()
        {
            //It moves forward, left and right depending on which lane it is on
            Vector3 movement = Vector3.zero;

            movement = new Vector3(motor.SnapToLane(), -1, motor.baseRunSpeed);

            return movement;
        }

        public override void Transition()
        {
            //It can change to any of the moving states depending con the player's input or
            //if it is no longer grounded
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
}
