using System.Collections;
using UnityEngine;

public class PlayerMovement_Ristriction : MonoBehaviour
{

    private static PlayerMovement_Ristriction instance;
    [SerializeField]
    Joystick joystick;
    Vector3 move;
    [SerializeField]
    float moveSpeed = 10f, rotateSpeed;
    [SerializeField]
    Transform cam;
    [SerializeField]
    float maxXValue, maxZValue;
    bool stopRun;
    [SerializeField]
    GameObject firstTimeFight;
    bool canLeftRightMovement;
    [SerializeField]
    GameObject timerUpPanel;

    [SerializeField]
    float playLastPosZVal = 100f;

    [SerializeField]
    float leftRightMoveSpeed;
    [SerializeField]
    float positiveXVal = 2.5f, negativeXVal = -2.5f, yAfterLost;
    private void Awake()
    {
        instance = this;
    }
    private void OnEnable()
    {
        n_Timer.NotifyPlayerTimeUp += AfterTimeUpPlayer;
    }
    private void OnDisable()
    {
        n_Timer.NotifyPlayerTimeUp -= AfterTimeUpPlayer;
    }
    private void OnDestroy()
    {
        Destroy(gameObject);
    }
    private void Start()
    {
        stopRun = false;
    }
    private void Update()
    {
        if (!stopRun)
        {
            PlayerMovement();
        }
        if (canLeftRightMovement)
        {
            FightTimeMovement();
        }
    }
    public void PlayerMovement()
    {
        //move = cam.transform.right * joystick.Horizontal +
        //           cam.transform.forward * joystick.Vertical;
        //move.y = 0f;

        //transform.position += (move * moveSpeed * Time.deltaTime);
        //if (move.magnitude >= 0.15f || move.magnitude <= -0.15f)
        //{
        //    Rotate();
        //}
        //PlayerBoundires();

     
        
            move = cam.transform.right * joystick.Horizontal + cam.transform.forward * joystick.Vertical;
            move.y = 0f;
            move = move.normalized;

            float joystickStrength = Mathf.Clamp01(new Vector2(joystick.Horizontal, joystick.Vertical).magnitude);
        
            transform.position += move * moveSpeed * joystickStrength * Time.deltaTime;

            if (joystickStrength >= 0.01f)
            {
                Rotate();
            }

            PlayerBoundires();
        

    }
    public void Rotate()
    {
        Quaternion rot = Quaternion.LookRotation(move, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot,
            rotateSpeed * Time.deltaTime);
    }
    void PlayerBoundires()
    {
        if (transform.position.x >= maxXValue)
        {
            transform.position = new Vector3(maxXValue, transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= -maxXValue)
        {
            transform.position = new Vector3(-maxXValue, transform.position.y, transform.position.z);
        }
        if (transform.position.z <= -maxZValue)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -maxZValue);
        }
    }

    public static void CanStopMove(bool can)
    {
        instance.stopRun = can;
    }

    void AfterTimeUpPlayer()
    {
        StartCoroutine(WaitForFightingPanel());
        transform.rotation = Quaternion.Euler(Vector3.zero);
       
        transform.position = new Vector3(0, 0, playLastPosZVal);
        GameManager.instance.makeMeFitScreen.SetActive(true);

        //transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
        //transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
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
        transform.rotation = Quaternion.Euler(Vector3.zero);

        timerUpPanel.SetActive(false);
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
}

