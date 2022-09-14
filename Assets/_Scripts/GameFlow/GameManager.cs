using System.Collections;
using System.Collections.Generic;
using _Scripts.GameFlow.GameState;
using _Scripts.WorldGeneration;
using UnityEngine;

namespace _Scripts.GameFlow
{
    /// <summary>
    /// The camera states possible. Each one corresponds with a virtual CM camera.
    /// If more cameras are added to the game, include them here.
    /// </summary>
    public enum CameraState
    {
        Init = 0,
        Game = 1,
        Shop = 2,
        Respawn = 3
    }

    /// <summary>
    /// The GameManager manages the game flow via the game states. It also chooses which camera to use depending
    /// on the game state that is activated at the time.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager Instance { get { return _instance; } }
        public GameObject[] cameras;

        public PlayerMotor.PlayerMotor motor;
        public WorldGeneration.WorldGeneration worldGeneration;
        public SceneChunkGeneration sceneChunkGeneration;

        private GameState.GameState _state;

        private void Awake()
        {
            if(_instance == null)
                _instance = this;
            else
                Destroy(this.gameObject);
   
            _state = GetComponent<GameStateInit>();
            _state.Construct();
        }

        private void Update()
        {
            _state.UpdateState();
        }

        /// <summary>
        /// Transition between game states, destroying the old one and constructing the new one
        /// </summary>
        /// <param name="state">New game state</param>
        public void ChangeState(GameState.GameState state)
        {
            this._state.Destruct();
            this._state = state;
            this._state.Construct();
        }

        /// <summary>
        /// Transition between virtual cameras
        /// </summary>
        /// <param name="cameraState">new camera</param>
        public void ChangeCamera(CameraState cameraState)
        {
            foreach(var camera in cameras)
            {
                camera.SetActive(false);
            }

            cameras[(int)cameraState].SetActive(true);
        }
    }
}