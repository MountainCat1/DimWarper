using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGameProgressOnHeight : OnHeightBehaviour
{
    [SerializeField] private int newGameProgressValue = 1;


    protected override void Action()
    {
        if (GameDataManager.Data.gameProgress >= newGameProgressValue) return;
        
        
        GameDataManager.Data.gameProgress = newGameProgressValue;
        GameDataManager.SaveData();
    }
}
