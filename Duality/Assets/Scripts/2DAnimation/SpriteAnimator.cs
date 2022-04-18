using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class SpriteAnimator : MonoBehaviour
{
    [SerializeField] public float speedMultiplier = 1f;
    [HideInInspector] public string playAnimationOnStart;

    [HideInInspector] private SpriteRenderer spriteRenderer;

    [HideInInspector] public List<SpriteAnimation> animations;
    [HideInInspector] public SpriteAnimation currentAnimation;
    [HideInInspector] public bool playing = false;
    [HideInInspector] public bool flipped { get => spriteRenderer.flipX; set => spriteRenderer.flipX = value; }

    [HideInInspector] private int currentFrame = 0;
    [HideInInspector] private bool loop = true;

    public event Action<SpriteAnimation> animationFinishedEvent;

    public int RedererSortingOrder { get => spriteRenderer.sortingOrder; set => spriteRenderer.sortingOrder = value; }

    protected virtual void Awake()
    {
        if (!spriteRenderer)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void OnEnable()
    {
        if (playAnimationOnStart != "")
            Play(playAnimationOnStart);
    }
    protected virtual void OnDisable()
    {
        playing = false;
        currentAnimation = null;
    }

    public void Play(string name, bool loop = true, int startFrame = 0)
    {
        SpriteAnimation animation = GetAnimation(name);
        if (animation != null && animation.Frames.Length > 0)
        {
            if (animation != currentAnimation)
            {
                ForcePlay(name, loop, startFrame);
            }
        }
        else
        {
            Debug.LogWarning("could not find animation, or frames are empty: " + name);
        }
    }

    public void ForcePlay(string name, bool loop = true, int startFrame = 0)
    {
        SpriteAnimation animation = GetAnimation(name);
        if (animation != null)
        {
            this.loop = loop;
            currentAnimation = animation;
            playing = true;
            currentFrame = startFrame;
            spriteRenderer.sprite = animation.Frames[currentFrame];
            StopAllCoroutines();
            StartCoroutine(PlayAnimation(currentAnimation));
        }
    }

    public SpriteAnimation GetAnimation(string name)
    {
        return animations.Find(x => x.Name == name);
    }

    IEnumerator PlayAnimation(SpriteAnimation animation)
    {

        float timer = 0f;
        float delay = animation.Time / speedMultiplier;
        bool direction = true;

        while (loop || currentFrame < animation.Frames.Length - 1)
        {
            while (timer < delay) // wait to match fps
            {
                timer += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            while (timer > delay) // display frames
            {
                timer -= delay;

                NextFrame(animation, ref direction);
            }

            spriteRenderer.sprite = animation.Frames[currentFrame];
        }

        animationFinishedEvent?.Invoke(animation);
        currentAnimation = null;
        playing = false;
    }

    void NextFrame(SpriteAnimation animation, ref bool direction)
    {
        if (!loop)
        {
            currentFrame++;

            if (currentFrame >= animation.Frames.Length)
            {
                currentFrame = animation.Frames.Length - 1;
            }
            return;
        }


        switch (animation.Type)
        {
            case SpriteAnimation.AnimationType.Normal:
                currentFrame++;

                if (currentFrame >= animation.Frames.Length)
                {
                    currentFrame = 0;
                    animationFinishedEvent?.Invoke(animation);
                }
            break;
            case SpriteAnimation.AnimationType.PingPong:
                if (direction)
                {
                    currentFrame += 1;

                    if (currentFrame >= animation.Frames.Length - 1)
                    {
                        direction = false;
                        animationFinishedEvent?.Invoke(animation);
                    }
                }
                else
                {
                    currentFrame -= 1;

                    if (currentFrame <= 0)
                    {
                        direction = true;
                        animationFinishedEvent?.Invoke(animation);
                    }
                }
            break;

            default:
                throw new NotImplementedException();
        }
    }
}

[System.Serializable]
public class SpriteAnimation
{
    public enum AnimationType
    {
        Normal, PingPong
    }

    public string Name { get; set; }
    public float Speed { get; private set; } = 1;
    public float Time { get => 1f / Speed; }
    public AnimationType Type { get; set; }
    public Sprite[] Frames { get; set; }


    public SpriteAnimation(string name, float speed, Sprite[] frames)
    {
        Name = name;
        Speed = speed;
        Frames = frames;
    }

    public SpriteAnimation()
    {
    }
}