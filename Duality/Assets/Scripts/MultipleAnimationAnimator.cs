using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MultipleAnimationAnimator : SpriteAnimator
{
    [SerializeField] private List<string> framesLocations = new List<string>();
    private bool animationsLoaded = false;

    protected override void Awake()
    {
        base.Awake();

        if (!animationsLoaded)
            LoadAnimations();
    }

    private void LoadAnimations()
    {
        List<SpriteAnimation> loadedAnimations = new List<SpriteAnimation>();

        string basePath = $"Sprites/Animations";

        foreach (var animationPath in framesLocations)
        {
            loadedAnimations.Add(new SpriteAnimation()
            {
                Frames = Resources.LoadAll<Sprite>($"{basePath}/{animationPath}").ToArray(),
                Name = animationPath
            });
        }

        animations = loadedAnimations;
        animationsLoaded = true;
    }
}
