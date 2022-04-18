using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGameOnHeight : OnHeightBehaviour
{
    [SerializeField] private MenuWindow winScreen;
    [SerializeField] private float delay = 3f;
    
    protected override void Action()
    {
        StartCoroutine(WinDelayCoroutine());
        GameManager.Instance.Win();
    }
    
    IEnumerator WinDelayCoroutine()
    {
        yield return new WaitForSeconds(delay);

        winScreen.Show();
    }
}
