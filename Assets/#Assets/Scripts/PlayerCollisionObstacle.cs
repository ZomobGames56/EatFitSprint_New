using DG.Tweening;
using System.Collections;
using UnityEngine;

public class PlayerCollisionObstacle : MonoBehaviour
{
    // collision or trigger check
    // camera Shake
    // food/ Junk food substract
    //Game over screen

    // object that recude the junkfood and fruits
    // object that direct over the game.

    //types 1). Hay, Box, 

    [SerializeField]
    GameObject gameOverScreen, timerBG;
    bool triggerAtOnce, doTweemAnimationCalled;
    [SerializeField]
    AudioClip crashSound;
    [SerializeField]
    float yAfterLost, rotationZ, rotateDuration;
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Hay":
                //damege food -4 points
                if (GameManager.instance.fruitCount != 0)
                    GameManager.instance.fruitCount -= 4;
                if (GameManager.instance.junkFoodCount != 0)
                    GameManager.instance.junkFoodCount -= 4;

                if (GameManager.instance.fruitCount < 0)
                {
                    GameManager.instance.fruitCount = 0;
                }
                if (GameManager.instance.junkFoodCount < 0)
                {
                    GameManager.instance.junkFoodCount = 0;
                }

                break;
            case "Car":
                //Cart Damage Game over
                //Player transform change
                if (!triggerAtOnce)
                {
                    triggerAtOnce = true;
                    HY_AudioManager.instance.PlayAudioEffectOnce(crashSound);
                    if (!doTweemAnimationCalled)
                    {
                        doTweemAnimationCalled = true;
                        //yTiltSpeed = 0;
                        transform.DOShakePosition(0.5f, 0.5f, 10, 90)
                              .OnComplete(() =>
                              {
                                  //Vector3 pos = transform.position;
                                  // pos.y = yAfterLost; // Lock Y
                                  transform.DORotate(new Vector3(0, 0, rotationZ), rotateDuration, RotateMode.FastBeyond360).SetEase(Ease.OutBack)
                                  .OnUpdate(() =>
                                  { 
                                      transform.DOMove(new Vector3(transform.position.x, yAfterLost, transform.position.z), 0.2f);
                                  });

                              });
                        //                 transform.DORotate(new Vector3(0, 0, rotationZ), rotateDuration, RotateMode.FastBeyond360)
                        //.SetEase(Ease.OutBack);
                    }
                    PlayerMovement_Ristriction.CanStopMove(true);
                    n_Timer.StartTimerBool(false);
                    timerBG.gameObject.SetActive(false);
                    StartCoroutine(WaitForGameOverScreen());
                }
                break;
            case "WoodCart":
                //cart Damage Game over
                if (!triggerAtOnce)
                {
                    triggerAtOnce = true;
                    HY_AudioManager.instance.PlayAudioEffectOnce(crashSound);
                    if (!doTweemAnimationCalled)
                    {
                        doTweemAnimationCalled = true;
                        //yTiltSpeed = 0;
                        transform.DOShakePosition(0.5f, 0.5f, 10, 90)
                              .OnUpdate(() =>
                              {
                                  Vector3 pos = transform.position;
                                  pos.y = yAfterLost; // Lock Y
                                  transform.position = pos;
                              }).OnComplete(() =>
                              {
                                  transform.DORotate(new Vector3(0, 0, rotationZ), rotateDuration, RotateMode.FastBeyond360)
                 .SetEase(Ease.OutBack);
                              });



                    }
                    PlayerMovement_Ristriction.CanStopMove(true);
                    n_Timer.StartTimerBool(false);
                    timerBG.gameObject.SetActive(false);
                    StartCoroutine(WaitForGameOverScreen());
                }
                break;
            case "Barrier":
                //Cart Damage Game over
                if (!triggerAtOnce)
                {
                    triggerAtOnce = true;
                    //  HY_AudioManager.instance.PlayAudioEffectOnce(crashSound);
                    if (!doTweemAnimationCalled)
                    {
                        doTweemAnimationCalled = true;
                        //yTiltSpeed = 0;
                        transform.DOShakePosition(0.5f, 0.5f, 10, 90)
                              .OnComplete(() =>
                              {
                                  Vector3 pos = transform.position;
                                  pos.y = yAfterLost; // Lock Y
                                  transform.position = pos;
                              }).OnComplete(() =>
                              {
                                  transform.DORotate(new Vector3(0, 0, rotationZ), rotateDuration, RotateMode.FastBeyond360)
                 .SetEase(Ease.OutBack);
                              });



                    }
                    PlayerMovement_Ristriction.CanStopMove(true);
                    n_Timer.StartTimerBool(false);
                    timerBG.gameObject.SetActive(false);
                    StartCoroutine(WaitForGameOverScreen());
                }
                break;
            case "LongBarrier":
                //cart Damage Game over
                if (!triggerAtOnce)
                {
                    triggerAtOnce = true;
                    //  HY_AudioManager.instance.PlayAudioEffectOnce(crashSound);
                    if (!doTweemAnimationCalled)
                    {
                        doTweemAnimationCalled = true;
                        //yTiltSpeed = 0;
                        transform.DOShakePosition(0.5f, 0.5f, 10, 90)
                              .OnComplete(() =>
                              {
                                  Vector3 pos = transform.position;
                                  pos.y = yAfterLost; // Lock Y
                                  transform.position = pos;
                              }).OnComplete(() =>
                              {
                                  transform.DORotate(new Vector3(0, 0, rotationZ), rotateDuration, RotateMode.FastBeyond360)
                 .SetEase(Ease.OutBack);
                              });



                    }
                    PlayerMovement_Ristriction.CanStopMove(true);
                    n_Timer.StartTimerBool(false);
                    timerBG.gameObject.SetActive(false);
                    StartCoroutine(WaitForGameOverScreen());
                }
                break;
            case "Box":
                // Damage Food -2 points
                if (GameManager.instance.fruitCount != 0)
                    GameManager.instance.fruitCount -= 4;
                if (GameManager.instance.junkFoodCount != 0)
                    GameManager.instance.junkFoodCount -= 4;

                if (GameManager.instance.fruitCount < 0)
                {
                    GameManager.instance.fruitCount = 0;
                }
                if (GameManager.instance.junkFoodCount < 0)
                {
                    GameManager.instance.junkFoodCount = 0;
                }
                break;
        }
    }

    IEnumerator WaitForGameOverScreen()
    {
        yield return new WaitForSeconds(2f);
        gameOverScreen.SetActive(true);

    }
}
