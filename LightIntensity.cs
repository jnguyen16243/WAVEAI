using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class LightIntensity : MonoBehaviour
{
    public PostProcessVolume volume;
    Bloom bloomLayer = null;
    public float bloomSpeed = 0.5f;

    public float maxIntensity = 30f;
    public float minIntensity = 1f;
    // Start is called before the first frame update
    void Start()
    {
        volume.profile.TryGetSettings(out bloomLayer);
        if(bloomLayer != null)

        {
            Debug.Log("Bloom layer is not null");
            bloomLayer.enabled.value = true;
            bloomLayer.intensity.value = 5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Bloom Intensity Value: " + bloomLayer.intensity.value);
        bloomLayer.intensity.value = Mathf.Lerp(minIntensity, maxIntensity, AudioPeer._AmplitudeBuffer);
     
    }
}
