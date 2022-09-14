using _Scripts.Inputs;
using UnityEngine;

namespace _Scripts.PlayerMotor.State
{
    public class SlidingState : BaseState
    {
        public float slideDuration = 1.0f;

        //Collider logic
        private Vector3 initialCenter;
        private float initialSize;
        private float slideStart;

        public override void Construct()
        {
            //Trigger the "Slide" animation
            motor.anim?.SetTrigger("Slide");
            //Sets the time where the slide begins to track its duration
            slideStart = Time.time;
            //Changes the colliders size to match the slide looks and mechanic
            initialSize = motor.controller.height;
            initialCenter = motor.controller.center;

            motor.controller.height = initialSize * 0.5f;
            motor.controller.center = initialCenter * 0.5f;
        }

        public override void Destruct()
        {
            //Sets the colliders size to default
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
            //Slide canceling is possible by jumping or falling
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
            //Same movement as when running
            Vector3 movement = Vector3.zero;

            movement = new Vector3(motor.SnapToLane(), -1, motor.baseRunSpeed);

            return movement;
        }
    }
}
