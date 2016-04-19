using UnityEngine;
using System.Collections;

public class PlaySound : MonoBehaviour {
    AudioSource aud;

    void Start()
    {
        aud = transform.GetComponent<AudioSource>();
    }

    public void PlayAud()
    {
        aud.Play();   
    }


}
