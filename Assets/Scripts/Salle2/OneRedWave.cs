using UnityEngine;
using System.Collections;

public class OneRedWave : MonoBehaviour {

    public int speed = 5;
    private float go;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        go = speed* Time.deltaTime;

        transform.Translate(0, 0, -go);

        if(transform.position.x<18)
        {
            gameObject.SetActive(false);
        }

	
	}
    
}
