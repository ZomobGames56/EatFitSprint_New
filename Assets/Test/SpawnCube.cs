using UnityEngine;

public class SpawnCube : MonoBehaviour
{
    [SerializeField] GameObject cube;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject obj = Instantiate(cube);
            obj.transform.position = Vector3.zero;
        }

        if (Input.GetKey(KeyCode.P))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.forward, out hit))
            {
                if (hit.collider != null && hit.collider.tag == "Player")
                {
                    Vector3 dis = transform.position - hit.collider.transform.position;
                    hit.collider.gameObject.transform.position = Vector3.Lerp(transform.position, dis, 500 * Time.deltaTime);
                }
            }





        }
    }
}
