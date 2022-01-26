using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class CharacterAnimator : SpriteAnimator
{
    [SerializeField] private string framesLocation = "";
    private bool animationsLoaded = false;

    public enum Animation
    {
        Idle, Walk, Fall, Jump

    }

    public void PlayAnimation(Animation animation, bool loop = true, int startFrame = 0)
    {
        Play(animation.ToString().ToLower(), loop, startFrame);
    }

    public void SetAnimation(Animation animation)
    {
        if (currentAnimation.Name != animation.ToString().ToLower())
        {
            PlayAnimation(animation);
        }
    }

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

        loadedAnimations.Add(new SpriteAnimation()
        {
            Frames = Resources.LoadAll<Sprite>($"{basePath}/walk").ToArray(),
            Name = "walk",
            Type = SpriteAnimation.AnimationType.PingPong
        });

        loadedAnimations.Add(new SpriteAnimation()
        {
            Frames = Resources.LoadAll<Sprite>($"{basePath}/jump").ToArray(),
            Name = "jump",
            Type = SpriteAnimation.AnimationType.Normal
        });

        loadedAnimations.Add(new SpriteAnimation()
        {
            Frames = Resources.LoadAll<Sprite>($"{basePath}/fall").ToArray(),
            Name = "fall",
            Type = SpriteAnimation.AnimationType.Normal
        });

        animations = loadedAnimations;
        animationsLoaded = true;
    }
}
