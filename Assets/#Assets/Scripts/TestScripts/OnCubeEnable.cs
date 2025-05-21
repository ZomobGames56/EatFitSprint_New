using UnityEngine;

public class OnCubeEnable : MonoBehaviour
{
    [SerializeField]
    float speed;
    private void OnEnable()
    {
       // transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, speed * Time.deltaTime);
    }
    private void OnDisable()
    {
        transform.localScale = Vector3.zero;
    }
    private void Update()
    {
        Vector3 targetScale = Vector3.one;
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale,speed*Time.deltaTime);

    }
}
