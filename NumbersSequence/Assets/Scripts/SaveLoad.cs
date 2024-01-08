using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoad 
{
    private const string FileName = "level_data";

    /// <summary>
    /// Read json file from a Resources folder and parses it into a LevelList class
    /// </summary>
    /// <returns>levelList - data class from json file</returns>
    public static LevelList ReadJsonData()
    {
        LevelList levelList = new LevelList();

        try
        {
            // Load json file
            TextAsset jsonFile = Resources.Load<TextAsset>("Text/"+FileName);

            // Parse the JSON data 
            levelList = JsonUtility.FromJson<LevelList>(jsonFile.text);

        }
        catch (System.Exception e)
        {
            Debug.LogError("Exception: " + e.Message);
        }
        return levelList;

    }


    
}

[System.Serializable]
public class LevelData
{
    public List<string> level_data;
}

[System.Serializable]
public class LevelList
{
    public List<LevelData> levels;
}