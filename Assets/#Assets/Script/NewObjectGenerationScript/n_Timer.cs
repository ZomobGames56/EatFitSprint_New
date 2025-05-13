using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class n_Timer : MonoBehaviour
{
    private static n_Timer instance;
    [SerializeField]
    float seconds = 30, minutes = 0;

    bool canStartTimer;
    [SerializeField]
    TextMeshProUGUI timerText;
    [SerializeField]
    GameObject timerObj,timeUpPanel;
    public static event Action OnHideParent;
    public static event Action NotifyPlayerTimeUp;
    bool isInvoked;

    private void Awake()
    {
        instance = this;
        canStartTimer = false;
        isInvoked = false;
    }
    private void OnDestroy()
    {
        Destroy(gameObject);
    }
    private void Update()
    {
        if(canStartTimer)
        {
            seconds -= Time.deltaTime;
           // print((int)seconds);
            if (seconds < 0)
            {
                seconds = 0;
                PlayerMovement_Ristriction.CanStopMove(true);
                //UI Pop up--> Transaction Screen--> Player Positon Reset-->All objects disable
                timerObj.SetActive(false);
                if (!isInvoked)
                {
                    timeUpPanel.SetActive(true);
                    OnHideParent?.Invoke();
                    NotifyPlayerTimeUp?.Invoke();
                    isInvoked = true;
                }
                //timeUpPanel.SetActive(true);
                //OnHideParent?.Invoke();
                //NotifyPlayerTimeUp?.Invoke();
                //canStartTimer=false;
            }
            if (seconds < 10)
            {
                timerText.text = "0" + minutes + ":" + "0" + (int)seconds;

            }
            else
            {
                timerText.text = "0" + minutes + ":" + (int)seconds;

            }
        }
    }

    public static void StartTimerBool(bool can)
    {
        instance.canStartTimer = can;
        
    }

  
}
