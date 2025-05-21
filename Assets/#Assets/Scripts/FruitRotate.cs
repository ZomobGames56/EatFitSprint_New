using DG.Tweening;
using UnityEngine;

public class FruitRotate : MonoBehaviour
{
    [SerializeField]
    float XrotSpeed = 5,YrotSpeed=5,ZrotSpeed=5;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(XrotSpeed * Time.deltaTime, YrotSpeed * Time.deltaTime, ZrotSpeed * Time.deltaTime);
    }
}
