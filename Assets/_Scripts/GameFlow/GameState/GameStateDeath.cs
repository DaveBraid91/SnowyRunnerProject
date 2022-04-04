using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class GameStateDeath : GameState, IUnityAdsShowListener
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

    public void TryResumeGame()
    {
        AdManager.Instance.ShowRewardedAdd();
    }

    public void ResumeGame()
    {
        gameManager.ChangeState(GetComponent<GameStateGame>());
        GameManager.Instance.motor.RespawnPlayer();
    }

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
