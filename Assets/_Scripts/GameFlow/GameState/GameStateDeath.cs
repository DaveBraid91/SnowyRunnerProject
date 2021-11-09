using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStateDeath : GameState
{
    public GameObject deathCanvas;
    [SerializeField] private TMP_Text highscoreText, scoreText, totalCollectablesText, collectablesText;

    //Completion circle fields
    [SerializeField] private Image completionCircle;
    [SerializeField] private float timeToDecide = 5.0f;
    private float deathTime;

    public override void Construct()
    {
        GameManager.Instance.motor.PausePlayer();

        //Prior to saving, set the highscore if needed
        if (SaveManager.Instance.saveState.Highscore < (int)GameStats.Instance.score)
        {
            scoreText.color = Color.yellow;
            SaveManager.Instance.saveState.Highscore = (int)GameStats.Instance.score;
        }

        SaveManager.Instance.saveState.TotalCollectables += GameStats.Instance.collectedThisRound;

        SaveManager.Instance.Save();

        highscoreText.text = "Highscore: " + SaveManager.Instance.saveState.Highscore.ToString();
        scoreText.text = GameStats.Instance.ScoreToText();
        totalCollectablesText.text = "Total: " + SaveManager.Instance.saveState.TotalCollectables.ToString();
        collectablesText.text = GameStats.Instance.CollectablesToText(); ;

        deathTime = Time.time;

        completionCircle.gameObject.SetActive(true);

        deathCanvas.SetActive(true);
    }

    public override void UpdateState()
    {
        float ratio = (Time.time - deathTime) / timeToDecide;
        completionCircle.color = Color.Lerp(Color.green, Color.red, ratio);
        completionCircle.fillAmount = 1 - ratio;

        if (ratio > 1)
            completionCircle.gameObject.SetActive(false);
    }

    public override void Destruct()
    {
        deathCanvas.SetActive(false);
    }

    public void ToMenu()
    {
        gameManager.ChangeState(GetComponent<GameStateInit>());

        GameManager.Instance.motor.ResetPlayer();
        GameManager.Instance.worldGeneration.ResetWorld();
        GameManager.Instance.sceneChunkGeneration.ResetWorld();

    }

    public void ResumeGame()
    {
        gameManager.ChangeState(GetComponent<GameStateGame>());
        GameManager.Instance.motor.RespawnPlayer();
    }
}
