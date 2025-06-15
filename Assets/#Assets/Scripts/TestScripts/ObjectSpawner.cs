using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Object Pools")]
    public List<GameObject> foodPrefabs;
    public List<GameObject> junkFoodPrefabs;
    public List<GameObject> obstaclePrefabs;

    [Header("Spawn Settings")]
    public float spawnDistance = 10f; // Distance between objects
    public float triggerDistance = 50f; // Distance to trigger new spawn
    public Transform playerTransform;

    private Transform lastSpawnedObject;
    private Queue<GameObject> activeObjects = new Queue<GameObject>();

    private void Start()
    {
        lastSpawnedObject = playerTransform;
        SpawnInitialObjects();
    }

    private void Update()
    {
        if (playerTransform.position.z - lastSpawnedObject.position.z < triggerDistance)
        {
            SpawnObject();
        }
    }

    private void SpawnInitialObjects()
    {
        for (int i = 0; i < 20; i++) // Spawn initial set of objects
        {
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        float xPos = Random.Range(-3.5f, 3.5f);
        Vector3 spawnPos = new Vector3(xPos, 1, lastSpawnedObject.position.z + spawnDistance);

        GameObject objToSpawn = SelectObjectToSpawn();
       // GameObject pooledObj = ObjectPooler.Instance.GetPooledObject(objToSpawn);

        //if (pooledObj != null)
        //{
        //    pooledObj.transform.position = spawnPos;
        //    pooledObj.SetActive(true);
        //    activeObjects.Enqueue(pooledObj);
        //    lastSpawnedObject = pooledObj.transform;
        //}

        // Optionally, recycle objects to maintain performance
        if (activeObjects.Count > 30) // Keep max 30 objects active
        {
            GameObject oldObj = activeObjects.Dequeue();
            oldObj.SetActive(false);
        }
    }

    private GameObject SelectObjectToSpawn()
    {
        int random = Random.Range(0, 6);
        if (random == 0) // Higher chance for food and junk food
        {
            return obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)];
        }
        else if (random < 4)
        {
            return foodPrefabs[Random.Range(0, foodPrefabs.Count)];
        }
        else
        {
            return junkFoodPrefabs[Random.Range(0, junkFoodPrefabs.Count)];
        }
    }
}
