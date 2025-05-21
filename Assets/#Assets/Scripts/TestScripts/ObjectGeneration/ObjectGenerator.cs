using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    [SerializeField]
    List<GameObject> objectToSpawn = new List<GameObject>();

    [SerializeField]
    List<GameObject> fruitGameObject = new List<GameObject>();

    [SerializeField]
    List<GameObject> junkFoodGameObject = new List<GameObject>();

    [SerializeField]
    GameObject objPrefab;
    [SerializeField]
    int circleCount = 6;
    [SerializeField]
    float radius;

    [SerializeField]
    Transform target;
    Transform lastSpawned;


    private void Awake()
    {
        lastSpawned = target;
        CirclePattern();
    }
    private void Start()
    {
        Spawn();
    }
    void CirclePattern()
    {
        Vector3 center = Vector3.zero;
        GameObject go = new GameObject("Circle");
        for (int i = 0; i < circleCount; i++)
        {
            radius = circleCount / 3f;
            float angle = i * Mathf.PI * 2 / circleCount;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            Vector3 pos = center + new Vector3(x, 0, z);
            Instantiate(objPrefab, pos, Quaternion.identity, go.transform);
        }
        Instantiate(objPrefab, center, Quaternion.identity, go.transform);
        objectToSpawn.Add(go);
        go.transform.position = new Vector3(-17f, -28f, -850f);
    }
    void Spawn()
    {
        for (int i = 0; i < 60; i++)
        {
            if (objectToSpawn != null || objectToSpawn.Count > 0)
            {
                GameObject obj = Instantiate(objectToSpawn[0]);
                 obj.transform.position= new Vector3(Random.Range(-17, 18), target.position.y, lastSpawned.position.z + 25);
                lastSpawned = obj.transform;

            }
        }
    }
    void Pyramid()
    {

    }

    void HexagonaGrid()
    {

    }

    void Grid()
    {
        // for(int i = 0; i<)
    }
}
