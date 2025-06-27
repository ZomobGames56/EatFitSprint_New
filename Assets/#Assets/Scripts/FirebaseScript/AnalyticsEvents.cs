using Firebase.Analytics;
using UnityEngine;

public class AnalyticsEvents : MonoBehaviour
{
    private static AnalyticsEvents instacne;

    private void Awake()
    {
        if (instacne != null && instacne != this)
        {
            Destroy(gameObject);
            return;
        }
        instacne = this;
        DontDestroyOnLoad(gameObject);
    }

    public static void LevelStartEvent(int level)
    {
        if (FirebaseInit.isFirebaseReady)
        {
            FirebaseAnalytics.LogEvent("Level_Start", new Parameter("Level", level + 1));
            Debug.Log("Event Called");
        }
        else
        {
            Debug.Log("Failed to load");
            Debug.Log(FirebaseInit.isFirebaseReady);
        }
    }
    public static void LevelNameEvent(string name)
    {
        if (FirebaseInit.isFirebaseReady)
        {
            FirebaseAnalytics.LogEvent("Level_Name", new Parameter("Level", name));
            Debug.Log("Event Called");
        }
    }
    public static void LevelCompleteEvent(int level, string status)
    {
        if (FirebaseInit.isFirebaseReady)
        {
            FirebaseAnalytics.LogEvent("Level_Completed", new Parameter("Level", level + 1));
            FirebaseAnalytics.LogEvent("Status", new Parameter("Status", status));
            Debug.Log("Event Called");
        }
    }

    public static void GameOverEvent(string reason)
    {
        if (FirebaseInit.isFirebaseReady)
        {
            FirebaseAnalytics.LogEvent("Game_Over", new Parameter("Reason", reason));
            Debug.Log("Event Called");
        }
    }

    public static void GameSessionStart()
    {
        FirebaseAnalytics.LogEvent("Session", new Parameter("GameStart","GameStart"));
    }
    public static void GameSessionEnd()
    {
        FirebaseAnalytics.LogEvent("Session", new Parameter("GameEnd", "GameEnd"));
    }

}
