using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class SingleAnimationAnimator : SpriteAnimator
{
    private bool animationsLoaded = false;

    public enum Animation
    {
        Default
    }
    protected override void Awake()
    {
        base.Awake();
    }

    public void Initialize(string animationName, float speed = 1f)
    {
        if (!animationsLoaded)
            LoadAnimation(animationName);

        speedMultiplier = speed;

        animationFinishedEvent += SingleAnimationAnimator_animationFinishedEvent;

        Play("default", false);
    }

    private void SingleAnimationAnimator_animationFinishedEvent(SpriteAnimation obj)
    {
        Destroy(gameObject);
    }

    private void LoadAnimation(string animationName)
    {
        List<SpriteAnimation> loadedAnimations = new List<SpriteAnimation>();

        string path = $"Sprites/Animations/{animationName}";

        loadedAnimations.Add(new SpriteAnimation()
        {
            Frames = Resources.LoadAll<Sprite>($"{path}").ToArray(),
            Name = "default",
            Type = SpriteAnimation.AnimationType.Normal
        });

        animations = loadedAnimations;
        animationsLoaded = true;
    }
}
