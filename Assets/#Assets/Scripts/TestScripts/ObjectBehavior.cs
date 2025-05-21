using UnityEngine;

public class ObjectBehavior : MonoBehaviour
{
    private void Update()
    {
      if (this.gameObject.activeInHierarchy && transform.position.y <= -5f)
        {
            gameObject.SetActive(false);
            FoodObjectPooler.instance.deactivateFoodObj.Add(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            this.gameObject.SetActive(false);
            FoodObjectPooler.instance.deactivateFoodObj.Add(this.gameObject);
        }
    }

}
