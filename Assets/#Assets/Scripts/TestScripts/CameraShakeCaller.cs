using UnityEngine;

public class CameraShakeCaller : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CameraShake.Instance.Shake();
        }
    }
}
