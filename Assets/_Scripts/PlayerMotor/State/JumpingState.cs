using UnityEngine;

namespace _Scripts.PlayerMotor.State
{
    public class JumpingState : BaseState
    {
        public float jumpForce = 7.0f;

        public override void Construct()
        {
            //Trigger the "Jump" animation
            motor.anim?.SetTrigger("Jump");
            //Gives the body an instant vertical force, ergo the jump
            motor.verticalVelocity = jumpForce;
        }

        public override Vector3 ProcessMotion()
        {
            // Apply gravity
            motor.ApplyGravity();

            //Create return Vector
            Vector3 movement = Vector3.zero;
            //It can't swap lanes during the jump
            movement = new Vector3(motor.SnapToLane(), motor.verticalVelocity, motor.baseRunSpeed);

            return movement;
        }

        public override void Transition()
        {
            //It changes to the falling sate when it vertical velocity is negative
            if (motor.verticalVelocity < 0)
                motor.ChangeState(GetComponent<FallingState>());
        }
    }
}
