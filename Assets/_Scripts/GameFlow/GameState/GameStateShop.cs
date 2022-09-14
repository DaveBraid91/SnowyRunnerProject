using _Scripts.Save;
using _Scripts.Shop;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

namespace _Scripts.GameFlow.GameState
{
    public class GameStateShop : GameState
    {
        public GameObject shopCanvas;
        [SerializeField] private TMP_Text collectablesScoreText;
        [SerializeField] private TMP_Text currentHatName;
        [SerializeField] private HatLogic hatLogic;
        private int _unlockedHats;

        [Header("Shop Items")]
        public GameObject hatPrefab;
        public Transform hatContainer;
        [SerializeField]
        private Hat[] hats;
        
        [Header("Completion Circle")] //The circle fills depending on the percentage of items purchased by the player
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

        /// <summary>
        /// Initialized the UI of the list of hats,
        /// depending on the information in "hats", which are ScriptableObjects
        /// To fill in a new hat go to "Resources/Hats" and add a "Hat" ScriptableObject
        /// </summary>
        private void InitializeShop()
        {
            for(int i = 0; i < hats.Length; i++)
            {
                var index = i;
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
                    _unlockedHats++;
                }
                else
                {
                    hatInstance.transform.GetChild(2).GetComponent<TMP_Text>().text = hats[index].ItemPrice.ToString();
                }
            
            }
        }
        
        /// <summary>
        /// Changes the current hat and saves it, so it is still on when the player closes and opens the game
        /// </summary>
        /// <param name="i">The hat index the player clicks on</param>
        private void ChangeHat(int i)
        {
            SaveManager.Instance.saveState.CurrentHatIndex = i;
            UpdateCurrentHatName(i);
            hatLogic.SelectHat(i);
            SaveManager.Instance.Save();
        }
        /// <summary>
        /// Updates the number of collectables in the shop UI
        /// </summary>
        private void UpdateCollectablesScoreText()
        {
            collectablesScoreText.text = SaveManager.Instance.saveState.TotalCollectables.ToString();
        }
        /// <summary>
        /// Updates the name of the hat in the shop UI
        /// </summary>
        /// <param name="i">The hat index the player clicks on</param>
        private void UpdateCurrentHatName(int i)
        {
            currentHatName.text = hats[i].ItemName;
        }
        /// <summary>
        /// Updates the amount of filling of the completion circle
        /// </summary>
        private void UpdateCompletionCircle()
        {
            var hatCount = hats.Length - 1;
            var currentlyUnlockedHatsCount = _unlockedHats - 1;
            completionCircleImage.fillAmount = (float)currentlyUnlockedHatsCount / (float)hatCount;
            completionTMP.text = $"{currentlyUnlockedHatsCount}/{hatCount}";
        }
        /// <summary>
        /// When the player clicks on a hat, he changes or purchases it, depending on his collectable score and
        /// if he has already unlocked it.
        /// </summary>
        /// <param name="i">The hat index the player clicks on</param>
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
                _unlockedHats++;
                UpdateCollectablesScoreText();
                UpdateCompletionCircle();
                hatContainer.GetChild(i).transform.GetChild(2).GetComponent<TMP_Text>().text = "";
                ChangeHat(i);

            }
            else
            {
                Debug.Log("Not enough fish");
                //TODO: Display the lack of fish in the UI
            }
        
        }
        /// <summary>
        /// Goes back to the main menu
        /// </summary>
        public void OnHomeClick()
        {
            gameManager.ChangeState(GetComponent<GameStateInit>());
        }
    }
}
