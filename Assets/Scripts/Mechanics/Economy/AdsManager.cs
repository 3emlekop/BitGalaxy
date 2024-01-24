using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsShowListener, IUnityAdsLoadListener
{
    [SerializeField] private string androidGameId;
    [SerializeField] private string iosGameId;
    [SerializeField] private bool isTestMode;
    [SerializeField] private AudioManager audioManager;

    private string adId = "Reward";
    private Action onShowedAdAction;

    private void Awake()
    {
        string gameId = androidGameId;
        Advertisement.Initialize(gameId, isTestMode, this);
    }

    public void OnInitializationComplete()
    {
        Advertisement.Load(adId, this);
    }

    public void ShowRewardedAd()
    {
        Advertisement.Show(adId, this);
    }

    public void SetRewardAction(Action rewardAction)
    {
        onShowedAdAction = rewardAction;
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
    }
    public void OnUnityAdsShowStart(string placementId)
    {
        StartCoroutine(audioManager.CurrentAudioFadeOut(true));
    }
    public void OnUnityAdsFinish()
    {
        StartCoroutine(audioManager.UnpauseCurrentAudio());
    }
    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId == adId && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
            onShowedAdAction?.Invoke();
    }
    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.LogError($"PlacementId: {placementId} failed to show: {message}");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.LogError($"PlacementId: {placementId} failed to load: {message}");
        Advertisement.Load(adId, this);

    }
    public void OnInitializationFailed(UnityAdsInitializationError error, string msg)
    {
        Debug.LogError($"Error: {error}, message: {msg}");
    }
    public void OnUnityAdsShowClick(string placementId)
    {

    }
}
