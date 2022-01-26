using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class HeightDisplay : MonoBehaviour
{
    private Text textComponent;

    private float maxHeight = -1f;

    private void Awake()
    {
        textComponent = GetComponent<Text>();
    }

    void Update()
    {
        if (PlayerController.Instance.transform.position.y > maxHeight)
            maxHeight = PlayerController.Instance.transform.position.y;

        textComponent.text = (Mathf.Round( maxHeight * 10) / 10f).ToString();
    }
}
