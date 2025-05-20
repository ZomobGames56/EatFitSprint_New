using System.Collections.Generic;
using UnityEngine;

public class FoodObjectPooler : MonoBehaviour
{
    public static FoodObjectPooler instance;

    [Header("Object Pooler Setting")]
    [SerializeField]
    private GameObject[] poolFoodObjects; // Renamed for clarity

    [SerializeField]
    private int poolObjectSize = 20;

    public List<GameObject> deactivateFoodObj = new List<GameObject>();

    private void Awake()
    {
        // Singleton pattern with safety check
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Null or empty array check
        if (poolFoodObjects == null || poolFoodObjects.Length == 0)
        {
            Debug.LogError("[FoodObjectPooler] No pool objects assigned!", this);
            return;
        }

        // Prepopulate the pool
        for (int i = 0; i < poolObjectSize; i++)
        {
            CreateAndAddPooledObject();
        }
        
    }

    private GameObject CreateAndAddPooledObject()
    {
        // Ensure Random.Range is inclusive of all array elements
        GameObject obj = Instantiate(poolFoodObjects[Random.Range(0, poolFoodObjects.Length)]);
        if (obj != null)
        {
            obj.SetActive(true);
            //deactivateFoodObj.Add(obj);
            Debug.Log("Object added to the deactivated pool.");
        }
        else
        {
            Debug.LogWarning("[FoodObjectPooler] Failed to create a new pool object.");
        }
        return obj;
    }

    public GameObject GetPooledObject()
    {
        if (deactivateFoodObj.Count > 0)
        {
            // Get and activate the last object in the list
            GameObject obj = deactivateFoodObj[deactivateFoodObj.Count - 1];
            deactivateFoodObj.RemoveAt(deactivateFoodObj.Count - 1); // Remove from the pool
            obj.SetActive(true);
            return obj;
        }

        Debug.Log("[FoodObjectPooler] Pool is empty, creating a new object.");
        return CreateAndAddPooledObject();
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        if (!deactivateFoodObj.Contains(obj))
        {
            deactivateFoodObj.Add(obj);
        }
    }
}
