using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using System.Collections;

public class RewardedAdWithCooldown : MonoBehaviour
{
    public Button rewardButton;
    public float cooldownTime = 60f; // Cooldown in seconds

#if UNITY_ANDROID
    private string adUnitId = "ca-app-pub-3940256099942544/5224354917"; // test ad
#elif UNITY_IPHONE
    private string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
    private string adUnitId = "unused";
#endif

    private RewardedAd rewardedAd;
    private bool isCooldown = false;

    private void Start()
    {
        rewardButton.onClick.AddListener(ShowRewardedAd);
        rewardButton.interactable = false;
        LoadRewardedAd();
    }

    private void LoadRewardedAd()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        var adRequest = new AdRequest();

        RewardedAd.Load(adUnitId, adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.LogError("Failed to load rewarded ad: " + error);
                return;
            }

            rewardedAd = ad;
            rewardedAd.OnAdFullScreenContentClosed += () =>
            {
                Debug.Log("Rewarded ad closed.");
                // Optional: Reload the ad or re-enable button here
                HandleAdClosed(ad);
            };

            rewardedAd.OnAdFullScreenContentFailed += (AdError adError) =>
            {
                Debug.LogError("Rewarded ad failed to show: " + adError.GetMessage());
                HandleAdFailed(ad,error);
            };


           

            // Enable button only if not in cooldown
            if (!isCooldown)
            {
                rewardButton.interactable = true;
            }

            Debug.Log("Rewarded ad loaded.");
        });
    }

    private void ShowRewardedAd()
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                Debug.Log($"User rewarded with: {reward.Type}, amount: {reward.Amount}");
                // Give reward here
            });

            StartCoroutine(StartCooldown());
        }
    }

    private void HandleAdClosed(RewardedAd ad)
    {
        LoadRewardedAd(); // Preload next ad
    }

    private void HandleAdFailed(RewardedAd ad, AdError error)
    {
        Debug.LogWarning("Ad failed to show: " + error);
        LoadRewardedAd(); // Try reloading
    }

    private IEnumerator StartCooldown()
    {
        isCooldown = true;
        rewardButton.interactable = false;

        float elapsed = 0f;
        while (elapsed < cooldownTime)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        isCooldown = false;

        // Only enable button if ad is ready again
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardButton.interactable = true;
        }
    }
}
