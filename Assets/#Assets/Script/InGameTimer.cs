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
    Image win_loseBGImg;
    [SerializeField]
    GameObject gameEndPanel;

    public bool isGameEnded;
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

    // Update is called once per frame
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
    }
    void OpenGameOverPanel()
    {
        statusTXT.text = GameManager.instance.status;
        gameEndPanel.SetActive(true);
        if (GameManager.instance.isVictory)
        {
            win_loseBGImg.sprite = win;
        }
        else
        {
            win_loseBGImg.sprite = lose;
        }

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
               // print(GameManager.instance.status);
                // print("Min goes<0called");
                //revive panel open
                timeUp = true;
                timerText.text = "00:00";
                junkFoodThrowBtn.interactable = false;
                fruitThrowBtn.interactable = false;
                minutes = 0;
                isGameEnded = true;
                

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
