using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterShapeChange : MonoBehaviour
{
    public static CharacterShapeChange instance;
    
    [SerializeField]
    SkinnedMeshRenderer[] girlSkinMeshRenderer;
    public float myValue = 0;
    //[SerializeField]
    public Slider fitNessBar, toShowplayerBar;
    //public int coinsUpdater;
    int rndGenratorNum, rndStartSlimVal, rndStartFatVal;
    [SerializeField]
    AudioClip hitSound;
    [SerializeField]
    GameObject fruitTickImg,fruitCrossImg, junkTickImg,junkCrossImg;
    [SerializeField]
    TextMeshProUGUI fruitTxt, junkTxt;
   
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        fruitTickImg.gameObject.SetActive(false);
        junkTickImg.gameObject.SetActive(false);
    }
    void Start()
    {
        // transform.position = new Vector3(transform.position.x, 18, transform.position.z);
        //SplayersSkin = GetComponentInChildren<SkinnedMeshRenderer>();
        rndGenratorNum = Random.Range(0, 2);
        // print(rndGenratorNum);
        if (rndGenratorNum == 0)
        {
            // Means Character is slim, generate slim bar value.
            rndStartSlimVal = Random.Range(-99, -70);
           // playersSkin.SetBlendShapeWeight(0, rndStartSlimVal);
            CharacterBlendShapesUPD(rndStartSlimVal);
            fitNessBar.value = rndStartSlimVal;
            toShowplayerBar.value = rndStartSlimVal;
            print(rndStartSlimVal);
            ImageObjActiveState(false);
            fruitTxt.text = "Avoid Fruit!";
            ////fruitTxt.color = Color.red;
            junkTxt.text = "Collect Junk Food \n To \n Get Fit!";

        }
        if (rndGenratorNum == 1)
        {
            // Means Character is fat, genrate fat bar value.
            rndStartFatVal = Random.Range(60, 100);
           // playersSkin.SetBlendShapeWeight(0, rndStartFatVal);
            CharacterBlendShapesUPD(rndStartFatVal);
            fitNessBar.value = rndStartFatVal;
            toShowplayerBar.value = rndStartFatVal;
            print(rndStartFatVal);
            ImageObjActiveState(true);
            junkTxt.text = "Avoid Junk Food!";
           // junkTxt.color = Color.red;
            fruitTxt.text = "Collect Fruit \n To \n Get Fit!";
        }
    }
    void ImageObjActiveState(bool canTrue)
    {
        fruitCrossImg.SetActive(!canTrue);
        fruitTickImg.SetActive(canTrue);
        junkTickImg.SetActive(!canTrue);
        junkCrossImg.SetActive(canTrue);
    }
    public void takeDamage(float damageVal)
    {
        fitNessBar.value += damageVal;
        //playersSkin.SetBlendShapeWeight(0, fitNessBar.value);
        CharacterBlendShapesUPD(fitNessBar.value);
         HY_AudioManager.instance.PlayAudioEffectOnce(hitSound);
    }
    public void CharacterBlendShapesUPD(float val)
    {
        foreach (var skins in girlSkinMeshRenderer)
        {
            skins.SetBlendShapeWeight(0, val);  
        }
    }
}