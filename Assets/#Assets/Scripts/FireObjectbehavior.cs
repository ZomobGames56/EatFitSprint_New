using UnityEngine;

public class FireObjectbehavior : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float fwdSpeed = 2, upForce;
    [SerializeField]
    float life = 3;
    [SerializeField]
    float giveDamge = 0.5f;
    [SerializeField]
  //  int force = 150;
    Transform enemy;
    Rigidbody rb;
    [SerializeField]
    GameObject vfxCollideParticle;
    [SerializeField]
    AudioClip hitClip;
    [SerializeField]
    float adjustVal;
    private void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity = Vector3.forward * fwdSpeed + Vector3.up * upForce;

        Destroy(gameObject, life);

        //transform.position = Vector3.MoveTowards(transform.position, enemy.position, force * Time.deltaTime);
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Enemy")
    //    {
    //        HY_AudioManager.instance.PlayAudioEffectOnce(hitClip);
    //        CharacterShapeChange.instance.takeDamage(giveDamge);
    //        // print("TakingDamageCalled");
    //        //  Instantiate(vfxCollideParticle, gameObject.transform.position, Quaternion.identity);
    //        ParticlePool.instance.HitEffect(new Vector3(transform.position.x,transform.position.y,transform.position.z));
    //        Destroy(gameObject);
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            HY_AudioManager.instance.PlayAudioEffectOnce(hitClip);
            CharacterShapeChange.instance.takeDamage(giveDamge);
            // print("TakingDamageCalled");
            //  Instantiate(vfxCollideParticle, gameObject.transform.position, Quaternion.identity);
            ParticlePool.instance.HitEffect(new Vector3(transform.position.x, transform.position.y, transform.position.z));
            Destroy(gameObject);
        }
    }
}
