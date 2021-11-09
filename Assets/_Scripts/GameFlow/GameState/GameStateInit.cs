using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStateInit : GameState
{
    public GameObject menuCanvas;
    [SerializeField] TMP_Text highScoreText;
    [SerializeField] TMP_Text collectablesScoreText;


    public override void Construct()
    {
        GameManager.Instance.ChangeCamera(CameraState.Init);

        highScoreText.text = "Highscore: " + SaveManager.Instance.saveState.Highscore.ToString(); ;
        collectablesScoreText.text = "Fish: " + SaveManager.Instance.saveState.TotalCollectables.ToString(); ;

        menuCanvas.SetActive(true);
    }

    public override void Destruct()
    {
        menuCanvas.SetActive(false);
    }

    public void OnPlayClick()
    {
        gameManager.ChangeState(GetComponent<GameStateGame>());
        GameStats.Instance.ResetSession();
    }

    public void OnShopClick()
    {
        //gameManager.ChangeState(GetComponent<GameStateShop>());
        Debug.Log("Shop button has been clicked.");
    }
}
