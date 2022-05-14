using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGameOnHeight : OnHeightBehaviour
{
    [SerializeField] private float delay = 3f;

    private void Awake()
    {
        usePlayerHeight = true;
    }

    protected override void Action()
    {
        StartCoroutine(WinDelayCoroutine());
        GameManager.Instance.cameraSpeedChangeSpeed *= 2f; // Speed up the camera speed change
        GameManager.Instance.Win();
    }
    
    IEnumerator WinDelayCoroutine()
    {
        yield return new WaitForSeconds(delay);

        InGameUIManager.Instance.OnWin();
    }
}
