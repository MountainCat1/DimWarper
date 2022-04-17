using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CampaignMapMenu : MonoBehaviour
{
    [SerializeField] private List<Button> locationButtons;
    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(locationButtons[0].gameObject);
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

    }

    private void Enter()
    {
        
    }

    private void MoveSelectedLocation(int move)
    {
        var selectedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();

        var index = locationButtons.IndexOf(selectedButton);
        
        EventSystem.current.SetSelectedGameObject(locationButtons[index + move].gameObject);
    }
}
