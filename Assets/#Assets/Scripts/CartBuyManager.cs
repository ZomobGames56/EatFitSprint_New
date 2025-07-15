using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CartData
{
    public GameObject cartObject;
    public int cost;
    public bool isUnlocked;
}

public class CartBuyManager : MonoBehaviour
{

    public static CartBuyManager instance;
    public CartData[] carts;

    public Button leftButton;
    public Button rightButton;
    public Button buyButton;

    private int currentIndex = 0;

    [SerializeField]
    TextMeshProUGUI buyButtonText;
    // private int CoinUpdateManager.GetCoin() = 100; // Replace this with your real coin system
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        // Unlock and show 0th cart
        carts[0].isUnlocked = true;

        leftButton.onClick.AddListener(() => Navigate(-1));
        rightButton.onClick.AddListener(() => Navigate(1));
        buyButton.onClick.AddListener(BuyCurrent);

        ShowCart(currentIndex);
    }

    private void Navigate(int direction)
    {
        currentIndex += direction;

        // Wrap around
        if (currentIndex < 0) currentIndex = carts.Length - 1;
        if (currentIndex >= carts.Length) currentIndex = 0;

        ShowCart(currentIndex);
    }

    private void ShowCart(int index)
    {
        for (int i = 0; i < carts.Length; i++)
        {
            carts[i].cartObject.SetActive(i == index);
        }

        // Buy button logic
        if (carts[index].isUnlocked)
        {
            //buyButton.gameObject.SetActive(false);
            buyButtonText.text = "SELECTED";

        }
        else
        {
            buyButton.gameObject.SetActive(true);
            buyButton.interactable = CoinsUpdateManager.GetCoin() >= carts[index].cost;
            buyButtonText.text = carts[index].cost.ToString();
        }
    }

    private void BuyCurrent()
    {
        var current = carts[currentIndex];
        if (current.isUnlocked) return;

        if (CoinsUpdateManager.GetCoin() >= current.cost)
        {
            CoinsUpdateManager.SpendCoins(current.cost);
            current.isUnlocked = true;
            ShowCart(currentIndex); // Refresh
        }
    }

    public int GetSelectedCartIndex()
    {
        return currentIndex;
    }
}
