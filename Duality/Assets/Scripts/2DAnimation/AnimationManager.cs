using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public static AnimationManager Instance { get; set; }

    public GameObject singleAnimationPrefab;

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
    }

    public static void PlayAnimationAtPoint(Vector2 position, string animationName, float animationSpeed = 1f)
    {
        var go = Instantiate(Instance.singleAnimationPrefab, position, Quaternion.identity);

        go.GetComponent<SingleAnimationAnimator>().Initialize(animationName, animationSpeed);
    }
}
