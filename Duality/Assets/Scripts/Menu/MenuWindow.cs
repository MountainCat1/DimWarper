using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuWindow : MonoBehaviour
{
    [SerializeField] private GameObject defaultSelected;

    public void Show()
    {
        gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(defaultSelected);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
