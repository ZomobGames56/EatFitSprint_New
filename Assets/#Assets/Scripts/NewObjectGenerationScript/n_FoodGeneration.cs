using UnityEngine;
using System.Collections.Generic;
public class n_FoodGeneration : MonoBehaviour
{
    [SerializeField]
    List<GameObject> m_fruits = new List<GameObject>();
    [SerializeField]
    List<GameObject> m_junks = new List<GameObject>();
    [SerializeField]
    float maxX, distanceInZ, distanceInY;
    [SerializeField]
    Transform lastSpawnObj, parent;
    [SerializeField]
    int foodSpawnCount;
    private void OnEnable()
    {
        n_Timer.OnHideParent += HandleHideParent;
    }

    private void OnDisable()
    {
        n_Timer.OnHideParent -= HandleHideParent;
    }
    private void Start()
    {

        for(int i = 0; i <=foodSpawnCount; i++)
        {
            if (i%2==0)
            {
                float x = Random.Range(-maxX,maxX);
                GameObject g_obj = Instantiate(m_fruits[Random.Range(0,m_fruits.Count-1)],parent);
                g_obj.transform.position = new Vector3(x, distanceInY, lastSpawnObj.position.z + distanceInZ);
                lastSpawnObj = g_obj.transform;
            }
            else
            {
                float x = Random.Range(-maxX, maxX);
                GameObject g_obj = Instantiate(m_junks[Random.Range(0, m_junks.Count - 1)], parent);
                g_obj.transform.position = new Vector3(x, distanceInY, lastSpawnObj.position.z + distanceInZ);
                lastSpawnObj = g_obj.transform;
            }
        }
    }


    
    void HandleHideParent()
    {
        parent.gameObject.SetActive(false);
    }
}
