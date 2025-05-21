using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;

public class DoMaskImageMove : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       // transform.DOMove(new Vector3(transform.position.x + 1000, transform.position.y, transform.position.z), 0.75f).SetLoops(-1).SetDelay(2f);
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOMove(new Vector3(transform.position.x + 1000, transform.position.y, transform.position.z), 0.75f))
           .AppendInterval(1f)
           .SetLoops(-1);
    }

}

