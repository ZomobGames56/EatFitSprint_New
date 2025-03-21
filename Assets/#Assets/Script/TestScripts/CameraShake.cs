using DG.Tweening;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;
    private void Awake() => Instance = this;
    public float duration = 0.5f;
    public float strength = 1f;
    public int vibrato = 10;
    public float randomness = 90f;

    public void Shake()
    {
        transform.DOShakePosition(duration, strength, vibrato, randomness);
    }
}
