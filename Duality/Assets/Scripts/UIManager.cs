using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text heightDisplay;
    [SerializeField] private Slider energyBar;

    private float maxHeight = -1f;

    void Update()
    {
        UpdateHeightDisplay();
        UpdateEnergyBar();
    }

    void UpdateHeightDisplay()
    {
        if (PlayerController.Instance.transform.position.y > maxHeight)
            maxHeight = PlayerController.Instance.transform.position.y;

        heightDisplay.text = (Mathf.Round(maxHeight * 10) / 10f).ToString();
    }

    void UpdateEnergyBar()
    {
        energyBar.maxValue = Mathf.RoundToInt( GameManager.Instance.maxEnergy / GameManager.Instance.actionEnergyCost);
        energyBar.value = Mathf.FloorToInt( GameManager.Instance.Energy / GameManager.Instance.actionEnergyCost);
    }
}
