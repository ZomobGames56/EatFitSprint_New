using System.Collections.Generic;
using UnityEngine;

public class StartFoodGeneration : MonoBehaviour
{
    [SerializeField]
    List<GameObject> spawnObjetList = new List<GameObject>();
    [SerializeField]
    Transform obstacle;
    [SerializeField]
    int lenght = 120;

    [SerializeField]
    float maxX = 9, maxZ = 15;

    [SerializeField]
    Transform lastSpawedObj;

    [SerializeField]
    Transform parent;
    void Start()
    {

        for (int i = 0; i < lenght; i++)
        {
            float x = Random.Range(-maxX, maxX);
            if (i % 8 == 0)
            {
                if (i != 0)
                {
                    int value = Random.Range(0, 2);
                    
                    if (value == 0)
                    {
                        GameObject obs_Obj = Instantiate(obstacle.gameObject,parent);
                        obs_Obj.transform.position = new Vector3(-10.5f, 1, lastSpawedObj.position.z + maxZ);
                        lastSpawedObj = obs_Obj.transform;
                    }
                    if (value == 1)
                    {
                        GameObject obs_Obj = Instantiate(obstacle.gameObject,parent);
                        obs_Obj.transform.position = new Vector3(5f, 1, lastSpawedObj.position.z + maxZ);
                        lastSpawedObj = obs_Obj.transform;
                    }
                    
                }
                else
                {
                    GameObject obj = Instantiate(spawnObjetList[Random.Range(0, spawnObjetList.Count - 1)],parent);
                    obj.transform.position = new Vector3(x, 3.5f, lastSpawedObj.position.z + maxZ);
                    lastSpawedObj = obj.transform;
                }
                
            }
            else
            {
                GameObject obj = Instantiate(spawnObjetList[Random.Range(0, spawnObjetList.Count - 1)], parent);
                obj.transform.position = new Vector3(x, 3.5f, lastSpawedObj.position.z + maxZ);
                lastSpawedObj = obj.transform;
            }

        }
    }

}
