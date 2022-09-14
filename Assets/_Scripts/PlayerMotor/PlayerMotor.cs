using System;
using _Scripts.GameFlow;
using _Scripts.PlayerMotor.State;
using UnityEngine;

namespace _Scripts.PlayerMotor
{
    public class PlayerMotor : MonoBehaviour
    {
        [HideInInspector] public Vector3 moveVector;
        [HideInInspector] public float verticalVelocity;
        [HideInInspector] public bool isGrounded;
        [HideInInspector] public int currentLane; // -1 0 1

        public float distanceBetweenLanes = 3.0f;
        public float baseRunSpeed = 5.0f;
        public float baseSidewaySpeed = 10.0f;
        public float gravity = 14.0f;
        public float terminalVelocity = 20.0f;

        public CharacterController controller;
        public Animator anim;

        private BaseState state;
        private bool isPaused;

        private void Start()
        {
            controller = GetComponent<CharacterController>();
            anim = GetComponent<Animator>();
            state = GetComponent<RunningState>();
            state.Construct();

            isPaused = true;
        }

        private void Update()
        {
            if(!isPaused)
                UpdateMotor();
        }

        private void UpdateMotor()
        {
            //Check if we're grounded
            isGrounded = controller.isGrounded;

            // How should we be moving? based on state
            moveVector = state.ProcessMotion();
            //Trying to change state?
            state.Transition();

            //Feed the animator with some values
            anim.SetBool("IsGrounded", isGrounded);
            anim.SetFloat("Speed", Mathf.Abs(moveVector.z));

            //Move the player
            controller.Move(moveVector * Time.deltaTime);
        }

        /// <summary>
        /// Moves the player to a lane until it is close enough to snap him to it
        /// </summary>
        /// <returns>The speed of said movement</returns>
        public float SnapToLane()
        {
            float returnValue = 0.0f;

            float tolerance = 0.05f;
            if(Math.Abs(transform.position.x - (currentLane * distanceBetweenLanes)) > tolerance) //We're not on top of a lane
            {
                float deltaToDesiredPosition = (currentLane * distanceBetweenLanes) - transform.position.x;
                returnValue = (deltaToDesiredPosition > 0) ? 1 : -1;
                returnValue *= baseSidewaySpeed;

                float actualDistanceTravel = returnValue * Time.deltaTime;
                if(Mathf.Abs(actualDistanceTravel) > Mathf.Abs(deltaToDesiredPosition))
                {
                    returnValue = deltaToDesiredPosition * (1 / Time.deltaTime);
                }
            }
            else
            {
                return returnValue;
            }

            return returnValue;
        }

        /// <summary>
        /// Changes the lane
        /// </summary>
        /// <param name="direction">Positive = right, negative = left</param>
        public void ChangeLane(int direction)
        {
            currentLane = Mathf.Clamp(currentLane + direction, -1, 1);
        }
        /// <summary>
        /// Transition between player states, destroying the old one and constructing the new one
        /// </summary>
        /// <param name="state">New player state</param>
        public void ChangeState(BaseState state)
        {
            this.state.Destruct();
            this.state = state;
            this.state.Construct();
        }
        /// <summary>
        /// Applies the custom gravity set on the editor
        /// </summary>
        public void ApplyGravity()
        {
            verticalVelocity -= gravity * Time.deltaTime;
            if (verticalVelocity < -terminalVelocity)
                verticalVelocity = -terminalVelocity;
        }
        /// <summary>
        /// Pauses the player, so it cannot move
        /// </summary>
        public void PausePlayer()
        {
            isPaused = true;
        }
        /// <summary>
        /// Enables the player, so it can move
        /// </summary>
        public void ResumePlayer()
        {
            isPaused = false;
        }
        /// <summary>
        /// Respawns the player
        /// </summary>
        public void RespawnPlayer()
        {
            Debug.Log("Respawning");
            ChangeState(GetComponent<RespawnState>());
            GameManager.Instance.ChangeCamera(CameraState.Respawn);
        }
        /// <summary>
        /// Resets the player to its main menu state
        /// </summary>
        public void ResetPlayer()
        {
            transform.position = Vector3.zero;
            currentLane = 0;
            anim?.SetTrigger("Idle");
            ChangeState(GetComponent<RunningState>());
            PausePlayer();
        }
        
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            //When the player hits a death collider it changes its state to death
            string hitLayerName = LayerMask.LayerToName(hit.gameObject.layer);

            if (hitLayerName.Equals("Death"))
                ChangeState(GetComponent<DeathState>());
        }
    }
}
