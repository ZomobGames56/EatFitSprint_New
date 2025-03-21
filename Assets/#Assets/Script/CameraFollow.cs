using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;
    [SerializeField]
    Transform playerTransform;
    [SerializeField]
    public Vector3 initialCameraRotation = new Vector3(45f, 0, 0);
    [SerializeField]
    public Vector3 offsetFromPlayer = new Vector3(0, 10, -10);

    public float cameraFollowSpeed = 5f;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (playerTransform == null)
        {
            Debug.Log("Player transform is missing");
            playerTransform = GameObject.FindGameObjectWithTag("HY_Player").transform;
        }
    }


    void LateUpdate()
    {
            transform.rotation = Quaternion.Euler(initialCameraRotation);

            transform.position = Vector3.Lerp(transform.position,
                playerTransform.position + offsetFromPlayer, cameraFollowSpeed * Time.deltaTime);
    }
}
