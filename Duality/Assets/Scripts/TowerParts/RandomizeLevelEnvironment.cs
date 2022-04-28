using System;
using System.Collections;
using System.Collections.Generic;
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
        
        boundRenderers.ForEach((spriteRenderer => spriteRenderer.sprite = environment.wall));
    }
}

[Serializable]
public class LevelEnvironment
{
    public Sprite foreground;
    public Sprite background;
    public Sprite wall;
    public AudioClip soundtrack;
}
