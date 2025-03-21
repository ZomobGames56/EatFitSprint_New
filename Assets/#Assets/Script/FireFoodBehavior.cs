using UnityEngine;

public class FireFoodBehavior : MonoBehaviour
{

    [SerializeField]
    float fwdSpeed = 5,life,force;
   
    // Update is called once per frame
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }
    void Update()
    {
        transform.position += Vector3.forward * fwdSpeed * Time.deltaTime;
        rb.linearVelocity = Vector3.up * force * Time.deltaTime;
        Destroy(gameObject, life);
    }
}
