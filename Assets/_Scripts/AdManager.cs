using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

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

    public void ShowRewardedAdd()
    {
        Advertisement.Show(_rewardedVideoPlacementId, GetComponent<GameStateDeath>());
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}
