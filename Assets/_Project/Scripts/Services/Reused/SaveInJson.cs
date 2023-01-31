using UnityEngine;

public class SaveInJson
{
    private const string SaveKey = "SaveData_0";
    private SaveData _saveData;

    public SaveData GetData
    {
        get
        {
            if (_saveData == null) 
                return null;
 
            return _saveData;
        }
    }

    public void Save()
    {
        if (_saveData == null)
            return;

        PlayerPrefs.SetString(SaveKey, JsonUtility.ToJson(_saveData));
    }

    public void Load()
    {
        string data = PlayerPrefs.GetString(SaveKey);
        if (data.Length != 0)
            _saveData = JsonUtility.FromJson<SaveData>(data);
        else
            _saveData = new SaveData();
    }
}