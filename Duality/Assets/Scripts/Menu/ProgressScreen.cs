using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.UI;

public class ProgressScreen : MonoBehaviour
{
    [SerializeField] private Transform scoresContainer;
    [SerializeField] private Text progressTextDisplay;
    
    [SerializeField] private GameObject scorePanelPrefab;

    private void OnEnable()
    {
        foreach (Transform child in scoresContainer)
        {
            Destroy(child.gameObject);
        }

        var highScoresSorted = GameDataManager.Data.highScores
            .OrderByDescending(x => x.height);
       
        foreach (var highScore in highScoresSorted)
        {
            var go = Instantiate(scorePanelPrefab, scoresContainer);

            var textScripts = go.GetComponentsInChildren<Text>();

            textScripts[0].text = highScore.height.ToString("#.##");
            textScripts[1].text = highScore.time.ToString("#.##");
            textScripts[2].text = highScore.DateTime.ToShortDateString();
            textScripts[3].text = highScore.levelName;
        }

        progressTextDisplay.text = $"Campaign progress: {(float)GameDataManager.Data.gameProgress / GameData.MaxGameDataProgress * 100f}%";
    }
}
