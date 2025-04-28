using System.Collections;
using UnityEngine;

public class FirstTimeUser : MonoBehaviour
{
    [SerializeField]
    GameObject clickImg;
    
    private void Start()
    {
        clickImg.SetActive(false);
        if (!SaveDataManager.instance.VariableExist("FirstTimeEnter"))
        {
            StartCoroutine(WaitToActiveClickImg());
        }
        else
        {
            clickImg.SetActive(false);
        }
    }

    IEnumerator WaitToActiveClickImg()
    {
        yield return new WaitForSeconds(1);
        clickImg.SetActive(true);
    }
  
}
