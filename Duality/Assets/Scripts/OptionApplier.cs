using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Rendering;


/// <summary>
/// This class objects purpose is to apply all the setting/options from the menu
/// </summary>
public class OptionApplier : MonoBehaviour
{
    private const string soundtrackAudiSourceGameObjectName = "Soundtrack";
    
    private void Start()
    {
        var settings = GameDataManager.Data?.playerSettings;
        if (settings == null)
        {
            Debug.LogWarning("Game Data is missing! OptionApplier will use default construct for user settings");
            settings = new PlayerSettings();
        }

        // Set general volume
        AudioListener.volume = settings.generalVolume;

        // Disable/Enable soundtrack
        var soundtrackAudioSource = GameObject.Find(soundtrackAudiSourceGameObjectName)
            .GetComponent<AudioSource>();
        soundtrackAudioSource.enabled = settings.musicEnabled;
        
        // Disable/Enable postprocessing 
        if (settings.postProcessingEnabled == false)
        {
            var postProcessingVolumes = GameObject.FindObjectsOfType<Volume>();
            postProcessingVolumes.ToList().ForEach(x => x.enabled = false);
        }
    }
}
