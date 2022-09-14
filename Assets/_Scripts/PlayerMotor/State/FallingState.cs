using UnityEngine;

namespace _Scripts.PlayerMotor.State
{
    public class FallingState : BaseState
    {
        public override void Construct()
        {
            //Trigger the "Fall" animation
            motor.anim?.SetTrigger("Fall");
        }

        public override Vector3 ProcessMotion()
        {
            // Apply gravity
            motor.ApplyGravity();

            //Create return Vector
            Vector3 movement = Vector3.zero;
            //It can't swap lanes while falling
            movement = new Vector3(motor.SnapToLane(), motor.verticalVelocity, motor.baseRunSpeed);

            return movement;
        }

        public override void Transition()
        {
            //When it touches the ground it goes back to the Running state
            if (motor.isGrounded)
                motor.ChangeState(GetComponent<RunningState>());
        }
    }
}
