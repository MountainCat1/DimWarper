using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class OptionsScreen : MenuWindow
{
    //[SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider generalVolumeSlider;
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle postProcessingToggle;

    private const float VolumeMultiplier = 10f;

    private PlayerSettings settings;
    
    private void OnEnable()
    {
        settings = GameDataManager.Data.playerSettings;

        musicToggle.isOn = settings.musicEnabled;
        postProcessingToggle.isOn = settings.postProcessingEnabled;

        generalVolumeSlider.value = settings.generalVolume * VolumeMultiplier;
        //musicVolumeSlider.value = settings.musicVolume;
    }

    private void OnDisable()
    {
        settings = null;
        GameDataManager.SaveData();
    }

    private void Update()
    {
        UpdateSettings();
    }

    private void UpdateSettings()
    {
         settings.musicEnabled = musicToggle.isOn;
         settings.postProcessingEnabled = postProcessingToggle.isOn;

         settings.generalVolume = generalVolumeSlider.value / VolumeMultiplier;
         //settings.musicVolume = musicVolumeSlider.value / VolumeMultiplier;
    }
}
