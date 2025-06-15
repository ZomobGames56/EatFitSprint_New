using UnityEngine;

public class Timer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    int sec, min;
    float elapsedtime;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        elapsedtime += Time.deltaTime * 60;

        sec = (int)(elapsedtime % 60); // Calculate seconds from elapsed time
        min = (int)(elapsedtime / 60); // Calculate minutes from elapsed time

      //  Debug.LogError(string.Format("{0:00}:{1:00}", min, sec));
    }
}
