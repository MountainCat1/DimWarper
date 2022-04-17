using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class GameDataLoader : MonoBehaviour
{
    [SerializeField] private bool resetGameData = false;
    void Awake()
    {
        if(resetGameData)
            GameDataManager.ResetData();
        
        if (GameDataManager.Data == null)
        {
            GameDataManager.LoadData();
        }
        
        GameDataManager.SaveData();
    }
}
