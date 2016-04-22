using UnityEngine;
using System.Collections;

public class DesParticule : MonoBehaviour {
    float i = 0;
    ParticleSystem part;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, 8);
        part = GetComponent<ParticleSystem>();
	}
    void Update()
    {
        if(i>5)
        {
            part.Stop();   
        }
        i += Time.deltaTime;
    }

}
