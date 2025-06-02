using UnityEngine;
using Firebase.Analytics;

public class AnalyticsEvents : MonoBehaviour
{
    private static AnalyticsEvents instacne;

    private void Awake()
    {
        instacne = this;
    }
    private void OnDestroy()
    {
        Destroy(gameObject);
    }
    public static void LevelStartEvent(int level)
    {
        FirebaseAnalytics.LogEvent("Level_Start", new Parameter("Level", level + 1));
    }
    public static void LevelNameEvent(string name)
    {
        FirebaseAnalytics.LogEvent("Level_Name:", new Parameter("Level",name));
    }
    public static void LevelCompleteEvent(int level,string status)
    {
        FirebaseAnalytics.LogEvent("Level_Completed", new Parameter("Level", level + 1));
        FirebaseAnalytics.LogEvent("Status", new Parameter("Status",status));
    }

    public static void GameOverEvent(string reason)
    {
        FirebaseAnalytics.LogEvent("Game_Over", new Parameter("Reason", reason));
    }

}
