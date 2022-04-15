using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ActivatePostProcessingVolumeOnHeight : OnHeightBehaviour
{
    public Volume volume;

    public float time = 2f;

    protected override void Action()
    {
        StartCoroutine(SmoothVolumeActivationCoroutine());
    }


    IEnumerator SmoothVolumeActivationCoroutine()
    {
        while (volume.weight<1)
        {
            volume.weight += 1f / time * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
