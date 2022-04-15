using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuWindow : MonoBehaviour
{
    private GameObject lastSelected;
    
    [SerializeField] private bool disableDeselecting = true;
    [SerializeField] private GameObject defaultSelected;
    
    public bool Shown { get; private set; } = false;

    private void Update()
    {
        if (disableDeselecting)
            CancelBackgroundDeselectionClick();
        
        lastSelected = EventSystem.current.currentSelectedGameObject;
    }

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
    
    public void CancelBackgroundDeselectionClick()
    {
        if (lastSelected != null && EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastSelected);
        }
    }
}
