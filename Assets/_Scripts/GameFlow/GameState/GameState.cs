using UnityEngine;

namespace _Scripts.GameFlow.GameState
{
    /*Abstract class to establish the main structure of the game-flow states*/
    public abstract class GameState : MonoBehaviour
    {
        protected GameManager gameManager;

        protected virtual void Awake()
        {
            gameManager = GetComponent<GameManager>();
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
        /// When a state is running, the Update() logic will be run here
        /// </summary>
        public virtual void UpdateState() { }

    }
}
