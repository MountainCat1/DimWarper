using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuWindow : MonoBehaviour
{
    [SerializeField] private GameObject defaultSelected;
    
    public bool Shown { get; private set; } = false;
    

    public void Show()
    {
        Shown = true;
        gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(defaultSelected);
    }

    public void Hide()
    {
        Shown = false;
        gameObject.SetActive(false);
    }
    
    
}
