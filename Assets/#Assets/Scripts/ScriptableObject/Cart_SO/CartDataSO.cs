using UnityEngine;

[CreateAssetMenu(fileName = "Cart", menuName = "Carts/Cart Data")]
public class CartDataSO : ScriptableObject
{
    public GameObject cartObjectInScene; // reference to scene cart object
    public int cost;
    [HideInInspector] public bool isUnlocked; // runtime only, loaded from PlayerPrefs
}
