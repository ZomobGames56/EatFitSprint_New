using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class interstitialUnity : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static interstitialUnity instance;
    [SerializeField] string _androidAdUnitId = "Interstitial_Android";
    [SerializeField] string _iOsAdUnitId = "Interstitial_iOS";
    
    string _adUnitId;
    //int i = 0;

    void Awake()
    {
        instance = this;
        // Get the Ad Unit ID for the current platform:
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOsAdUnitId
            : _androidAdUnitId;
        LoadAd();
    }

    // Load content to the Ad Unit:
    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        //Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    // Show the loaded content in the Ad Unit:
    public void ShowAd()
    {
        // Note that if the ad content wasn't previously loaded, this method will fail
        //Debug.Log("Showing Ad: " + _adUnitId);
        Advertisement.Show(_adUnitId, this);
        
    }

    // Implement Load Listener and Show Listener interface methods: 
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        // Optionally execute code if the Ad Unit successfully loads content.
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
    }

    public void OnUnityAdsShowStart(string adUnitId) 
    {
       
    }
    public void OnUnityAdsShowClick(string adUnitId) { }
    public void rewardedad()
    {
       
    }
    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showResult) {
        if (!PlayerPrefs.HasKey("first"))
        {
           // GameManager.instance.sendadjusttoken("oon4uy");
          //  PlayerPrefs.SetInt("first", 1);
        }
       // GameManager.instance.sendadjusttoken("6y6cp3");

    }
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        
    }
}
