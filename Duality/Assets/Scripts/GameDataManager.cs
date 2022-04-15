using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameData Data { get; private set; }
    
    private static readonly string directory;
    private static readonly string fileName;
    
    static GameDataManager()
    {
        directory = Application.persistentDataPath;
        fileName = "GameData.json";
    }


    /// <summary>
    /// Sets GameData to default and saves it afterwards
    /// </summary>
    public static void ResetData()
    {
        Data = new GameData();
        SaveData();
    }
    public static void LoadData()
    {
        string json = string.Empty;
        string fullPath = Path.Combine(directory, fileName);
        
        // If file doesnt exist, just create new GameData
        if (!File.Exists(fullPath))
        {
            Debug.Log("Game data not found! Creating new one...");
            Data = new GameData();
            return;
        }
        
        using (var reader = new StreamReader(fullPath))
        {
            json = reader.ReadToEnd();
        }

        Data = JsonUtility.FromJson<GameData>(json);
        Debug.Log($"Game data loaded (progress: {Data.gameProgress})...");
    }

    public static void SaveData()
    {
        if (Data == null)
        {
            throw new NullReferenceException("Tried to save Game Data, but it was null");
        }
        var json = JsonUtility.ToJson(Data);

        Directory.CreateDirectory(directory);
        
        var fullPath = Path.Combine(directory, fileName);
        using (var writer = new StreamWriter(fullPath))
        {
            writer.Write(json);
        }
        
        Debug.Log($"Game data saved {directory}");
    }
}

[System.Serializable]
public class GameData
{
    public int gameProgress = 0;
}
