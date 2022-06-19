using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SteamIntegrationInitializer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            Steamworks.SteamClient.Init(2056190);
            Debug.Log($"Logged to steam as {Steamworks.SteamClient.Name}");
        }
        catch(Exception ex)
        {
            Debug.LogError($"Error accrued while trying to connect to to steam!");
        }
    }
}
