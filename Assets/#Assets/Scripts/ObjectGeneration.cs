using System.Collections.Generic;
using UnityEngine;

public class ObjectGeneration : MonoBehaviour
{
    public Transform player; // Player reference
    public float spawnDistance = 10f; // Distance ahead of the player to spawn objects
    public float spawnInterval = 3f; // Distance between objects
    public int initialObjects = 10; // Number of objects in the pool

    public string[] objectTags = { "Fruit", "JunkFood", "Obstacle" }; // Tags for different objects
    [SerializeField]
    private List<GameObject> spawnedObjects = new List<GameObject>();

    void Start()
    {
        // Spawn initial objects
        for (int i = 0; i < initialObjects; i++)
        {
            SpawnNextObject();
        }
    }

    void Update()
    {
        // Check if the first object is far behind the player
        if (spawnedObjects.Count > 0 && player.position.z > spawnedObjects[0].transform.position.z + spawnDistance)
        {
            RecycleObject();
        }
    }

    void SpawnNextObject()
    {
        // Get a random object type (Fruit, Junk, or Obstacle)
        string randomTag = objectTags[Random.Range(0, objectTags.Length)];

        Vector3 spawnPosition;
        if (spawnedObjects.Count > 0)
        {
            // Position in front of the last object
            spawnPosition = spawnedObjects[spawnedObjects.Count - 1].transform.position + Vector3.forward * spawnInterval;
        }
        else
        {
            // First spawn position in front of the player
            spawnPosition = player.position + Vector3.forward * spawnDistance;
        }

        // Randomize horizontal position
        spawnPosition.x += Random.Range(-3f, 3f);

        // Get an object from the pool
        //GameObject newObject = ObjectPooler.Instance.SpawnFromPool(randomTag, spawnPosition, Quaternion.identity);

        // Store the object in the list
       // spawnedObjects.Add(newObject);
    }

    void RecycleObject()
    {
        // Get the first object
        GameObject oldObject = spawnedObjects[0];

        // Move it forward to the new spawn position
        oldObject.transform.position = spawnedObjects[spawnedObjects.Count - 1].transform.position + Vector3.forward * spawnInterval;

        // Randomize horizontal position
        oldObject.transform.position += new Vector3(Random.Range(-3f, 3f), 0, 0);

        // Move it to the end of the list
        spawnedObjects.RemoveAt(0);
        spawnedObjects.Add(oldObject);
    }
}
