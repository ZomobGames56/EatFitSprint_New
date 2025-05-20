using System;
using UnityEngine;
//using UnityEngine.Events;

public class CameraEventHandler : MonoBehaviour
{

    public static event Action CameraEvent;
    public void Print()
    {
        print("Im for camera class");
        CameraEvent.Invoke();
    }
}