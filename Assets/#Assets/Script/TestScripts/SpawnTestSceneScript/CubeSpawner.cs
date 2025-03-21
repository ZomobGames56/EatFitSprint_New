using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject cube;
    [SerializeField]
    int numberOfCubes;
    [SerializeField]
    float radius;
    public GameObject waterMelon;
    [SerializeField]
    bool changeValue;
    Transform lastObj;
    float z = 1.5f;
    [SerializeField]
    GameObject[] parentObj;
    private void Start()
    {
        GameObject parent = new GameObject("ObjectParent");
        parent.transform.position = Vector3.zero;
        // waterMelon.SetActive(false);
        SpawnCubeInCircle(waterMelon);
        lastObj = waterMelon.transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            float x = Random.Range(-4f, 4f);
            int index =Random.Range(0,parentObj.Length-1);

            SpawnCubeInCircle(parentObj[index]);
            z += 2;
            parentObj[index].transform.position = new Vector3(x, 0, lastObj.transform.position.z + z);
        }
    }


    void SpawnCubeInCircle(GameObject ParentObj)
    {
        for (int i = 0; i < numberOfCubes; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfCubes; // Divide the circle into equal parts
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            print(angle);
            Vector3 position = new Vector3(x, waterMelon.transform.position.y, z); // Position on the XY plane

            Instantiate(cube, position, Quaternion.identity, ParentObj.transform); // Spawn cube

        }
    }
}
