using TMPro;
using UnityEngine;

public class CoinsUpdateManager : MonoBehaviour
{
    private static CoinsUpdateManager instance;

    [SerializeField]
    TextMeshProUGUI coinText;
    int currentCoins;

    float coinInFloat;
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

    public static int GetCoin()
    {
        return instance.currentCoins;
    }
    void UpdateUI()
    {
        #region //Not in use now
        //if (coinText != null)
        //{
        //    coinText.text = currentCoins.ToString();
        //}
        //else
        //{
        //    Debug.LogError("CoinText is Not Assinged to CoinManager Object");
        //}
        #endregion

        coinInFloat = currentCoins;
        if (currentCoins >= 10000000)
        {
            coinText.text = (coinInFloat / 10000000).ToString("F") + "B";
        }
        else if (currentCoins < 10000000 && currentCoins >= 1000000)
        {
            coinText.text = (coinInFloat / 1000000).ToString("F") + "M";
        }
        else if (currentCoins < 1000000 && currentCoins >= 1000)
        {
            coinText.text = (coinInFloat / 1000).ToString("F") + "K";

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
