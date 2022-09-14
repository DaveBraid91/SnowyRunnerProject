using _Scripts.Save;
using TMPro;
using UnityEngine;

namespace _Scripts.GameFlow.GameState
{
    public class GameStateInit : GameState
    {
        public GameObject menuCanvas;
        [SerializeField] TMP_Text highScoreText;
        [SerializeField] TMP_Text collectablesScoreText;


        public override void Construct()
        {
            GameManager.Instance.ChangeCamera(CameraState.Init);

            highScoreText.text = $"Highscore: {SaveManager.Instance.saveState.Highscore}"; ;
            collectablesScoreText.text = $"Fish: {SaveManager.Instance.saveState.TotalCollectables}"; ;

            menuCanvas.SetActive(true);
        }

        public override void Destruct()
        {
            menuCanvas.SetActive(false);
        }

        /// <summary>
        /// When the button "Play" is clicked a new game is started and goes to the game state
        /// </summary>
        public void OnPlayClick()
        {
            gameManager.ChangeState(GetComponent<GameStateGame>());
            GameStats.Instance.ResetSession();
            GetComponent<GameStateDeath>().EnableRevive();
        }

        /// <summary>
        /// Go to the shop state
        /// </summary>
        public void OnShopClick()
        {
            gameManager.ChangeState(GetComponent<GameStateShop>());
            Debug.Log("Shop button has been clicked.");
        }
    }
}
