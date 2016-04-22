using UnityEngine;
using System.Collections;

public class ExploDie : MonoBehaviour {


	// Use this for initialization
	void Start () {
        Destroy(gameObject, 1);
        GetComponent<AudioSource>().Play();
	}
	
}
