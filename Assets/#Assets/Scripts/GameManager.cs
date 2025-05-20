using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int index;
    public int fruitCount, junkFoodCount;
    [SerializeField]
    public List<GameObject> gameObjects = new List<GameObject>();
    public GameObject mainMenuPanel;

    public bool isVictory;

    public bool start;
    public string status;
    [SerializeField]
    TextMeshProUGUI fruitCountTxt, junkFoodCountTxt;

    public GameObject makeMeFitScreen;
    public GameObject fightingPanel, collectingPanel;
    [SerializeField]
    GameObject m_movementInstruction;
    [SerializeField]
    AudioClip playBtnClip, winClip, loseClip;

    [SerializeField]
    GameObject timerBG_GO, modelViewCamera;
    [SerializeField]
    GameObject modelViewPanel, joystick;

    bool soundPlayed;
    [SerializeField]
    GameObject loadingPanel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        soundPlayed = false;
        start = false;
        collectingPanel.SetActive(false);
        isVictory = false;
        mainMenuPanel.SetActive(true);
        m_movementInstruction.SetActive(false);
        timerBG_GO.SetActive(false);
        modelViewCamera.SetActive(false);
    }
    private void OnEnable()
    {
        CameraEventHandler.CameraEvent += CameraAnimationEndEvent;
    }
    private void OnDisable()
    {
        CameraEventHandler.CameraEvent -= CameraAnimationEndEvent;
    }
    private void Update()
    {
        fruitCountTxt.text = fruitCount.ToString();
        junkFoodCountTxt.text = junkFoodCount.ToString();
        if (isVictory && InGameTimer.instance.isGameEnded)
        {
            if (!soundPlayed)
            {
                HY_AudioManager.instance.PlayAudioEffectOnce(winClip);
                soundPlayed = true;
            }
        }
        else if (!isVictory && InGameTimer.instance.isGameEnded)
        {
            if (!soundPlayed)
            {
                HY_AudioManager.instance.PlayAudioEffectOnce(loseClip);
                soundPlayed = true;
            }
        }


    }
    public void Play()
    {
        modelViewCamera.SetActive(true);
        //if (!SaveDataManager.instance.VariableExist("LearnedPlay"))
        //{
        //    m_movementInstruction.SetActive(true);
        //}
        //else
        //{
        //    start = true;
        //    collectingPanel.SetActive(true);
        //    timerBG_GO.SetActive(true);
        //    n_Timer.StartTimerBool(true);

        //}


        HY_AudioManager.instance.PlayAudioEffectOnce(playBtnClip);
        mainMenuPanel.SetActive(false);
    }
    //---------------------------- -150 to 150 

    public void FitnessStatus(float sliderVal)
    {
        if (sliderVal >= 70 && sliderVal <= 150)
        {
            status = "TOO FAT";
            //200
            // DefaultCoins = 200;
            //default coins


        }
        else if (sliderVal >= 15 && sliderVal <= 69)
        {
            status = "FAT";
            //350
            // DefaultCoins = 350;

        }
        else if (sliderVal >= 6 && sliderVal <= 14)
        {
            status = "FIT";
            //1000
            isVictory = true;
            //  DefaultCoins = 1000;
        }
        else if (sliderVal >= -10 && sliderVal <= 5)
        {
            status = "PERFECT";
            //2000
            isVictory = true;

            //  DefaultCoins = 2000;
            // WinCheck();

        }
        else if (sliderVal >= -14 && sliderVal <= -9)
        {
            status = "FIT";
            //1000
            isVictory = true;

            // DefaultCoins = 1000;

        }
        else if (sliderVal >= -69 && sliderVal <= -15)
        {
            status = "SLIM";
            //350
            // DefaultCoins = 350;

        }
        else if (sliderVal >= -150 && sliderVal <= -70)
        {
            status = "TOO SLIM";
            //200
            // DefaultCoins = 200;

        }
    }
    public void Quit()
    {
        HY_AudioManager.instance.PlayAudioEffectOnce(playBtnClip);
        Application.Quit();
    }
    public void Retry()
    {
        HY_AudioManager.instance.PlayAudioEffectOnce(playBtnClip);
        int rand = Random.Range(0, 4);
        StartCoroutine(LoadSceneAsync(rand));
    }


    IEnumerator LoadSceneAsync(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        while (!operation.isDone)
        {
            loadingPanel.SetActive(true);
            yield return null;
        }
        loadingPanel.SetActive(false);
    }


    IEnumerator ModelCameraView()
    {
        // start model camera false
        //event for model camera complete
        // 
        yield return null;
    }


    public void CameraAnimationEndEvent()
    {
        //set active panel
        //status of the character
        print("End Calling");
        // instance.modelViewCamera.SetActive(false);
        modelViewPanel.SetActive(true);
        modelViewPanel.transform.DOScale(1, 0.5f).SetEase(Ease.OutBack);

    }

    public void Lets_GO()
    {
        modelViewPanel.SetActive(false);
        modelViewCamera.SetActive(false);
        joystick.SetActive(true);
        if (!SaveDataManager.instance.VariableExist("LearnedPlay"))
        {
            m_movementInstruction.SetActive(true);
        }
        else
        {
            start = true;
            collectingPanel.SetActive(true);
            timerBG_GO.SetActive(true);
            n_Timer.StartTimerBool(true);

        }
    }
}
