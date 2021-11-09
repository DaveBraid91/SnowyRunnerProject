using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraState
{
    Init = 0,
    Game = 1,
    Shop = 2,
    Respawn = 3
}

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    public GameObject[] cameras;

    public PlayerMotor motor;
    public WorldGeneration worldGeneration;
    public SceneChunkGeneration sceneChunkGeneration;

    private GameState state;

    private void Awake()
    {
        instance = this;
        state = GetComponent<GameStateInit>();
        state.Construct();
    }

    private void Update()
    {
        state.UpdateState();
    }

    public void ChangeState(GameState state)
    {
        this.state.Destruct();
        this.state = state;
        this.state.Construct();
    }

    public void ChangeCamera(CameraState cameraState)
    {
        foreach(GameObject camera in  cameras)
        {
            camera.SetActive(false);
        }

        cameras[(int)cameraState].SetActive(true);
    }
}
