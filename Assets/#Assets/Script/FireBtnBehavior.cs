
using System.Collections.Generic;
using UnityEngine;

public class FireBtnBehavior : MonoBehaviour
{
    [SerializeField]
    List<GameObject> foodList = new List<GameObject>();
    [SerializeField]
    List<GameObject> junkFoodList = new List<GameObject>();

    [SerializeField]
    Transform spawnPoint;
    [SerializeField]
    AudioClip throwSound;
    public void FoodButton()
    {
        HY_AudioManager.instance.PlayAudioEffectOnce(throwSound);
        if (GameManager.instance.fruitCount > 0)
        {
            Instantiate(foodList[Random.Range(0, foodList.Count)], spawnPoint.position, Quaternion.identity);
            
            GameManager.instance.fruitCount--;
        }
    }

    public void JunkFoodButton()
    {
        HY_AudioManager.instance.PlayAudioEffectOnce(throwSound);

        if (GameManager.instance.junkFoodCount > 0)
        {
            Instantiate(junkFoodList[Random.Range(0, junkFoodList.Count)],spawnPoint.position,Quaternion.identity);
            GameManager.instance.junkFoodCount--;
        }
    }
}
