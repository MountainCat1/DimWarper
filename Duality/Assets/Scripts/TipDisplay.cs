using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TipDisplay : MonoBehaviour
{
    [SerializeField] private Text tipDisplay;
    [SerializeField] private TextAsset tipFile;
    

    private List<string> tips = new List<string>();

    private void Start()
    {
        tips.AddRange(tipFile.text.Split('\n'));

        tipDisplay.text = tips[Random.Range(0, tips.Count - 1)];
    }
}
