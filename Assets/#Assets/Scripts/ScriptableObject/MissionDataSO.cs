using UnityEngine;
[CreateAssetMenu(fileName = "NewMission", menuName = "Missions/Mission")]
public class MissionData : ScriptableObject
{
    public string missionTitle;
    public Sprite missionIcon;
    public float targetAmount;
    public int reward;
    public string ItemName;

    [HideInInspector] public float currentAmount;

    public bool IsComplete => currentAmount >= targetAmount;

    public void ResetProgress()
    {
        currentAmount = 0;
          
    }

    public void AddProgress(int amount)
    {
        currentAmount += amount;
        currentAmount = Mathf.Min(currentAmount, targetAmount);

    }

    public void IncreaceTargetAmount()
    {
        targetAmount += Random.Range(15, 51);
    }

    public void RewardIncrease()
    {
        reward += Random.Range(5, 51);
    }
}
