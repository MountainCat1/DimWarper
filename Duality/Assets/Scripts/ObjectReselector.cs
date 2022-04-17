using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectReselector : MonoBehaviour
{
    private GameObject lastSelected;
    
    [SerializeField] private bool disableDeselecting = true;

    // Update is called once per frame
    void Update()
    {
        if (disableDeselecting)
            CancelBackgroundDeselectionClick();
        
        lastSelected = EventSystem.current.currentSelectedGameObject;
    }
    
    public void CancelBackgroundDeselectionClick()
    {
        if (lastSelected != null && EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastSelected);
        }
    }
}
