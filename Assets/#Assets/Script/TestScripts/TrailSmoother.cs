using UnityEngine;

public class TrailSmoother : MonoBehaviour
{
    TrailRenderer trail;
    private void Start()
    {
        trail = GetComponent<TrailRenderer>();
        trail.time = 1.0f; // Trail lasts for 1 second
        trail.minVertexDistance = 0.05f; // Makes the trail smoother
        trail.startWidth = 0.3f;
        trail.endWidth = 0.0f; // Makes it fade smoothly

    }
}
