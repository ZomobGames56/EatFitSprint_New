using TMPro;
using UnityEngine;

public class CoinsUpdateManager : MonoBehaviour
{
    private static CoinsUpdateManager instance;

    [SerializeField]
    TextMeshProUGUI coinText;
    int currentCoins;
    private void Awake()
    {
        instance = this;
    }

    private void OnDestroy()
    {
        Destroy(gameObject);
    }
    private void Start()
    {
        LoadCoins();
        UpdateUI();
    }
   
    public static void AddCoins(int amount)
    {
        instance.currentCoins += amount;
        instance.UpdateUI();
        instance.SaveCoins();
    }
    public static void SpendCoins(int amount)
    {
        if (instance.currentCoins >= amount)
        {
            instance.currentCoins -= amount;
            instance.UpdateUI();
            instance.SaveCoins();
        }
        else
        {
            Debug.LogError("Not have Enough coins");
        }
    }
    void UpdateUI()
    {
        if (coinText != null)
        {
            coinText.text = currentCoins.ToString();
        }
        else
        {
            Debug.LogError("CoinText is Not Assinged to CoinManager Object");
        }
    }

    void SaveCoins()
    {
        SaveDataManager.instance.SaveData("Coins", currentCoins);
    }
    void LoadCoins()
    {
        currentCoins = SaveDataManager.instance.GetSavedData("Coins");
    }
}
