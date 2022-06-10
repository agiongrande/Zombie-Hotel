using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcess : MonoBehaviour
{
    private PostProcessVolume _postProcessVolume;
    // Start is called before the first frame update
    void Start()
    {
        _postProcessVolume = GetComponent<PostProcessVolume>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Apuntar(bool apuntar){
        if (apuntar){
            if (_postProcessVolume.profile.TryGetSettings(out Vignette _vignette)){
                _vignette.intensity.value = 0.34f;
            }
        } else {
            if (_postProcessVolume.profile.TryGetSettings(out Vignette _vignette)){
                _vignette.intensity.value = 0.7f;
            }
        }
        
    }
}
