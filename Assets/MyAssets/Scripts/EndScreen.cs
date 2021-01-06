using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class EndScreen : MonoBehaviour
{

    public PostProcessProfile blurProfile;
    public PostProcessProfile normalProfile;
    public PostProcessVolume postProcesssVolume;

    public void EnableCameraBlur(bool state)
	{
        if(postProcesssVolume != null && blurProfile != null && normalProfile != null)
		{
            postProcesssVolume.profile = (state) ? blurProfile : normalProfile;
		}
	}

}
