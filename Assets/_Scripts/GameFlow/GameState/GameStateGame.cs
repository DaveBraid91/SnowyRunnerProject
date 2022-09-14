using TMPro;
using UnityEngine;

namespace _Scripts.GameFlow.GameState
{
    public class GameStateGame : GameState
    {
        public GameObject gameCanvas;
        [SerializeField] private TMP_Text fishText;
        [SerializeField] private TMP_Text scoreText;

        public override void Construct()
        {
            //Let the player move and changes the camera to the game camera
            GameManager.Instance.motor.ResumePlayer();
            GameManager.Instance.ChangeCamera(CameraState.Game);
            //Subscription to Actions
            GameStats.Instance.OnScoreChange += OnScoreChange; //(s) => { scoreText.text = s.ToString(); };
            GameStats.Instance.OnCollectableChange += OnCollectCollectable;

            gameCanvas.SetActive(true);
        }

    
        public override void UpdateState()
        {
            GameManager.Instance.worldGeneration.ScanPosition();
            GameManager.Instance.sceneChunkGeneration.ScanPosition();
        }

        public override void Destruct()
        {
            gameCanvas.SetActive(false);
            //desubscription to Actions
            GameStats.Instance.OnScoreChange -= OnScoreChange;
            GameStats.Instance.OnCollectableChange -= OnCollectCollectable;
        }
        
        /// <summary>
        /// Updates the score text in the UI
        /// </summary>
        /// <param name="score">new score</param>
        private void OnScoreChange(float score)
        {
            scoreText.text = GameStats.Instance.ScoreToText();
        }

        /// <summary>
        /// Updates the collectibles text in the UI
        /// </summary>
        /// <param name="amountCollected">new amount collected</param>
        private void OnCollectCollectable(int amountCollected)
        {
            fishText.text = GameStats.Instance.CollectablesToText(); ;
        }
    }
}
