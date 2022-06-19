using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Steamworks;
using UnityEngine;

public class SteamIntegrationInitializer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            if (SteamClient.IsLoggedOn)
            {
                Debug.Log($"<Steam Initialized> Already logged in as {SteamClient.Name}");
                return;
            }
        }
        catch (Exception e)
        {
            // ignored
        }


        try
        {
            SteamClient.Init(2056190);
            Debug.Log($"<Steam Initialized> Logged to steam as {SteamClient.Name}");
        }
        catch(Exception ex)
        {
            Debug.LogError($"<Steam Initialized> Error accrued while trying to connect to to steam!");
        }
    }
}
