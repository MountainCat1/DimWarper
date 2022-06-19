using System;
using System.Collections;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

public class SteamAchievementForProgress : MonoBehaviour
{
    public List<SteamAchievementForProgressModel> Achievements = new List<SteamAchievementForProgressModel>();

    private void Start()
    {
        Debug.Log("Checking for steam achievements for game progress...");

        var data = GameDataManager.Data;
        
        foreach (var achievement in Achievements)
        {
            if (achievement.requiredGameProgress >= data.gameProgress)
            {
                Debug.Log($"Achievement unlocked! {achievement.steamAchievementId}");

                UnlockSteamAchievement(achievement.steamAchievementId);
            }
        }
    }


    private void UnlockSteamAchievement(string id)
    {
        var steamAchievement = new Steamworks.Data.Achievement(id);
        steamAchievement.Trigger();
    }
}

[System.Serializable]
public struct SteamAchievementForProgressModel
{
    public string steamAchievementId;
    public int requiredGameProgress;
}