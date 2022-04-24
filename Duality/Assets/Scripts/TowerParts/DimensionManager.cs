using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionManager : MonoBehaviour
{
    public static DimensionManager Instance { get; set; }

    [SerializeField] private AudioSource changeDimensionAudio;

    [SerializeField] private GameObject icePostProcessingVolume;
    [SerializeField] private GameObject firePostProcessingVolume;
    
    [SerializeField] public float deactivatedObjectAlpha = 0.07f; 
    
    public enum Dimension
    {
        Fire,
        Ice
    }

    public HashSet<DimensionObject> dimensionObjects = new HashSet<DimensionObject>();

    public Dimension dimension;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Singeleton duplicated!");
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        Time.timeScale = 2;
    }

    public void SetDimension(Dimension dimension)
    {
        this.dimension = dimension;
        foreach (var item in dimensionObjects)
        {
            item.SetActive(item.dimension == dimension);
        }
    }

    public void SwitchDimension()
    {
        if(dimension == Dimension.Ice)
        {
            SetDimension(Dimension.Fire);
            icePostProcessingVolume.SetActive(false);
            firePostProcessingVolume.SetActive(true);
        }
        else
        {
            SetDimension(Dimension.Ice);
            icePostProcessingVolume.SetActive(true);
            firePostProcessingVolume.SetActive(false);
        }
            

        changeDimensionAudio.Play();
    }
}
