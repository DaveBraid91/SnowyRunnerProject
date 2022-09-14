using UnityEngine;

namespace _Scripts.PlayerMotor.State
{
    public abstract class BaseState : MonoBehaviour
    {
        protected PlayerMotor motor;

        private void Awake()
        {
            motor = GetComponent<PlayerMotor>();
        }
        /// <summary>
        /// When a state is called, the logic to construct that state will be run here
        /// </summary>
        public virtual void Construct() { }
        /// <summary>
        /// When a state is called, the logic to destruct and exit the previous state will be run here
        /// </summary>
        public virtual void Destruct() { }
        /// <summary>
        /// When a state is running, the transition logic possibilities to other states will be here
        /// </summary>
        public virtual void Transition() { }
        
        /// <summary>
        /// The motion of the player will be processed here
        /// </summary>
        /// <returns>The resulting motion</returns>
        public virtual Vector3 ProcessMotion()
        {
            Debug.Log($"Process motion is not implemented in {this}");
            return Vector3.zero;
        }
    }
}
