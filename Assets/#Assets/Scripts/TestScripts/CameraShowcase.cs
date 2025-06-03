using DG.Tweening;
using UnityEngine;

public class CameraShowcase : MonoBehaviour
{
    [SerializeField] private float durationToMiddle = 2f;
    [SerializeField] private float durationToEnd = 2f;
    [SerializeField] private Transform lookAtTarget;

    private Camera cam;

    [SerializeField]
    private void Start()
    {
        // cam = Camera.main;

        Vector3 startPoint = new Vector3(0, 10, -900);
        Vector3 middlePoint = new Vector3(0, 20, -400);  // Goes slightly up in Y
        Vector3 endPoint = new Vector3(0, 30, 100);      // Final Z position

       // cam.transform.position = startPoint;
       // cam.transform.LookAt(lookAtTarget.position);

        StartCameraShowcase(middlePoint, endPoint);
    }

    public void StartCameraShowcase(Vector3 middle, Vector3 end)
    {
        Sequence camSequence = DOTween.Sequence();

        camSequence.Append(cam.transform.DOMove(middle, durationToMiddle).SetEase(Ease.InOutSine));
        camSequence.Join(cam.transform.DOLookAt(lookAtTarget.position, durationToMiddle));

        camSequence.Append(cam.transform.DOMove(end, durationToEnd).SetEase(Ease.InOutSine));
        camSequence.Join(cam.transform.DOLookAt(lookAtTarget.position, durationToEnd));

        camSequence.OnComplete(() =>
        {
            Debug.Log("Camera showcase complete.");
        });
    }
}
