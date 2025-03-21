using UnityEngine;
using UnityEngine.UI;

public class CharacterShapeChange : MonoBehaviour
{
    public static CharacterShapeChange instance;
    [SerializeField]
    SkinnedMeshRenderer playersSkin;
    public float myValue = 0;
    //[SerializeField]
    public Slider fitNessBar;
    //public int coinsUpdater;
    int rndGenratorNum, rndStartSlimVal, rndStartFatVal;
    [SerializeField]
    AudioClip hitSound;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        // transform.position = new Vector3(transform.position.x, 18, transform.position.z);
        playersSkin = GetComponentInChildren<SkinnedMeshRenderer>();
        rndGenratorNum = Random.Range(0, 2);
        // print(rndGenratorNum);
        if (rndGenratorNum == 0)
        {
            // Means Character is slim, genrate slim bar value.
            rndStartSlimVal = Random.Range(-150, -70);
            playersSkin.SetBlendShapeWeight(0, rndStartSlimVal);
            fitNessBar.value = rndStartSlimVal;
        }
        if (rndGenratorNum == 1)
        {
            // Means Character is fat, genrate fat bar value.
            rndStartFatVal = Random.Range(70, 151);
            playersSkin.SetBlendShapeWeight(0, rndStartFatVal);
            fitNessBar.value = rndStartFatVal;

        }
    }

    public void takeDamage(float damageVal)
    {
        fitNessBar.value += damageVal;

        playersSkin.SetBlendShapeWeight(0, fitNessBar.value);
        // HY_AudioManager.instance.PlayAudioEffectOnce(hitSound);


    }

}