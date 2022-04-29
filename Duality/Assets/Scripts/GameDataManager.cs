using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    private const int MaxSavedScores = 10;
    
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

    public static void AddHighScore(string levelName, float height, float time)
    {
        if (Data.highScores.Count >= MaxSavedScores)
        {
            var lowestHighScore = Data.highScores
                .OrderBy(x => x.height)
                .First();

            if (lowestHighScore.height < height)
                Data.highScores.Remove(lowestHighScore);
            else
                return;
        }

        Data.highScores.Add(new HighScore()
        {
            dateTimeText = DateTime.Now.ToString(CultureInfo.InvariantCulture),
            height = height,
            time = time,
            levelName = levelName
        });
        
        SaveData();
    }
}

[Serializable]
public class GameData
{
    public const int MaxGameDataProgress = 8;
    
    public int gameProgress = 0;

    public List<HighScore> highScores = new List<HighScore>();
}

[Serializable]
public class HighScore
{
    public float height;
    public float time;
    public string levelName;

    public string dateTimeText;
    public DateTime DateTime => DateTime.ParseExact(dateTimeText, "MM/dd/yyyy HH:mm:ss", null);
}