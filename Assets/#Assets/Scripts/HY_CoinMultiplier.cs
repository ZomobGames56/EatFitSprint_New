using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HY_CoinMulitiplier : MonoBehaviour
{
    public static HY_CoinMulitiplier instance;
    [SerializeField]
    Image indigetor;
    //bool canRotate;

    [SerializeField]
    TextMeshProUGUI coinMultiplierTxt;
    public float coinMultiplier;

    [SerializeField] private float rotationSpeed = 100f; // Speed of rotation
    [SerializeField] private float maxRotation = 80f;     // Maximum rotation angle
    private float currentAngle = 0f;
    private bool rotatingRight = true;
    private RectTransform rectTransform;
    bool canRotate;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        canRotate = true;
    }
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    private void Update()
    {
        // Determine the direction of rotation
        if (canRotate)
        {
            if (rotatingRight)
            {
                currentAngle += rotationSpeed * Time.deltaTime;
                if (currentAngle >= maxRotation)
                {
                    currentAngle = maxRotation;
                    rotatingRight = false;
                }
            }
            else
            {
                currentAngle -= rotationSpeed * Time.deltaTime;
                if (currentAngle <= -maxRotation)
                {
                    currentAngle = -maxRotation;
                    rotatingRight = true;
                }
            }

            // Apply rotation using Quaternion to avoid gimbal lock
            rectTransform.localRotation = Quaternion.Euler(0f, 0f, currentAngle);
        }

        float angleZ = indigetor.rectTransform.rotation.eulerAngles.z;
        if (angleZ > 180) angleZ -= 360; // Convert values like 353 to -7

        if (angleZ <= 80 && angleZ >= 25)
        {

            coinMultiplier = GameManager.instance.gameRewardCoins * 1.5f;
            coinMultiplierTxt.text = coinMultiplier.ToString();
        }
        else if (Mathf.Approximately(angleZ, -7) || Mathf.Approximately(angleZ, 15))
        {

            coinMultiplier = GameManager.instance.gameRewardCoins * 2f;
            coinMultiplierTxt.text = coinMultiplier.ToString();

        }
        else if (angleZ <= -29 && angleZ >= -80)
        {

            coinMultiplier = GameManager.instance.gameRewardCoins * 3f;
            coinMultiplierTxt.text = coinMultiplier.ToString();
        }
        else if ((angleZ > -7.5f && angleZ < -6.5f) || (angleZ > 14.5f && angleZ < 15.5f))
        {

            coinMultiplier = GameManager.instance.gameRewardCoins * 2f;
            coinMultiplierTxt.text = coinMultiplier.ToString();
        }
        else if (Mathf.Abs(Mathf.DeltaAngle(angleZ, -7)) < 1 || Mathf.Abs(Mathf.DeltaAngle(angleZ, 15)) < 1)
        {

            coinMultiplier = GameManager.instance.gameRewardCoins * 2f;
            coinMultiplierTxt.text = coinMultiplier.ToString();

        }


    }
    public void CoinMultipier()
    {

        canRotate = false;
        float angleZ = indigetor.rectTransform.rotation.eulerAngles.z;
        // if (angleZ > 180) angleZ -= 360; // Convert values like 353 to -7

        if (angleZ <= 80 && angleZ >= 25)
        {

            coinMultiplier = GameManager.instance.gameRewardCoins * 1.5f;
            coinMultiplierTxt.text = coinMultiplier.ToString();


        }
        else if (Mathf.Approximately(angleZ, -7) || Mathf.Approximately(angleZ, 15))
        {

            coinMultiplier = GameManager.instance.  gameRewardCoins * 2f;
            coinMultiplierTxt.text = coinMultiplier.ToString();
        }
        else if (angleZ <= -29 && angleZ >= -80)
        {

            coinMultiplier = GameManager.instance.gameRewardCoins * 3f;
            coinMultiplierTxt.text = coinMultiplier.ToString();
        }

        else if (Mathf.Abs(Mathf.DeltaAngle(angleZ, -7)) < 1 || Mathf.Abs(Mathf.DeltaAngle(angleZ, 15)) < 1)
        {

            coinMultiplier = GameManager.instance.gameRewardCoins * 2f;
            coinMultiplierTxt.text = coinMultiplier.ToString();
        }

        // GameManager.instance.coins += (int)coinMultiplier;
        CoinsUpdateManager.AddCoins((int)coinMultiplier);
        GameManager.instance.HomeBtn();
        //rewardGrantedPanel.SetActive(true);
        //Home/restart/


       
    }

}