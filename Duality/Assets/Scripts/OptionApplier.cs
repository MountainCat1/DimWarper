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
    private static OptionApplier Instance { get; set; }
    
    private const string SoundtrackAudiSourceGameObjectName = "Soundtrack";

    private void OnEnable()
    {
        if(Instance != null)
            Debug.LogError($"Second instance of {GetType().Name} initialized");
        
        Instance = this;
    }

    private void OnDisable()
    {
        if(Instance == this)
            Instance = null;
    }

    private void Start()
    {
        Apply();
    }

    public static void ApplyOptions()
    {
        if (Instance == null)
        {
            Debug.LogError($"Singleton of OptionsApplier was not initialized!");
            return;
        }
        
        Instance.Apply();
    }
    private void Apply()
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
        var soundtrackAudioSource = GameObject.Find(SoundtrackAudiSourceGameObjectName)
            ?.GetComponent<AudioSource>();
        soundtrackAudioSource.enabled = settings.musicEnabled;
        
        // Disable/Enable postprocessing 
        if (settings.postProcessingEnabled == false)
        {
            var postProcessingVolumes = GameObject.FindObjectsOfType<Volume>();
            postProcessingVolumes.ToList().ForEach(x => x.enabled = false);
        }
    }
}
