using DG.Tweening;
using UnityEngine;

public class DonwDotween : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //transform.DOScale(0, 0.1f).SetEase(Ease.InBack)
        //    .OnComplete(() =>
        //    {
        //        transform.DOScale(1, 0.3f).SetEase(Ease.OutBack)
        //        .OnComplete(() =>
        //        {
        //            transform.DOMove(new Vector3(transform.position.x, transform.position.y - 100f, transform.position.z), 0.1f)
        //                .OnComplete(() =>
        //                {
        //                    transform.DOMove(new Vector3(transform.position.x, transform.position.y + 90, transform.position.z), 0.1f)
        //                    .OnComplete(() =>
        //                    {
        //                        transform.DOMove(new Vector3(transform.position.x, transform.position.y - 100f, transform.position.z), 0.1f);
        //                    });
        //                });

        //        });

        //    });

        //transform.DOScale(1.2f, 1f).OnComplete(() =>
        //{
        //    transform.DORotate(new Vector3(0, 0, 10), 0.25f).OnComplete(() =>
        //    {
        //        transform.DORotate(new Vector3(0, 0, -10), 0.25f).OnComplete(() =>
        //        {
        //            transform.DOScale(1, 1f).OnComplete(() =>
        //            {
        //                transform.DORotate(Vector3.zero, 0.1f);
        //            }).SetLoops(-1);
        //        });
        //    });
        //});

        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOScale(1.2f, 0.5f))                                // Scale up
           .Append(transform.DORotate(new Vector3(0, 0, 10), 0.15f))           // Rotate right
           .Append(transform.DORotate(new Vector3(0, 0, -10), 0.15f))          // Rotate left
           .Append(transform.DORotate(Vector3.zero, 0.1f))                     // Reset rotation
           .Append(transform.DOScale(1f, 0.35f))                                  // Scale down
           .AppendInterval(1f)                                                 // Delay before loop
           .SetLoops(-1);

    }
}
