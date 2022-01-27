using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EffectManager : MonoBehaviour
{
    public Volume volume;

    public Color iceColor;
    public Color fireColor;

    // Update is called once per frame
    void Update()
    {
        if (volume.profile.TryGet<Vignette>(out var settings))
        {
            var newIntensity = settings.intensity;

            if (Input.GetKeyDown(KeyCode.Return))
            {
                newIntensity.value = 1f;

                if(DimensionManager.Instance.dimension == DimensionManager.Dimension.Fire)
                {
                    settings.color.value = iceColor;// = new ColorParameter(fireColor, true);
                }
                else
                {
                    settings.color.value = fireColor;
                }
            }

            newIntensity.value -= 1f * Time.deltaTime;
            settings.intensity = newIntensity;
        }

    }
}
