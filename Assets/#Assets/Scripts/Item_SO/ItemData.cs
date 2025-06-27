using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="NewItem",menuName ="Mission Food/Junk&FoodItem")]
public class ItemData : ScriptableObject
{
    public Sprite itemImg;
    public string itemName;
    public float coinPrice;
    public float diamondPrice;

}
