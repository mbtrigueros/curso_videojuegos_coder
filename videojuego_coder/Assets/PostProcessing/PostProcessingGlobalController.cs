using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingGlobalController : MonoBehaviour
{
    private PostProcessVolume globalVolume;

    private void Awake()
    {
        globalVolume = GetComponent<PostProcessVolume>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void colorEffect(bool status)
    {
        ColorGrading colorFx;
        globalVolume.profile.TryGetSettings(out colorFx);
        colorFx.active = status;
    }

}
