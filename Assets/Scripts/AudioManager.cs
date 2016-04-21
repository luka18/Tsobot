using UnityEngine;
using System.Collections;


public class AudioManager : MonoBehaviour {
    [SerializeField]
    AudioClip[] cliptab;

    AudioSource aud;
	// Use this for initialization
	void Start () {
        aud = GetComponent<AudioSource>();
        
	}
	
    public void Throw()
    {
        int a = Random.Range(0, 2);
        print(a + "lol?");
        if (a == 0)
        {
            aud.clip = cliptab[0];
        }
        else
        {
            aud.clip = cliptab[1];
        }
        print("play?");
        print(aud.clip.name);
        aud.Play();
    }
	
}
