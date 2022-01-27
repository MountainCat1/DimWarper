using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text heightDisplay;
    [SerializeField] private Text coinDisplay;

    private float maxHeight = -1f;

    private void Awake()
    {
        ;
    }

    void Update()
    {
        UpdateCoinDisplay();
        UpdateHeightDisplay();
    }

    void UpdateHeightDisplay()
    {
        if (PlayerController.Instance.transform.position.y > maxHeight)
            maxHeight = PlayerController.Instance.transform.position.y;

        heightDisplay.text = (Mathf.Round(maxHeight * 10) / 10f).ToString();
    }

    void UpdateCoinDisplay()
    {
        coinDisplay.text = GameManager.Instance.Coins.ToString();
    }
}
