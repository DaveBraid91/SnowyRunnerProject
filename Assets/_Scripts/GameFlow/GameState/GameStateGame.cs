using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStateGame : GameState
{
    public GameObject gameCanvas;
    [SerializeField] TMP_Text fishText;
    [SerializeField] TMP_Text scoreText;

    public override void Construct()
    {
        GameManager.Instance.motor.ResumePlayer();
        GameManager.Instance.ChangeCamera(CameraState.Game);

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

        GameStats.Instance.OnScoreChange -= OnScoreChange;
        GameStats.Instance.OnCollectableChange -= OnCollectCollectable;
    }

    private void OnScoreChange(float score)
    {
        scoreText.text = GameStats.Instance.ScoreToText();
    }

    private void OnCollectCollectable(int amountCollected)
    {
        fishText.text = GameStats.Instance.CollectablesToText(); ;
    }
}
