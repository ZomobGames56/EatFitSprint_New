using GoogleMobileAds.Api;
using UnityEngine;

public class InitilizeAdMob : MonoBehaviour
{
    private void Start()
    {
        MobileAds.Initialize((InitializationStatus initstatus) =>
        {
            if (initstatus == null)
            {
                Debug.LogError("Google Mobile Ads initialization failed.");
                return;
            }
            Debug.Log("Google Mobile Ads initialization complete.");
        });
    }

    
}
