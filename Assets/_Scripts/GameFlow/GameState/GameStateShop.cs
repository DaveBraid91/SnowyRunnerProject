using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStateShop : GameState
{
    public GameObject shopCanvas;
    [SerializeField] private TMP_Text collectablesScoreText;
    [SerializeField] private TMP_Text currentHatName;
    [SerializeField] private HatLogic hatLogic;
    private int unlockedHats;

    [Header("Shop Items")]
    public GameObject hatPrefab;
    public Transform hatContainer;
    [SerializeField]
    private Hat[] hats;

    [Header("Completion Circle")] 
    public Image completionCircleImage;
    public TMP_Text completionTMP;
    protected override void Awake()
    {
        base.Awake();
        hats = Resources.LoadAll<Hat>("Hats/");
        InitializeShop();
        UpdateCurrentHatName(SaveManager.Instance.saveState.CurrentHatIndex);
        UpdateCompletionCircle();
    }

    public override void Construct()
    {
        GameManager.Instance.ChangeCamera(CameraState.Shop);

        UpdateCollectablesScoreText();

        shopCanvas.SetActive(true);
    }
    
    public override void Destruct()
    {
        shopCanvas.SetActive(false);
    }

    private void InitializeShop()
    {
        for(int i = 0; i < hats.Length; i++)
        {
            int index = i;
            GameObject hatInstance = Instantiate(hatPrefab, hatContainer) as GameObject;
            //Button
            hatInstance.GetComponent<Button>().onClick.AddListener( () => OnHatClick(index));
            //Thumbnail
            hatInstance.transform.GetChild(0).GetComponent<Image>().sprite = hats[index].Thumbnail;
            //ItemName
            hatInstance.transform.GetChild(1).GetComponent<TMP_Text>().text = hats[index].ItemName;
            //Price
            if(SaveManager.Instance.saveState.UnlockedHatFlag[i] == 1)
            {
                hatInstance.transform.GetChild(2).GetComponent<TMP_Text>().text = "";
                unlockedHats++;
            }
            else
            {
                hatInstance.transform.GetChild(2).GetComponent<TMP_Text>().text = hats[index].ItemPrice.ToString();
            }
            
        }
    }

    private void ChangeHat(int i)
    {
        SaveManager.Instance.saveState.CurrentHatIndex = i;
        UpdateCurrentHatName(i);
        hatLogic.SelectHat(i);
        SaveManager.Instance.Save();
    }

    private void UpdateCollectablesScoreText()
    {
        collectablesScoreText.text = SaveManager.Instance.saveState.TotalCollectables.ToString();
    }

    private void UpdateCurrentHatName(int i)
    {
        currentHatName.text = hats[i].ItemName;
    }

    private void UpdateCompletionCircle()
    {
        int hatCount = hats.Length - 1;
        int currentlyUnlockedHatsCount = unlockedHats - 1;
        completionCircleImage.fillAmount = (float)currentlyUnlockedHatsCount / (float)hatCount;
        completionTMP.text = currentlyUnlockedHatsCount + "/" + hatCount;
    }

    private void OnHatClick(int i)
    {
        if(SaveManager.Instance.saveState.UnlockedHatFlag[i] == 1)
        {
            ChangeHat(i);
        }
        else if(SaveManager.Instance.saveState.TotalCollectables >= hats[i].ItemPrice)
        {
            SaveManager.Instance.saveState.TotalCollectables -= hats[i].ItemPrice;
            SaveManager.Instance.saveState.UnlockedHatFlag[i] = 1;
            unlockedHats++;
            UpdateCollectablesScoreText();
            UpdateCompletionCircle();
            hatContainer.GetChild(i).transform.GetChild(2).GetComponent<TMP_Text>().text = "";
            ChangeHat(i);

        }
        else
        {
            Debug.Log("Not enough fish");
        }
        
    }

    public void OnHomeClick()
    {
        gameManager.ChangeState(GetComponent<GameStateInit>());
    }
}
