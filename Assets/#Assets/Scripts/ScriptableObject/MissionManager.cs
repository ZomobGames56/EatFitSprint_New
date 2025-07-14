using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager instance;
    public MissionUI[] missionSlots;
    public MissionData[] missions;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        for (int i = 0; i < missionSlots.Length && i < missions.Length; i++)
        {
            int index = i;
            missionSlots[i].Setup(missions[i]);
            missionSlots[i].OnMissionComplete += OnMissionCompleted;
        }
    }

    private void OnMissionCompleted(int index)
    {
        Debug.Log($"Mission {index + 1} completed! Reward: {missions[index].reward}");
        CoinsUpdateManager.AddCoins(missions[index].reward);
        
        // Grant reward, log analytics, etc.
    }

    // Call this when fruit or junk is collected
    public void AddMissionProgress(string missionKeyword, int amount = 1)
    {
        foreach (var mission in missions)
        {
            if (mission.missionTitle.Contains(missionKeyword)) // simple match
            {
                mission.AddProgress(amount);
            }
        }

        foreach (var slot in missionSlots)
        {
            slot.UpdateProgressUI();
        }
    }
}
