using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ParticlePool : MonoBehaviour
{
    public static ParticlePool instance;
    public GameObject particlePrefab,modelHitEffect;
    [SerializeField]
    private Queue<GameObject> pool = new Queue<GameObject>();
    private Queue<GameObject> effectPool = new Queue<GameObject>();

    private void Awake()
    {
        instance = this;
    }
    public void HitEffect(Vector3 hitPos)
    {
        GameObject particle;
        if (effectPool.Count > 0)
        {
            particle = pool.Dequeue();
            particle.SetActive(true);
        }
        else
        {
            particle = Instantiate(modelHitEffect);
            particle.SetActive(true);
        }

        particle.transform.position = hitPos;
        StartCoroutine(ReturnToPool(particle));
    }
    public void PlayEffect(Vector3 position)
    {
        GameObject particle;
        if (pool.Count > 0)
        {
            particle = pool.Dequeue();
            particle.SetActive(true);
        }
        else
        {
            particle = Instantiate(particlePrefab);
            particle.SetActive(true);
        }

        particle.transform.position = position;
        StartCoroutine(ReturnToPool(particle));
    }

    private IEnumerator ReturnToPool(GameObject obj)
    {
        yield return new WaitForSeconds(1f); // adjust based on particle duration
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
