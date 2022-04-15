using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Enables the button assigned to the object depending if game progress reach specified level
/// 
/// </summary>
public class EnableButtonOnGameProgress : MonoBehaviour
{
    [SerializeField] private int gameProgressRequired;

    void Start()
    {
        var button = GetComponent<Button>();
        
        button.interactable = gameProgressRequired <= GameDataManager.Data.gameProgress;
    }
}
