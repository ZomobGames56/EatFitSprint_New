using DG.Tweening;
using UnityEngine;

public class PickUpAnimation : MonoBehaviour
{
    [SerializeField] private float moveUpDistance = 1f;
    [SerializeField] private float moveUpDuration = 0.3f;
    [SerializeField] private float moveToPlayerDuration = 0.5f;
    [SerializeField] private Transform playerTransform;

    public float rotationAngle = 360; // Desired rotation angle
    public float duration = 0.5f; // Duration of the rotation
    [SerializeField]
    AudioClip objectCollection;
    private void Start()
    {
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        transform.DORotate(new Vector3(0, rotationAngle, 0), duration, RotateMode.LocalAxisAdd)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);
    }

    public void OnPickup()
    {
        // Move up animation
        transform.DOMoveY(transform.position.y + moveUpDistance, moveUpDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                // Move toward player
                transform.DOMove(playerTransform.position, moveToPlayerDuration)
                    .SetEase(Ease.InOutQuad)
                    .OnComplete(() =>
                    {
                        HY_AudioManager.instance.PlayAudioEffectOnce(objectCollection);
                        // Perform any additional actions (e.g., add to inventory)
                        //AddToPool();
                        gameObject.SetActive(false);
                        if (gameObject.transform.tag == "Fruit")
                        {
                            GameManager.instance.fruitCount += 2;
                            GameManager.instance.gameObjects[GameManager.instance.index].SetActive(true);
                            GameManager.instance.index++;
                        }
                        else if(gameObject.transform.tag =="JunkFood")
                        {
                            GameManager.instance.junkFoodCount += 2;
                        }
                       
                    });
            });
    }
    private void OnDisable()
    {
        transform.DOKill();
    }
    private void AddToPool()
    {
        gameObject.SetActive(false);

        // Only add to pool if it's not already in it
        if (!FoodObjectPooler.instance.deactivateFoodObj.Contains(gameObject))
        {
            FoodObjectPooler.instance.deactivateFoodObj.Add(gameObject);
        }
    }

    private void Update()
    {
        if (gameObject.activeInHierarchy && transform.position.y <= -2.5f)
        {
           // AddToPool();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag=="Player")
        {
           OnPickup();
        }
    }
}
