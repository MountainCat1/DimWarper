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
    
    public static void LoadData()
    {
        string json = string.Empty;
        string fullPath = Path.Combine(directory, fileName);
        
        // If file doesnt exist, just create new GameData
        if (!File.Exists(fullPath))
        {
            Data = new GameData();
            return;
        }
        
        using (var reader = new StreamReader(fullPath))
        {
            json = reader.ReadToEnd();
        }

        Data = JsonUtility.FromJson<GameData>(json);
    }

    public static void SaveData()
    {
        if (Data == null)
        {
            throw new NullReferenceException("Tried to save Game Data, but it was null");
        }
        string json = JsonUtility.ToJson(Data);

        Directory.CreateDirectory(directory);
        
        using (var writer = new StreamWriter(Path.Combine(directory, fileName)))
        {
            writer.Write(json);
        }
    }
}

[System.Serializable]
public class GameData
{
    public int levelProgress = 0;
}
