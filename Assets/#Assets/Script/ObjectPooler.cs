using DG.Tweening;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField]
    private GameObject platform;
    [SerializeField]
    List<GameObject> platformObjs = new List<GameObject>();
    [SerializeField]
    List<GameObject> deactivateObj= new List<GameObject>();
    [Header("Debug")]
    [SerializeField]
    Stack<GameObject> stacks = new Stack<GameObject>();  
    [SerializeField]
    private Transform player;
    Transform lastPlatform;
    private void Start()
    {
        lastPlatform = platformObjs[platformObjs.Count - 1].transform;
        for (int i = 0; i < 10; i++)
        {
            GameObject go = Instantiate(platform);
            go.SetActive(false);
            platformObjs.Add(go);
            stacks.Push(go);
        }
    }
    private void Update()
    {
        if (Mathf.Abs(PlayerDistacneFromLastObj) < 500)
        {
            GetPoolObject();
        }
        foreach (GameObject backObj in platformObjs)
        {
            if (backObj.transform.position.z - player.position.z <= -18f)
            {
                backObj.SetActive(false);
                platformObjs.Remove(backObj);
                deactivateObj.Add(backObj);
                break;
            }
        }   

        if (Input.GetKeyDown(KeyCode.P))
        {
            stacks.Pop();
        }
    }
    void GetPoolObject()
    {
        if (deactivateObj.Count !=0)
        {
            GameObject go = deactivateObj[0];
            go.transform.position = new Vector3(0, 0, lastPlatform.position.z + 10);
            deactivateObj.Remove(go);
            platformObjs.Add(go);
            go.SetActive(true);
            lastPlatform = go.transform;
           // Debug.Log("Got Deaactivate object");
        }
        else
        {
            GameObject newObj = Instantiate(platform);
           newObj.transform.position = new Vector3(0, 0, lastPlatform.position.z + 10);
            platformObjs.Add(newObj);
            lastPlatform = newObj.transform;
           // Debug.Log("Got New object");
        }
    }
    float PlayerDistacneFromLastObj => (player.position.z - lastPlatform.position.z);
    void SpawnGetObject()
    {
        GameObject go = GetDeactivateObject();
        go.transform.position = new Vector3(0, 0, platformObjs[platformObjs.Count - 1].transform.position.z + 10);
        go.SetActive(true);

    }
   GameObject GetDeactivateObject()
    {
        foreach (GameObject obj in platformObjs)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        GameObject newObj = Instantiate(platform);
        newObj.SetActive(false);
        platformObjs.Add(newObj);
        return newObj;
    }
    //GameObject PoolPlatform()
    //{
    //    foreach (GameObject obj in platformObjs)
    //    {
    //        if (!obj.activeInHierarchy)
    //        {
    //            return obj;
    //        }
    //    }
    //    GameObject newObj = Instantiate(platform);
    //    newObj.SetActive(false);
    //    platformObjs.Add(newObj);
    //    return newObj;
    //    //for (int i = platformObjs.Count-1; i > 0; i--)
    //    //{
    //    //    if (!platformObjs[i].activeInHierarchy)
    //    //    {
    //    //        platformObjs[i].transform.position = new Vector3(0, 0, platformObjs[platformObjs.Count - 1].transform.position.z + 10);
    //    //        platformObjs[i].SetActive(true);
    //    //    }
    //    //    else
    //    //    {

    //    //    }
    //    //}
        
    //}




}