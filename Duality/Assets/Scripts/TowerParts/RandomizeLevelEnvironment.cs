using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = System.Random;

public class RandomizeLevelEnvironment : MonoBehaviour
{
    [SerializeField] private SpriteRenderer foregroundRenderer;
    [SerializeField] private SpriteRenderer backgroundRenderer;
    [SerializeField] private AudioSource soundtrackAudioSource;
    [SerializeField] private List<SpriteRenderer> boundRenderers;

    [SerializeField] private List<LevelEnvironment> levelEnvironments;

    private void Start()
    {
        var environment = levelEnvironments[new Random().Next(0, levelEnvironments.Count)];

        foregroundRenderer.sprite = environment.foreground;
        backgroundRenderer.sprite = environment.background;
        soundtrackAudioSource.clip = environment.soundtrack;

        foregroundRenderer.color = environment.tint;
        backgroundRenderer.color = environment.tint;
        DimensionManager.Instance.deactivatedObjectAlpha = environment.notActiveObjectAlpha;
        
        boundRenderers.ForEach((spriteRenderer => spriteRenderer.sprite = environment.wall));
        
        Debug.Log($"Environment randomizer have chosen: {levelEnvironments.IndexOf(environment)}");
    }
}

[Serializable]
public class LevelEnvironment
{
    public Sprite foreground;
    public Sprite background;
    public Sprite wall;
    public AudioClip soundtrack;
    public Color tint = Color.white;
    public float notActiveObjectAlpha = 0.3f;
}
