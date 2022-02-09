using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class PostProcessingManager : MonoBehaviour
{
    public static PostProcessingManager instance;
    public GameObject volumeObject;
    private PostProcessVolume volume;
    private Vignette vignetteLayer;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
        volume = volumeObject.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out vignetteLayer);
    }
    public void ActivateLowerVignette()
    {
        vignetteLayer.active = true;
    }
    public void DisableLowerVignette()
    {
        vignetteLayer.active = false;
    }

}
