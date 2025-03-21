using DG.Tweening;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    float forwardSpeed, horizontalSpeed, zTilt, yTilt, yTiltSpeed;
    private bool doTweemAnimationCalled;
    [SerializeField]
    private Joystick dyanamicJoyStick;
    private float v;
    Vector3 targetRotation;
    [Header("Max X value where player can move")]
    [Space]
    [SerializeField]
    float maxX;


    [Header("Rotation Settings")]
    public float rotateDuration = 0.5f; // Duration of rotation
    public float rotationZ = 90f; // Target rotation on Z axis

    bool canLeftRightMovement;

    [SerializeField]
    float leftRightMoveSpeed;
    [SerializeField]
    float positiveXVal = 2.5f, negativeXVal = -2.5f;
    [SerializeField]
    GameObject firstTimeFight;
    [SerializeField]
    AudioClip crashSound,loseClip;
    private void Start()
    {
        canLeftRightMovement = false;
        doTweemAnimationCalled = false;
    }
    private void Update()
    {
        if (GameManager.instance.start && !canLeftRightMovement)
        {
            transform.position += Vector3.forward * forwardSpeed * Time.deltaTime;
            PlayerHorizontalMovement();
            PlayerBound();
        }
        if (canLeftRightMovement)
        {
            FightTimeMovement();
        }
    }
    void PlayerBound()
    {
        if (transform.position.x >= maxX)
        {
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, 0), forwardSpeed * Time.deltaTime);


        }
        else if (transform.position.x <= -maxX)
        {
            transform.position = new Vector3(-maxX, transform.position.y, transform.position.z);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, 0), forwardSpeed * Time.deltaTime);

        }
    }
    void PlayerHorizontalMovement()
    {

        if (dyanamicJoyStick.Horizontal >= 0.1f || dyanamicJoyStick.Horizontal <= -0.1f)
        {
            //transform.position += (transform.right * dyanamicJoyStick.Horizontal)
            //     .normalized * speed * Time.deltaTime;

            Vector3 move = (transform.right * dyanamicJoyStick.Horizontal)
                 .normalized * horizontalSpeed * Time.deltaTime;

            move.y = 0f;
            transform.position += move;
            if (dyanamicJoyStick.Horizontal >= 0.1f)
            {

                targetRotation = new Vector3(0, yTilt, -zTilt);
                //transform.localEulerAngles= Vector3.Lerp(transform.localEulerAngles, targetRotation, forwardSpeed *Time.deltaTime);
                transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(targetRotation), yTiltSpeed * Time.deltaTime);


                //  print(targetRotation)
            }

            else if (dyanamicJoyStick.Horizontal <= -0.1f)
            {
                targetRotation = new Vector3(0, -yTilt, zTilt);
                // transform.localEulerAngles =  Vector3.Lerp(transform.localEulerAngles, targetRotation,Time.deltaTime*forwardSpeed);
                transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(targetRotation), yTiltSpeed * Time.deltaTime);

                // print(targetRotation+" Else Time");
            }
            // if(transform.position.x)
        }

        else
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, 0), yTiltSpeed * Time.deltaTime);

            //transform.localEulerAngles =Vector3.Lerp(transform.localEulerAngles, new Vector3(0,0,0), forwardSpeed * Time.deltaTime);
        }

    }
    IEnumerator WaitGameOver()
    {
        yield return new WaitForSeconds(1.5f);
        HY_AudioManager.instance.PlayAudioEffectOnce(loseClip);
        UIManager.Instance.gameOverPanel.SetActive(true);
    }
    void FightTimeMovement()
    {

        transform.position += Vector3.right * leftRightMoveSpeed * Time.deltaTime;
        if (transform.position.x >= positiveXVal)
        {
            transform.position = new Vector3(positiveXVal,
                transform.position.y, transform.position.z);
            leftRightMoveSpeed *= -1;
        }
        if (transform.position.x <= negativeXVal)
        {
            transform.position = new Vector3(negativeXVal, transform.position.y,
                transform.position.z);
            leftRightMoveSpeed *= -1;
        }
    }
    IEnumerator WaitForFightingPanel()
    {
        yield return new WaitForSeconds(3f);
       
        GameManager.instance.makeMeFitScreen.SetActive(false);
        GameManager.instance.collectingPanel.SetActive(false);
        if (!SaveDataManager.instance.VariableExist("KnowFighting"))
        {
            firstTimeFight.SetActive(true);
            //print("TeachMe");
        }
        else
        {
            GameManager.instance.fightingPanel.SetActive(true);
            InGameTimer.instance.canRunTimer = true;
            canLeftRightMovement = true;
            CameraFollow.instance.initialCameraRotation = new Vector3(3f, 0, 0);
            CameraFollow.instance.offsetFromPlayer = new Vector3(0, 10, -25);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndLine"))
        {
            forwardSpeed = 0;
            transform.localRotation = Quaternion.Euler(Vector3.zero);
            //Open Fighting Panel.
            GameManager.instance.makeMeFitScreen.SetActive(true);
            canLeftRightMovement = true;
            StartCoroutine(WaitForFightingPanel());
        }

        if (other.CompareTag("Obstacle"))
        {
            HY_AudioManager.instance.PlayAudioEffectOnce(crashSound);
            if (!doTweemAnimationCalled)
            {
                doTweemAnimationCalled = true;
                yTiltSpeed = 0;
                transform.DOShakePosition(0.5f, 0.5f, 10, 90);
                transform.DORotate(new Vector3(0, 0, rotationZ), rotateDuration, RotateMode.FastBeyond360)
                   .SetEase(Ease.OutBack);

            }
            forwardSpeed = 0;
            //Open Game Over Screen.
            StartCoroutine(WaitGameOver());
        }



    }
}
