using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField]
    TextMeshProUGUI foodCountText, junkfoodText;

    [SerializeField]
    public GameObject gameOverPanel, settingPanel;
    [SerializeField]
    AudioClip buttonClip;
    [SerializeField]
    GameObject mission_ShopCrossBtn, mission_ShopObj, missionPanel, ShopPanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        gameOverPanel.SetActive(false);
        settingPanel.SetActive(false);
        mission_ShopCrossBtn.SetActive(false);
        mission_ShopObj.SetActive(true);
        missionPanel.SetActive(false);
        ShopPanel.SetActive(false);
    }
    // Update is called once per frame
    private void OnDestroy()
    {
        Destroy(gameObject);
    }
    void Update()
    {
        foodCountText.text = GameManager.instance.fruitCount.ToString();
        junkfoodText.text = GameManager.instance.junkFoodCount.ToString();
    }

    public void SettingBtn()
    {
        HY_AudioManager.instance.PlayAudioEffectOnce(buttonClip);
        settingPanel.SetActive(true);
    }

    public void NO()
    {
        HY_AudioManager.instance.PlayAudioEffectOnce(buttonClip);
        settingPanel.SetActive(false);
    }
    public void ToggleMission_ShopCrossBtn()
    {
        if (missionPanel.activeInHierarchy)
        {
            missionPanel.SetActive(false);
        }
        else
        {

            ShopPanel.SetActive(false);
        }
        mission_ShopCrossBtn.SetActive(false);
        mission_ShopObj.SetActive(true);
    }

    public void OnMissionBtn()
    {
        missionPanel.SetActive(true);
        mission_ShopObj.SetActive(false);
        mission_ShopCrossBtn.SetActive(true);
    }

    public void OnShopBtn()
    {
        ShopPanel.SetActive(true);
        mission_ShopObj.SetActive(false);
        mission_ShopCrossBtn.SetActive(true);
    }
}
