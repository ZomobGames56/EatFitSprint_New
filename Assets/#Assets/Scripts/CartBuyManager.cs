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
    [SerializeField]
    AudioClip buttonClip;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        LoadCartUnlockStatus();

        leftButton.onClick.AddListener(() => Navigate(-1));
        rightButton.onClick.AddListener(() => Navigate(1));
        buyButton.onClick.AddListener(BuyCurrent);

        ShowCart(currentIndex);
    }

    private void Navigate(int direction)
    {
        currentIndex += direction;
        HY_AudioManager.instance.PlayAudioEffectOnce(buttonClip);
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

        if (carts[index].isUnlocked)
        {
            buyButtonText.text = "SELECTED";
            buyButton.interactable = false;
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
            SaveCartUnlockStatus(currentIndex);
            ShowCart(currentIndex);
        }
    }

    public int GetSelectedCartIndex()
    {
        return currentIndex;
    }

    private void SaveCartUnlockStatus(int index)
    {
        PlayerPrefs.SetInt("cart_unlocked_" + index, 1);
        PlayerPrefs.Save();
    }

    private void LoadCartUnlockStatus()
    {
        for (int i = 0; i < carts.Length; i++)
        {
            // Cart 0 should always be unlocked
            if (i == 0 || PlayerPrefs.GetInt("cart_unlocked_" + i, 0) == 1)
                carts[i].isUnlocked = true;
            else
                carts[i].isUnlocked = false;
        }
    }
}
