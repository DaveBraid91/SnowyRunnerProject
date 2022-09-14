using _Scripts.GameFlow.GameState;
using UnityEngine;
using UnityEngine.Advertisements;

namespace _Scripts
{
    public class AdManager : MonoBehaviour, IUnityAdsInitializationListener
    {
        public static AdManager Instance { get { return instance; } }
        private static AdManager instance;

        [SerializeField] string _androidGameId;
        [SerializeField] string _iOSGameId;
        [SerializeField] bool _testMode = true;
        private string _gameId;
        [SerializeField] string _androidrewardedVideoPlacementId;
        [SerializeField] string _iOSrewardedVideoPlacementId;
        private string _rewardedVideoPlacementId;

        private void Awake()
        {
            instance = this;
            InitializeAds();
        }

        /// <summary>
        /// Initializes the ads depending on the RunTimePlatform
        /// </summary>
        public void InitializeAds()
        {
            _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
                ? _iOSGameId
                : _androidGameId;
            Advertisement.Initialize(_gameId, _testMode, this);

            _rewardedVideoPlacementId = (Application.platform == RuntimePlatform.IPhonePlayer)
                ? _iOSrewardedVideoPlacementId
                : _androidrewardedVideoPlacementId;
        }

        /// <summary>
        /// Shows a rewarded add and informs the GameStateDeath
        /// </summary>
        public void ShowRewardedAdd()
        {
            Advertisement.Show(_rewardedVideoPlacementId, GetComponent<GameStateDeath>());
        }

        /// <summary>
        /// Shows a message when the initialization is complete
        /// </summary>
        public void OnInitializationComplete()
        {
            Debug.Log("Unity Ads initialization complete.");
        }

        /// <summary>
        /// Shows a message when the initialization failed
        /// </summary>
        /// <param name="error">Error that caused the failure</param>
        /// <param name="message">Message of the error</param>
        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        }
    }
}
