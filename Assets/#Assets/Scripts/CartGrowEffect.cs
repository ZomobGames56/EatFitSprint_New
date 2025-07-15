using UnityEngine;
using DG.Tweening;

public class CarGrowEffect : MonoBehaviour
{
    [Header("Bubble Scale Settings")]
    public float growScale = 1.1f;       // Overshoot scale
    public float growDuration = 0.4f;    // Duration from 0 to 1.1x
    public float settleDuration = 0.2f;  // Duration from 1.1x to 1.0x

    private Vector3 originalScale;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    private void OnEnable()
    {
        PlayBubbleGrow();
    }

    public void PlayBubbleGrow()
    {
        // Start from scale 0
        transform.localScale = Vector3.zero;

        Sequence bubbleSeq = DOTween.Sequence();
        bubbleSeq.Append(transform.DOScale(originalScale, growDuration).SetEase(Ease.OutBack))
                 .Append(transform.DOScale(originalScale, settleDuration).SetEase(Ease.OutQuad));
    }
}
