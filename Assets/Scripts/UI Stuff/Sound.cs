using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour {


    public void OnVolumeChange(float k)
    {
        AudioListener.volume = k;
    }

}
