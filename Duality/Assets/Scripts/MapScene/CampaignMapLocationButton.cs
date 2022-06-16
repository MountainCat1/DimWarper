
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Enables the button assigned to the object depending if game progress reach specified level
/// 
/// </summary>
public class CampaignMapLocationButton : MonoBehaviour
{
    [SerializeField] private int gameProgressRequired;

    [SerializeField] public string locationName;
    [TextArea(10, 20)][SerializeField] public string locationDescription;

    [SerializeField] private float disabledLocationTextTransparency = 0.7f;

    private void Awake()
    {
        
    }

    void Start()
    {
        var button = GetComponent<Button>();
        var buttonText = GetComponentInChildren<Text>();

        bool canEnter = gameProgressRequired <= GameDataManager.Data.gameProgress;
        
        button.interactable = canEnter;

        if (!canEnter)
        {
            var newColor = buttonText.color;
            newColor.a = 1f - disabledLocationTextTransparency;
            buttonText.color = newColor;
        }
    }
    
}
