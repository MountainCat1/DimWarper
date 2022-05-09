using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen
{
    public static void Show()
    {
        var go = Resources.Load<GameObject>("Prefabs/UI/Loading Screen");
        Object.Instantiate(go);
    }
}
