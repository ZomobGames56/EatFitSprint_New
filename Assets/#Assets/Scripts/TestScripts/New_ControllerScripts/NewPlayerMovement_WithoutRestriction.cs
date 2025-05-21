using UnityEngine;

public class NewPlayerMovement_WithoutRestriction : MonoBehaviour
{
    
    [SerializeField]
    Joystick joystick;
    Vector3 move;
    [SerializeField]
    float moveSpeed = 10f;
    [SerializeField]
    Transform cam;

    private void Update()
    {
        PlayerMovement();
    }

    /// <summary>
    /// This Script is for without any Area restriction.
    /// </summary>
    public void PlayerMovement()
    {

        move = cam.transform.right * joystick.Horizontal +
                   cam.transform.forward * joystick.Vertical;
        move.y = 0f;
      
        
            transform.position += move * moveSpeed * Time.deltaTime;
            // Debug.Log("Transform one is calling");
       

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
    public void Rotate()
    {
        Quaternion rot = Quaternion.LookRotation(move, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot,
            720f* Time.deltaTime);
        // tfansform.rotation = Quaternion .Slerp(transform.rotation,rot, 10* Time.deltaTime);
    }


}
