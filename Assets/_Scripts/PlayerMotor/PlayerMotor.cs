using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public float SnapToLane()
    {
        float returnValue = 0.0f;

        if(transform.position.x != (currentLane * distanceBetweenLanes)) //We're not on top of a lane
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

    public void ChangeLane(int direction)
    {
        currentLane = Mathf.Clamp(currentLane + direction, -1, 1);
    }

    public void ChangeState(BaseState state)
    {
        this.state.Destruct();
        this.state = state;
        this.state.Construct();
    }
    public void ApplyGravity()
    {
        verticalVelocity -= gravity * Time.deltaTime;
        if (verticalVelocity < -terminalVelocity)
            verticalVelocity = -terminalVelocity;
    }

    public void PausePlayer()
    {
        isPaused = true;
    }
    public void ResumePlayer()
    {
        isPaused = false;
    }

    public void RespawnPlayer()
    {
        Debug.Log("Respawning");
        ChangeState(GetComponent<RespawnState>());
        GameManager.Instance.ChangeCamera(CameraState.Respawn);
    }

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
        string hitLayerName = LayerMask.LayerToName(hit.gameObject.layer);

        if (hitLayerName.Equals("Death"))
            ChangeState(GetComponent<DeathState>());
    }
}
