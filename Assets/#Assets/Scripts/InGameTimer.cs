using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameTimer : MonoBehaviour
{
    public static InGameTimer instance;
    [SerializeField]
    float seconds,waitTimeForGameEndScreen;
    [SerializeField]
    int minutes;
    public bool timeUp;
    [SerializeField]
    Button junkFoodThrowBtn, fruitThrowBtn;

    [SerializeField]
    TextMeshProUGUI timerText;
    public bool canRunTimer;

    [SerializeField]
    TextMeshProUGUI statusTXT;
    [SerializeField]
    Sprite win, lose;
    
    [SerializeField]
    GameObject gameEndPanel;

    public bool isGameEnded;
    [SerializeField]
    GameObject mainMenuPanel,topPanel,CenterPanel,downPanel,play_stagePanel, fightPanel;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        gameEndPanel.SetActive(false);
        isGameEnded = false;
    }
    void Start()
    {
        timeUp = false;
    }

    
    void Update()
    {
        if (canRunTimer && !timeUp)
        {
            TimerUpdate();
        }

    }
    IEnumerator WaitToShowGameOver()
    {
        yield return new WaitForSeconds(waitTimeForGameEndScreen);
        OpenGameOverPanel();
        mainMenuPanel.SetActive(true);
        topPanel.SetActive(true);
        CenterPanel.SetActive(false);
        downPanel.SetActive(false);
        play_stagePanel.SetActive(false);
        fightPanel.SetActive(false);
    }
    void OpenGameOverPanel()
    {
        statusTXT.text = GameManager.instance.status;
        gameEndPanel.SetActive(true);
    }
   
    void TimerUpdate()
    {
        seconds -= Time.deltaTime;
        if (seconds < 1)
        {
            minutes--;
            if (minutes <= 0)
            {
                GameManager.instance.FitnessStatus(CharacterShapeChange.instance.fitNessBar.value);
                StartCoroutine(WaitToShowGameOver());
                //revive panel open
                timeUp = true;
                timerText.text = "00:00";
                junkFoodThrowBtn.interactable = false;
                fruitThrowBtn.interactable = false;
                minutes = 0;
                isGameEnded = true;
                interstitialUnity.instance.ShowAd();
            }
            else
            {
                seconds = 60;
            }
        }
        if (seconds < 10)
        {
            timerText.text = "0" + minutes + " : " + "0" + (int)seconds;

        }
        else
        {
            timerText.text = "0" + minutes + " : " + (int)seconds;

        }

    }
}
