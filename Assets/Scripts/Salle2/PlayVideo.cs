using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayVideo : MonoBehaviour {
    [SerializeField]
    MovieTexture setting;
    [SerializeField]
    MovieTexture mouse;


	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	
    public void PlaySet()
    {
        GetComponent<RawImage>().texture = setting as MovieTexture;
        
    }

}
