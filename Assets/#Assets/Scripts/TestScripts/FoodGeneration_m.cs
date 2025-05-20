using UnityEngine;
using System.Collections.Generic;


public class FoodGeneration_m : MonoBehaviour
{
    [Header("Food Object/Prefab")]
    [SerializeField]
    List<GameObject> spawnObj = new List<GameObject>();
    [Header("Player Reference")]
    [SerializeField]
    Transform player_Ref;
    Transform lastSpawedObj;
    bool isGenerateFood;

    [Header("MaxX Value for the Generation")]
    [SerializeField]
    float maxX=9;
    [Header("MaxZ for Spawn Object distance from player")]
    [SerializeField]
    float maxZ=10;

    [SerializeField]
    Transform lastRoad;
    [SerializeField]
    float distance;
    private void Start()
    {
        lastSpawedObj = player_Ref;
        GameObject obj = Instantiate(spawnObj[0]);
        obj.transform.position = new Vector3(3.5f, 1, lastSpawedObj.position.z + 10f);
        lastSpawedObj = obj.transform;
        isGenerateFood = true;
    }
    private void Update()
    {
        if (Mathf.Abs(player_Ref.transform.position.z - lastRoad.position.z) < distance)
        {
            isGenerateFood=false;
        }
        if (isGenerateFood)
        {
            if (Mathf.Abs(player_Ref.transform.position.z - lastSpawedObj.position.z) < 15000)
            {
                float x = Random.Range(-maxX, maxX);
                //int index = Random.Range(0, spawnObj.Count);

                GameObject obj = FoodObjectPooler.instance.GetPooledObject();
                FoodObjectPooler.instance.deactivateFoodObj.Remove(obj);
                obj.transform.position = new Vector3(x, 1, lastSpawedObj.position.z + maxZ);
                obj.SetActive(true);
                lastSpawedObj = obj.transform;
            }
        }
    }




}
