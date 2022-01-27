using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text heightDisplay;

    private float maxHeight = -1f;

    private void Awake()
    {
        ;
    }

    void Update()
    {
        UpdateHeightDisplay();
    }

    void UpdateHeightDisplay()
    {
        if (PlayerController.Instance.transform.position.y > maxHeight)
            maxHeight = PlayerController.Instance.transform.position.y;

        heightDisplay.text = (Mathf.Round(maxHeight * 10) / 10f).ToString();
    }
}
