using UnityEngine;

public class PlayerMovement_Ristriction : MonoBehaviour
{ 

    [SerializeField]
    Joystick joystick;
    Vector3 move;
    [SerializeField]
    float moveSpeed = 10f,rotateSpeed;
    [SerializeField]
    Transform cam;
    [SerializeField]
    float maxXValue, maxZValue;

    private void Update()
    {
        PlayerMovement();
    }

    
    public void PlayerMovement()
    {

        move = cam.transform.right * joystick.Horizontal +
                   cam.transform.forward * joystick.Vertical;
        move.y = 0f;


        transform.position += move * moveSpeed * Time.deltaTime;
        // Debug.Log("Transform one is calling");

        if (move.magnitude != 0)
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
        // tfansform.rotation = Quaternion .Slerp(transform.rotation,rot, 10* Time.deltaTime);
    }
    void PlayerBoundires()
    {
        if (transform.position.x >= maxXValue)
        {
            transform.position = new Vector3(maxXValue, transform.position.y, transform.position.z);
           // transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, 0), 15 * Time.deltaTime);
            


        }
        else if (transform.position.x <= -maxXValue)
        {
            transform.position = new Vector3(-maxXValue, transform.position.y, transform.position.z);
          ///  transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, 0), 15 * Time.deltaTime);

        }
        if (transform.position.z <= -maxZValue)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -maxZValue);
        }
        
    }

}

