using _Scripts.GameFlow;
using _Scripts.GameFlow.GameState;
using UnityEngine;

namespace _Scripts.PlayerMotor.State
{
    public class DeathState : BaseState
    {
        [SerializeField] private Vector3 knockbackForce = new Vector3(0, 4, -3);
        [SerializeField] private float knockbackBrakeForce = 2.0f;
        private Vector3 _currentKnockback;

        public override void Construct()
        {
            //Trigger the "Death" animation
            motor.anim?.SetTrigger("Death");
            //Sets a knockback force
            _currentKnockback = knockbackForce;
        }

        public override Vector3 ProcessMotion()
        {
            //The model slows it's knockback down. When it stops it changes the GamesState to Death
            _currentKnockback = new Vector3(0, _currentKnockback.y -= motor.gravity * Time.deltaTime, _currentKnockback.z += knockbackBrakeForce * Time.deltaTime);

            if(_currentKnockback.z > 0)
            {
                _currentKnockback.z = 0;
                if (motor.isGrounded)
                    _currentKnockback.y = 0;
                GameManager.Instance.ChangeState(GameManager.Instance.GetComponent<GameStateDeath>());
            }
        

            return _currentKnockback;
        }
    }
}
