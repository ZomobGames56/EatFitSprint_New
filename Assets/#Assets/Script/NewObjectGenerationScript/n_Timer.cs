using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class n_Timer : MonoBehaviour
{
    private static n_Timer instance;
    [SerializeField]
    float seconds = 30, minutes = 0;

    bool canStartTimer;
    [SerializeField]
    TextMeshProUGUI timerText;
    [SerializeField]
    GameObject timerObj, timeText;
    public static event Action OnHideParent;
    public static event Action NotifyPlayerTimeUp;
    //bool isInvoked;

    [SerializeField]
    Image timerFillImg;
    float maxSeconds;

    private Tween pulseTween;
    bool isPulsing = false, isFade = false;

    private void Awake()
    {
        instance = this;
        canStartTimer = false;
       //isInvoked = false;
        maxSeconds = seconds;
        timerFillImg.fillAmount = seconds / maxSeconds;
    }
    private void OnDestroy()
    {
        Destroy(gameObject);
    }
    private void Update()
    {
        if (canStartTimer)
        {
            seconds -= Time.deltaTime;
            FillImageEffect();
            TimerBehavior();
            
        }
    }

    public static void StartTimerBool(bool can)
    {
        instance.canStartTimer = can;

    }
    void TimerBehavior()
    {
        if (seconds < 0)
        {
            seconds = 0;
            PlayerMovement_Ristriction.CanStopMove(true);
            TimeUpAnimationEffect();
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
    void FillImageEffect()
    {


        if (seconds != 0)
        {

            timerFillImg.fillAmount = (seconds / maxSeconds);
        }
        else
        {
            timerFillImg.fillAmount = 0;
        }

        if (timerFillImg.fillAmount < 6 / maxSeconds && !isPulsing && (int)seconds != 0)
        {
            isPulsing = true;
            pulseTween = timerObj.transform.DOScale(1.2f, 0.25f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
            print("under 5");
        }

        if ((int)seconds <= 0)
        {

            //pulseTween.Kill();
            //// isPulsing = false;
            //if (!isFade)
            //{
            //    /// Time Up Text animation
            //             timeText.SetActive(true);
            //    timeText.transform.DOMove(new Vector3(540, timeText.transform.position.y, timeText.transform.position.z), 0.5f)
            //        .OnComplete(() => {
            //            timeText.transform.DOMove(new Vector3(500, timeText.transform.position.y, timeText.transform.position.z), 4f)
            //            .OnComplete(() => { timeText.transform.DOMove(new Vector3(1500, timeText.transform.position.y, timeText.transform.position.z), 0.5f)
            //                    .OnComplete(() =>
            //                    {
            //                        timeText.transform.DOKill();
            //                        timeText.gameObject.SetActive(false);

            //                    });
            //            });
            //        });



            //               OnHideParent?.Invoke();
            //    timerObj.transform.DOScale(1.35f, 5f).SetEase(Ease.OutBack)
            //        .OnComplete(() =>
            //        {


            //            timerObj.transform.DOScale(0f, 0.5f).SetEase(Ease.InBack)
            //           .OnComplete(() =>
            //           {
            //               timerObj.transform.DOKill();
            //               timerObj.gameObject.SetActive(false);
            //               // timeUpPanel.transform.DOScale
            //               // Invoke Events
            //               //timeUpPanel.SetActive(true);
            //               NotifyPlayerTimeUp?.Invoke();
            //              // PlayerMovement_Ristriction.AfterTimeUpPlayer();

            //           });

            //        });
            //    isFade = true;
            //    // isPulsing = true;

            //}

        }

    }

    void TimeUpAnimationEffect()
    {
        pulseTween.Kill();
        // isPulsing = false;
        if (!isFade)
        {
            /// Time Up Text animation
            timeText.SetActive(true);
            timeText.transform.DOMove(new Vector3(540, timeText.transform.position.y, timeText.transform.position.z), 0.5f)
                .OnComplete(() =>
                {
                    timeText.transform.DOMove(new Vector3(500, timeText.transform.position.y, timeText.transform.position.z), 4f)
                    .OnComplete(() =>
                    {
                        timeText.transform.DOMove(new Vector3(1500, timeText.transform.position.y, timeText.transform.position.z), 0.5f)
                            .OnComplete(() =>
                            {
                                timeText.transform.DOKill();
                                timeText.gameObject.SetActive(false);

                            });
                    });
                });



            OnHideParent?.Invoke();
            timerObj.transform.DOScale(1.35f, 5f).SetEase(Ease.OutBack)
                .OnComplete(() =>
                {


                    timerObj.transform.DOScale(0f, 0.5f).SetEase(Ease.InBack)
                   .OnComplete(() =>
                   {
                       timerObj.transform.DOKill();
                       timerObj.gameObject.SetActive(false);

                       NotifyPlayerTimeUp?.Invoke();


                   });

                });
            isFade = true;


        }

    }



}
