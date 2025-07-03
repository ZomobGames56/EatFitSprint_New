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
    string levelIndex;
    [SerializeField]
    Transform fruitCounter, junkCounter;

    [SerializeField]
    int gameRewardCoins;
    [SerializeField]
    TextMeshProUGUI rewardScreenCoinTxt;
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
    private void Start()
    {
        FruitTextUpdate();
        JunkTextUpdate();
        StartCoroutine(StartFirebaseEvent());

    }
    private void OnDestroy()
    {
        Destroy(gameObject);
    }
    IEnumerator StartFirebaseEvent()
    {
        yield return new WaitUntil(() => FirebaseInit.isFirebaseReady);
        AnalyticsEvents.LevelStartEvent(SceneManager.GetActiveScene().buildIndex + 1);
        AnalyticsEvents.LevelNameEvent(SceneManager.GetActiveScene().name);
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
        // fruitCountTxt.text = fruitCount.ToString();
        // junkFoodCountTxt.text = junkFoodCount.ToString();

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
    public void FruitTextUpdate()
    {
        fruitCountTxt.text = fruitCount.ToString();
    }
    public void JunkTextUpdate()
    {
        junkFoodCountTxt.text = junkFoodCount.ToString();
    }
    public void FruitCounterEffect()
    {
        float x = fruitCounter.localScale.x;
        if (x < 1.21f)
            fruitCounter.DOPunchScale(Vector3.one * 0.2f, 0.3f).OnComplete(() => { fruitCounter.DOScale(Vector3.one, 0.2f); });


        Debug.Log(fruitCounter.localScale);
    }
    public void JunkCounterEffect()
    {
        float x = junkCounter.localScale.x;
        if (x < 1.21f)
            junkCounter.DOPunchScale(Vector3.one * 0.2f, 0.3f).OnComplete(() => { junkCounter.DOScale(Vector3.one, 0.2f); });

    }
    public void Play()
    {
        modelViewCamera.SetActive(true);
        HY_AudioManager.instance.PlayAudioEffectOnce(playBtnClip);
        mainMenuPanel.SetActive(false);
        CameraFollow.instance.initialCameraRotation = new Vector3(25, 0, 0);
        CameraFollow.instance.offsetFromPlayer = new Vector3(0, 40, -48);
    }
    //---------------------------- -150 to 150 

    public void FitnessStatus(float sliderVal)
    {
        if (sliderVal >= 70 && sliderVal <= 150)
        {
            //FITNESS: TOO SLIM
            status = "FITNESS: TOO FAT";
            AnalyticsEvents.LevelCompleteEvent(SceneManager.GetActiveScene().buildIndex, "Lose");

            //200
            // DefaultCoins = 200;
            //default coins
            gameRewardCoins = 200;


        }
        else if (sliderVal >= 30 && sliderVal <= 69)
        {
            status = "FITNESS: FAT";
            //350
            AnalyticsEvents.LevelCompleteEvent(SceneManager.GetActiveScene().buildIndex, "Lose");

            // DefaultCoins = 350;
            gameRewardCoins = 350;


        }
        else if (sliderVal >= 6 && sliderVal <= 31)
        {
            status = "FITNESS: FIT";
            //1000
            isVictory = true;
            AnalyticsEvents.LevelCompleteEvent(SceneManager.GetActiveScene().buildIndex, "Win");
            gameRewardCoins = 1000;

            //  DefaultCoins = 1000;
        }
        else if (sliderVal >= -10 && sliderVal <= 5)
        {
            status = "FITNESS: PERFECT";
            //2000
            isVictory = true;
            AnalyticsEvents.LevelCompleteEvent(SceneManager.GetActiveScene().buildIndex, "Win");

            gameRewardCoins = 2000;

            //  DefaultCoins = 2000;
            // WinCheck();

        }
        else if (sliderVal >= -30 && sliderVal <= -9)
        {
            status = "FITNESS: FIT";
            //1000
            isVictory = true;
            AnalyticsEvents.LevelCompleteEvent(SceneManager.GetActiveScene().buildIndex, "Lose");
            gameRewardCoins = 1000;

            // DefaultCoins = 1000;

        }
        else if (sliderVal >= -69 && sliderVal <= -31)
        {
            status = "FITNESS: SLIM";
            //350
            AnalyticsEvents.LevelCompleteEvent(SceneManager.GetActiveScene().buildIndex, "Lose");
            gameRewardCoins = 350;

            // DefaultCoins = 350;

        }
        else if (sliderVal >= -150 && sliderVal <= -70)
        {
            status = "FITNESS: TOO SLIM";
            AnalyticsEvents.LevelCompleteEvent(SceneManager.GetActiveScene().buildIndex, "Lose");

            //200
            // DefaultCoins = 200;
            gameRewardCoins = 200;


        }
        rewardScreenCoinTxt.text = gameRewardCoins.ToString();
    }
    public void Quit()
    {
        HY_AudioManager.instance.PlayAudioEffectOnce(playBtnClip);
        Application.Quit();
    }

    public void OnRewardNoTha4nks()
    {
        CoinsUpdateManager.AddCoins(gameRewardCoins);
        HomeBtn();
    }
    public void HomeBtn()
    {
        HY_AudioManager.instance.PlayAudioEffectOnce(playBtnClip);
        int rand = Random.Range(0, 3);
        StartCoroutine(LoadSceneAsync(rand));
    }

    public void Retry()
    {
        StartCoroutine(LoadSceneAsync(SceneManager.GetActiveScene().buildIndex));
        //LoadSceneAsync();
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
