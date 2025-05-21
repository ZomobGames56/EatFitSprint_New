using UnityEngine;

public class WheelRotate : MonoBehaviour
{
    [SerializeField]
    float rotationSpeed;


    private void Update()
    {
        transform.Rotate(0,0,rotationSpeed*Time.deltaTime);
      
    }




}
