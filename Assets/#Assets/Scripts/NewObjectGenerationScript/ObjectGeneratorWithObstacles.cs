using System.Collections.Generic;
using UnityEngine;

public class ObjectGeneratorWithObstacles : MonoBehaviour
{
    public enum FoodType { Fruit, Junk, Obstacle }

    [Range(0, 1)] public float fruitProbability = 0.4f;
    [Range(0, 1)] public float junkProbability = 0.4f;
    // obstacleProbability = 1 - (fruit + junk)

    public List<GameObject> m_fruits;
    public List<GameObject> m_junks;
    public List<GameObject> m_obstacles;

    public int foodSpawnCount = 10;
    public float maxX = 3.5f;
    public float distanceInY = 0f;
    public float distanceInZ = 5f;
    public Transform parent;
    public Transform lastSpawnObj;

    private FoodType? lastType = null;

    private void OnEnable()
    {
        n_Timer.OnHideParent += HideParent;
    }
    private void OnDisable()
    {
        n_Timer.OnHideParent += HideParent;

    }
    private void Start()
    {
        SpawnItems();
    }
    void SpawnItems()
    {
        for (int i = 0; i < foodSpawnCount; i++)
        {
            FoodType chosenType = GetRandomType(lastType);
            GameObject prefabToSpawn = null;

            switch (chosenType)
            {
                case FoodType.Fruit:
                    prefabToSpawn = m_fruits[Random.Range(0, m_fruits.Count)];
                    break;
                case FoodType.Junk:
                    prefabToSpawn = m_junks[Random.Range(0, m_junks.Count)];
                    break;
                case FoodType.Obstacle:
                    prefabToSpawn = m_obstacles[Random.Range(0, m_obstacles.Count)];
                    break;
            }

            float x = (int)Random.Range(-maxX, maxX);
            GameObject g_obj = Instantiate(prefabToSpawn, parent);
            g_obj.transform.position = new Vector3(x, distanceInY, lastSpawnObj.position.z + distanceInZ);
            lastSpawnObj = g_obj.transform;

            lastType = chosenType;
        }
    }
    void HideParent()
    {
        if (parent != null)
            parent.gameObject.SetActive(false);
    }
    FoodType GetRandomType(FoodType? last)
    {
        float rand = Random.value;
       // print(rand);
        // Adjusted to prevent same type repetition if needed
        if (last.HasValue)
        {
            // Reduce chance for last type
            switch (last.Value)
            {
                case FoodType.Fruit:
                    if (rand < junkProbability) return FoodType.Junk;
                    else return FoodType.Obstacle;
                case FoodType.Junk:
                    if (rand < fruitProbability) return FoodType.Fruit;
                    else return FoodType.Obstacle;
                case FoodType.Obstacle:
                    if (rand < fruitProbability) return FoodType.Fruit;
                    else return FoodType.Junk;
            }
        }

        // Normal probability
        if (rand < fruitProbability)
            return FoodType.Fruit;
        else if (rand < fruitProbability + junkProbability)
            return FoodType.Junk;
        else
            return FoodType.Obstacle;
    }
}
