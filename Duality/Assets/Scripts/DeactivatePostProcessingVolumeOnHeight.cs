using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DeactivatePostProcessingVolumeOnHeight : OnHeightBehaviour
{
    public Volume volume;

    public float time = 2f;

    protected override void Action()
    {
        StartCoroutine(SmoothVolumeDeactivationCoroutine());
    }


    IEnumerator SmoothVolumeDeactivationCoroutine()
    {
        while (volume.weight > 0)
        {
            volume.weight -= 1f / time * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
