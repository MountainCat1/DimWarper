using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimpleSpriteAnimation : SpriteAnimator
{
    [SerializeField] private string framesLocation = "";
    private bool animationsLoaded = false;

    protected override void Awake()
    {
        base.Awake();

        playAnimationOnStart = "idle";

        if (!animationsLoaded)
            LoadAnimations();
    }

    private void LoadAnimations()
    {
        List<SpriteAnimation> loadedAnimations = new List<SpriteAnimation>();

        string basePath = $"Sprites/{framesLocation}";

        loadedAnimations.Add(new SpriteAnimation()
        {
            Frames = Resources.LoadAll<Sprite>($"{basePath}/idle").ToArray(),
            Name = "idle"
        });

        animations = loadedAnimations;
        animationsLoaded = true;
    }
}
