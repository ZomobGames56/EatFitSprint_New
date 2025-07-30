using UnityEngine;
using UnityEngine.Advertisements;

public class _AdsInit : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId= "5564803";
    [SerializeField] string _iOSGameId= "5564802";
    [SerializeField] bool _testMode = true;
    private string _gameId;

    void Awake()
    {
        InitializeAds();
        // DontDestroyOnLoad(this.gameObject);
    }

    public void InitializeAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSGameId 
            : _androidGameId;
        #region commented code.
        //_gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
        //    ? _iOSGameId
        //    : _androidGameId;
        //var platform = RuntimePlatform.IPhonePlayer;
        //if (platform == RuntimePlatform.Android)
        //{
        //    _gameId = _androidGameId;
        //}
        //else if (platform == RuntimePlatform.IPhonePlayer)
        //{
        //    _gameId = _iOSGameId;
        //}
        //else
        //{
        //    Debug.Log("not valid");

        //}
        //Debug.Log(platform);
        #endregion
        Advertisement.Initialize(_gameId, _testMode, this);

    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete." +_gameId);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}