using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    public static SaveDataManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public int GetSavedData(string keyName)
    {
        return PlayerPrefs.GetInt(keyName);
    }
    public void SaveData(string name, int value)
    {
        PlayerPrefs.SetInt(name, value);
    }
    public bool VariableExist(string name)
    {
        return PlayerPrefs.HasKey(name);
    }
}
