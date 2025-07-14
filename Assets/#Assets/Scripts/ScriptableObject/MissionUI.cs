using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionUI : MonoBehaviour
{
    public Image iconImage;
    public TMP_Text titleText;
    public TMP_Text progressText;
    public Button completeButton;
    public Image fillBarImg;

    private MissionData mission;
    [SerializeField]
    TMP_Text rewardCoins;
    public event Action<int> OnMissionComplete;

    public void Setup(MissionData missionData)
    {
        mission = missionData;
        string name = mission.ItemName;

        //titleText.text = mission.missionTitle;
        titleText.text = $"Collect {mission.targetAmount} {name}"; 
        iconImage.sprite = mission.missionIcon;
       // print((float)mission.currentAmount / mission.targetAmount);

        fillBarImg.fillAmount = mission.currentAmount/mission.targetAmount;
        UpdateProgressUI();

        completeButton.onClick.AddListener(OnCompleteButtonClicked);
    }

    public void UpdateProgressUI()
    {
        string name= mission.ItemName;
        progressText.text = $"{mission.currentAmount}/{mission.targetAmount}";
       // print((float)mission.currentAmount / mission.targetAmount);
        fillBarImg.fillAmount = mission.currentAmount / mission.targetAmount;
        titleText.text = $"Collect {mission.targetAmount} {name}";
        rewardCoins.text = mission.reward.ToString();
        completeButton.interactable = mission.IsComplete;
    }

    private void OnCompleteButtonClicked()
    {
        if (mission.IsComplete)
        {
            OnMissionComplete?.Invoke(transform.GetSiblingIndex()); // e.g., notify index to manager
            mission.ResetProgress();
            mission.IncreaceTargetAmount();
            mission.RewardIncrease();
            UpdateProgressUI();
        }
    }
}
