using UnityEngine;
using System.Collections;

public class AxesMovements : MonoBehaviour {

    float i;
    Vector3 startp;
    Vector3 endp;
    
	// Use this for initialization
	void Start () {
        startp = transform.eulerAngles;
        endp = new Vector3(-startp.x, startp.y, startp.z);
	}
	
	// Update is called once per frame
	void Update () {

        i += Time.deltaTime;
        transform.eulerAngles = Vector3.Lerp(startp, endp, Mathf.SmoothStep(0.0f, 1.0f, i));
        if(i >=1)
        {
            Vector3 save = startp;
            startp = endp;
            endp = save;
            i = 0;
        }

    }

 
}
