using _Scripts.Save;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

namespace _Scripts.GameFlow.GameState
{
    public class GameStateDeath : GameState, IUnityAdsShowListener
    {
        public GameObject deathCanvas;
        [SerializeField] private TMP_Text highscoreText, scoreText, totalCollectablesText, collectablesText;

        //Completion circle fields
        [SerializeField] private Image completionCircle;
        [SerializeField] private float timeToDecide = 5.0f;
        private float _deathTime;

        public override void Construct()
        {
            GameManager.Instance.motor.PausePlayer();

            //Prior to saving, set the highscore if needed
            if (SaveManager.Instance.saveState.Highscore < (int)GameStats.Instance.score)
            {
                scoreText.color = Color.yellow;
                SaveManager.Instance.saveState.Highscore = (int)GameStats.Instance.score;
            }
            
            //Add the collected collectables to the total
            SaveManager.Instance.saveState.TotalCollectables += GameStats.Instance.collectedThisRound;
            
            //Save the game stats in the serialized document
            SaveManager.Instance.Save();
            
            //Set the UI texts of the death screen
            highscoreText.text = $"Highscore: {SaveManager.Instance.saveState.Highscore}";
            scoreText.text = GameStats.Instance.ScoreToText();
            totalCollectablesText.text = $"Total: {SaveManager.Instance.saveState.TotalCollectables}";
            collectablesText.text = GameStats.Instance.CollectablesToText(); ;

            //Set the time when the player died for the "Keep Running" option
            _deathTime = Time.time;

            //Activate de death UI
            deathCanvas.SetActive(true);
        }

        public override void UpdateState()
        {
            //Circle filling with change of color effect
            var ratio = (Time.time - _deathTime) / timeToDecide;
            completionCircle.color = Color.Lerp(Color.green, Color.red, ratio);
            completionCircle.fillAmount = 1 - ratio;
            
            //When the circle is complete, the "Keep Running" option deactivates
            if (ratio > 1)
                completionCircle.gameObject.SetActive(false);
        }

        public override void Destruct()
        {
            //Disable the death UI
            deathCanvas.SetActive(false);
        }

        /// <summary>
        /// Changes the state to the initial/main menu state and resets the game environment
        /// </summary>
        public void ToMenu()
        {
            gameManager.ChangeState(GetComponent<GameStateInit>());

            GameManager.Instance.motor.ResetPlayer();
            GameManager.Instance.worldGeneration.ResetWorld();
            GameManager.Instance.sceneChunkGeneration.ResetWorld();

        }

        /// <summary>
        /// Tries to run an ad, if succeeded, it resumes the game, if not, it goes back to the main menu
        /// </summary>
        public void TryResumeGame()
        {
            AdManager.Instance.ShowRewardedAdd();
        }

        /// <summary>
        /// Resume the game by respawning the player and going back to the game state
        /// </summary>
        public void ResumeGame()
        {
            gameManager.ChangeState(GetComponent<GameStateGame>());
            GameManager.Instance.motor.RespawnPlayer();
        }

        /// <summary>
        /// Enables the "Keep Running" Button int the death UI
        /// </summary>
        public void EnableRevive()
        {
            completionCircle.gameObject.SetActive(true);
        }

        #region IUnityAdsShowListener_Implementation
        public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
        {
            Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
            // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
        }

        public void OnUnityAdsShowStart(string adUnitId) { }
        public void OnUnityAdsShowClick(string adUnitId) { }
        public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState) 
        {
            completionCircle.gameObject.SetActive(false);
            switch (showCompletionState)
            {
                case UnityAdsShowCompletionState.COMPLETED:
                    ResumeGame();
                    break;

                case UnityAdsShowCompletionState.UNKNOWN:
                    ToMenu();
                    break;
            }
        }
        #endregion
    }
}
