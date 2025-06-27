using Google.Play.AppUpdate;
using Google.Play.Common;
using System.Collections;
using UnityEngine;

public class InAppUpdateManager : MonoBehaviour
{
    AppUpdateManager appUpdateManager;
    //bool update = false;
    [SerializeField]
    GameObject updatePanel;
    [SerializeField] private string playStoreUrlLink = "https://play.google.com/store/apps/details?id=com.playarenagamingstudio.Eatfitsprint&hl=en_IN";


    void Awake()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            this.appUpdateManager = new AppUpdateManager();
        }
        StartCoroutine(CheckForPlayStoreUpdate());
    }
    IEnumerator CheckForPlayStoreUpdate()
    {
        PlayAsyncOperation<AppUpdateInfo, AppUpdateErrorCode> appupdateInfoOperation =
        appUpdateManager.GetAppUpdateInfo();

        yield return appupdateInfoOperation;

        if (appupdateInfoOperation.IsSuccessful)
        {
            var appupdateInfoResult = appupdateInfoOperation.GetResult();
            //float size=appupdateInfoResult.TotalBytesToDownload * 1000000;
            if (appupdateInfoResult.UpdateAvailability == UpdateAvailability.UpdateAvailable)
            {
                Time.timeScale = 0.0f;
                updatePanel.SetActive(true);
            }
            else
            {
                Time.timeScale = 1.0f;
                updatePanel.SetActive(false);
            }
        }
    }
    public void UpdateButton()
    {
        Time.timeScale = 1.0f;
        Application.OpenURL(playStoreUrlLink);
    }
    public void NotNow()
    {
        Time.timeScale = 1.0f;
        updatePanel.SetActive(false);
        //or Application Quit.
    }

    //private AppUpdateManager updateManager;

    //void Start()
    //{
    //    updateManager = new AppUpdateManager();
    //    StartCoroutine(CheckUpdate());
    //}

    //IEnumerator CheckUpdate()
    //{
    //    var appUpdateInfoOperation = updateManager.GetAppUpdateInfo();
    //    yield return appUpdateInfoOperation;

    //    if (appUpdateInfoOperation.IsSuccessful)
    //    {
    //        var appUpdateInfo = appUpdateInfoOperation.GetResult();

    //        if (appUpdateInfo.UpdateAvailability == UpdateAvailability.UpdateAvailable)
    //        {
    //            Debug.Log("Flexible update available. Starting...");

    //            // ✅ Create proper options object
    //            var updateOptions = AppUpdateOptions.FlexibleAppUpdateOptions();

    //            // ✅ Start update with options
    //            updateManager.StartUpdate(appUpdateInfo, updateOptions);
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogError("Update check failed: " + appUpdateInfoOperation.Error);
    //    }
    //}

    //public void OnUpdateComplete()
    //{
    //    updateManager.CompleteUpdate();
    //}
}

