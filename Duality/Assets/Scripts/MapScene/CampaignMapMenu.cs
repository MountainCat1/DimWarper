using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CampaignMapMenu : MonoBehaviour
{
    [SerializeField] private GameObject characterLocationMarker;
    
    [SerializeField] private string mainMenuSceneName = "Main Menu";
    [SerializeField] private CampaignMapLocationButton defaultLocation;
    [SerializeField] private Text locationNameDisplay;
    [SerializeField] private Text locationDescriptionDisplay;
    [SerializeField] private List<Button> locationButtons;

    private void Start()
    {
        locationNameDisplay.text = defaultLocation.locationName;
        locationDescriptionDisplay.text = defaultLocation.locationDescription;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveSelectedLocation(1);   
            return;
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveSelectedLocation(-1);    
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            Enter();
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitToMenu();
        }
    }

    private void ExitToMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName, LoadSceneMode.Single);
    }

    private void Enter()
    {
        
    }

    private void MoveSelectedLocation(int move)
    {
        var selectedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();

        var index = locationButtons.IndexOf(selectedButton);
        var newIndex = index + move;
        
        // Validation
        if(newIndex < 0) return;
        if(GameDataManager.Data.gameProgress < newIndex ) return;
        //
        
        SelectLocation(locationButtons[newIndex]);
    }

    private void SelectLocation(Button button)
    {
        EventSystem.current.SetSelectedGameObject(button.gameObject);
        var locationScript = button
            .gameObject
            .GetComponent<CampaignMapLocationButton>();

        locationNameDisplay.text = locationScript.locationName;
        locationDescriptionDisplay.text = locationScript.locationDescription;

        characterLocationMarker.transform.position = button.transform.position;
    }
}
