using UnityEngine;

public class NewGirlCharacterShapeChange : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer[] skinMesh;
    int amount;
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
             amount -= 10;
            foreach (var skin in skinMesh)
            {
                skin.SetBlendShapeWeight(0, amount);    
            }
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            amount += 10;
            foreach (var skin in skinMesh)
            {
                skin.SetBlendShapeWeight(0, amount);
            }
        }
    }
}
