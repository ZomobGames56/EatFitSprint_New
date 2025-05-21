  using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class HY_Player_Control : MonoBehaviour
{
    [SerializeField]
    Joystick joystick;// JoyStick Refrence Given in the canvas.
    [SerializeField]
    Rigidbody rb;//Player Rigdbody
    Button btn;
    Vector3 move;
    [SerializeField]
    float moveSpeed = 10f, force = 7f, defaultSpeed = 0.97f, onSliderSpeed = 2.0f,
        waitForSec = 0.5f;//Jump Force
    [SerializeField]
    public Animator animator;
    [SerializeField]
    bool isGrounded, isDashing;
    [SerializeField]
    Transform cam;
    bool jumpbtnPressed = false;
    [SerializeField]
    Transform spawnPoint, firstSp, secondSp, thirdSp, fourthSp;
    [SerializeField]
    bool inAir;
    [SerializeField]
    GameObject effect;

    //[SerializeField]
    //float lastTapTime = 0f, doubleTapThreshold = 0.3f;
    [SerializeField]
    float dashDuration = 0.5f, dashSpeed = 10f;
    Vector3 playerScale;
    [SerializeField]
    float scale = 0.75f;
    public static bool canControl;
    float inAirTime;
    RaycastHit hit;

    bool isCalled;
    public bool rigidBodyControl, transformControl;
    [SerializeField]
    ParticleSystem dustEffect;

    [SerializeField]
    AudioClip jumpSound,fallInWater,collideSound;
    bool collideToWater;

    void Start()
    {
        collideToWater= false;
        isCalled = false;
        canControl = true;
        rb = GetComponent<Rigidbody>();
        isGrounded = false;
        animator = GetComponent<Animator>();
        spawnPoint = firstSp;
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.position;
            transform.rotation = spawnPoint.rotation;
        }
        playerScale = new Vector3(scale, scale, scale);
        transform.localScale = playerScale;
        rigidBodyControl = true;
        transformControl = false;


    }
    // Update is called once per frame
    
    void Update()
    {

        PlayerOutOfBounds();
        if (InAirTime() >= 0.15f)
        {
            HangingAnimation();
        }
        if (canControl == true)
        {
           
            if (Input.GetKeyDown(KeyCode.Space))
            {
                MobileJumpBtn();
            }
            CanAniamte();
        }
    }
    private void FixedUpdate()
    {
     if(canControl == true)
        {
            PlayerMovement();
        }   
    }
    void HangingAnimation()
    {
        animator.SetBool("Hanging", true);
    }// hanging animation true..
   
    public void PlayerMovement()
    {
        if ((Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer || 
            Application.platform==RuntimePlatform.WindowsEditor)&& canControl)
            
        {
            //Debug.Log("Mobile");
            joystick.gameObject.SetActive(true);
            move = cam.transform.right * joystick.Horizontal +
                   cam.transform.forward * joystick.Vertical;
            move.y = 0f;
            if (transformControl)
            {
                transform.position += move * moveSpeed * Time.fixedDeltaTime;
               // Debug.Log("Transform one is calling");
            }
            if (rigidBodyControl)
            {
                if (rb != null)
                {
                    rb.MovePosition(transform.position + move * moveSpeed * Time.fixedDeltaTime);
                   // Debug.Log("rigid one is calling");

                }
            }
            
            if (move.magnitude != 0)
            {
              //  dustEffect.Play();
                Rotate();
            }
            //if (move.magnitude == 0)
            //{
            //    dustEffect.Stop();
            //}
            
        }

        if ((Application.platform == RuntimePlatform.WindowsPlayer||Application.platform == RuntimePlatform.WindowsEditor) && canControl)
        {
            joystick.gameObject.SetActive(false);
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            move = (cam.transform.right * h +
                   cam.transform.forward * v);
            if (move.magnitude > 1)
            {
                move = move.normalized;
            }
            
            move.y = 0f;
            if (transformControl)
            {
                transform.position += move * moveSpeed * Time.deltaTime;
                Debug.Log("Transform one is calling");
            }
            if (rigidBodyControl)
            {
                if (rb != null)
                {
                    rb.MovePosition(transform.position + move * moveSpeed * Time.fixedDeltaTime);
                    //Debug.Log("rigid one is calling");

                }
            }
            if (move.magnitude != 0)
            {
                
                Rotate();
               // dustEffect.Play();
            }
            
        }

    }// joy stick movment.
    public void MobileJumpBtn()
    {
        if (isGrounded && !jumpbtnPressed)
        {
            animator.SetBool("Jump", true);
            HY_AudioManager.instance.PlayAudioEffectOnce(jumpSound);
            rb.AddForce(Vector3.up * force, ForceMode.Impulse);
            isGrounded = false;
            inAir = true;
            jumpbtnPressed = true;
            StartCoroutine(JumpUp());
        }
    }
    public IEnumerator JumpUp()
    {
        yield return new WaitForSeconds(.2f);
        animator.SetBool("Jump", false);
        animator.SetBool("Hanging", true);
        //rb.AddForce(Vector3.up * (-gravity), ForceMode.Impulse);
    }
    IEnumerator Dash()
    {
        //dash animation.
        animator.SetBool("Dash", true);
        isDashing = true;
        float timer = 0;
        while (timer < dashDuration)
        {
            rb.MovePosition(Vector3.forward * dashSpeed * Time.deltaTime);
            timer += Time.deltaTime;
            print("Dash Animation called");
            yield return null;
        }
        isDashing = false;
        //dash animation stop
        animator.SetBool("Dash", false);
    }//dash animation....//Not in use yet...............
    public void CanAniamte()
    {

        if (move.magnitude != 0)
        {
            animator.SetFloat("Run", move.magnitude);
        }
        else
        {
            animator.SetFloat("Run", 0);
        }

    }
   
    public void Rotate()
    {
        Quaternion rot = Quaternion.LookRotation(move, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot,
            720f * Time.deltaTime);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "LeftMover" ||
            collision.transform.tag == "RightMover" ||
            collision.transform.tag == "Water")
        {

            animator.SetBool("Hanging", false);

            // isGrounded = true;
            jumpbtnPressed = false;
            inAir = false;
            moveSpeed = defaultSpeed;
            animator.SetBool("Dash", false);
            isDashing = false;
        }
        if (collision.transform.tag == "Slider")
        {
            moveSpeed = onSliderSpeed;
            isDashing = true;
            animator.SetBool("Jump", false);
            animator.SetBool("Dash", true);
            animator.SetTrigger("Dashing");
            animator.SetBool("Hanging", false);
            isGrounded = true;
            jumpbtnPressed = false;
            inAir = false;

        }
        if (collision.transform.tag == "Water"&& !collideToWater)
        {
            OnCollideWater();
        }
        if (collision.transform.tag == "Obstacle")
        {
            HY_AudioManager.instance.PlayAudioEffectOnce(collideSound);
           // HY_PlayerRagdollActive.instance.OnObstacleCollide();
        }
    }
   
    void PlayerOutOfBounds()
    {
        if (transform.position.y <= -51&& !isCalled)
        {
            // gameObject.SetActive(false
            isCalled = true;
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, 500f);
            //Instantiate(effect, transform.position, Quaternion.EulerRotation(90, 0, 0));
            rb.isKinematic = true;
            StartCoroutine(SpawnWait());
            // set control false.
            canControl = false;
        }
    }

    //This function is responsible for Transform collide with water.
    private void OnCollideWater()
    {
        // gameObject.SetActive(false
        HY_AudioManager.instance.PlayAudioEffectOnce(fallInWater);
        collideToWater = true;
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, 500f);
        Instantiate(effect, transform.position, Quaternion.Euler(90, 0, 0));
        rb.isKinematic = true;
        StartCoroutine(SpawnWait());
        // set control false.
        canControl = false;
    }
    public IEnumerator SpawnWait()
    {
        yield return new WaitForSeconds(waitForSec);
        canControl = true;
        isCalled = false;
        transform.localScale = Vector3.Lerp(Vector3.zero, playerScale, 500f);
        rb.isKinematic = false;
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.localRotation;

    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "FirstSp":
                spawnPoint = firstSp;
                break;
            case "SecondSp":
                spawnPoint = secondSp;
                break;
            case "ThirdSp":
                spawnPoint = thirdSp;
                break;
            case "FourthSp":
                spawnPoint = fourthSp;
                break;
           
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ground")
        {
            collideToWater = false;

            isGrounded = true;
            animator.SetBool("Hanging", false);
            jumpbtnPressed = false;
            inAir = false;
            moveSpeed = defaultSpeed;
            animator.SetBool("Dash", false);
            isDashing = false;

            //in air-timer stop
            //inAirTime = 0;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground")
        {
            inAir = true;
            isGrounded = false;

            //in air-timer go
            //inAirTime += Time.deltaTime;
        }
    }

    float InAirTime()
    {
        if (!isGrounded && inAir && !isDashing)
        {
            inAirTime += Time.deltaTime;
        }
        else
        {
            inAirTime = 0;
        }
        return inAirTime;
    }
}
