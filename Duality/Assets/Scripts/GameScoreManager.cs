using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScoreManager : MonoBehaviour
{
    [SerializeField] private string levelName;
    private void Awake()
    {
        GameManager.Instance.GameLostEvent += GameLostEventHandler;
    }

    private void GameLostEventHandler()
    {
        float height = GameManager.Instance.ExpectedHeight;
        float time = GameManager.Instance.Timer;

        GameDataManager.AddHighScore(levelName, height, time);
    }
}
