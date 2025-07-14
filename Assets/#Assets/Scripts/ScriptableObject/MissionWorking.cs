using UnityEngine;

public class MissionWorking : MonoBehaviour
{
    
   public  void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            MissionManager.instance.AddMissionProgress("Fruits", 1);
        }
        if (Input.GetKey(KeyCode.E))
        {
            MissionManager.instance.AddMissionProgress("Cake", 1);
        }
        if (Input.GetKey(KeyCode.G))
        {
            MissionManager.instance.AddMissionProgress("Junk", 1);
        }
    }
}
