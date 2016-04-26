using UnityEngine;
using UnityEngine.Networking;
using System.Collections;


public class AxesMovements : NetworkBehaviour {

    float i;
    Vector3 startp;
    Vector3 endp;
    [SerializeField]
    Vector3 Forcevector;

    Vector3 wherewego;
    Vector3 wherewend;
    [SerializeField]
    float speed;
    [SerializeField]
    bool sens;
    [SerializeField]
    int angle;

	// Use this for initialization
	void Start () {
    
        startp = new Vector3(angle, 0, 0); // POSITIF TO NEGATIF SENS = TRUE
        endp = new Vector3(-angle,0,0);
        if (sens)
        {
            wherewego = endp;
            wherewend = startp;
        }
        else
        {
            wherewego = startp;
            wherewend = endp;
        }

        
        //GetComponent<NetworkTransform>().enabled = false;
        if (isServer)
        {
            StartCoroutine(Waiting());
        }
       
       
	}
    [ClientRpc]
    void RpcSendIt(float k, bool mdr)
    {
       
        i = k;
        sens = mdr;
        
    }
	// Update is called once per frame
	void Update () {
        i += Time.deltaTime*speed;
        transform.eulerAngles = Vector3.Lerp(wherewego, wherewend, Mathf.SmoothStep(0.0f, 1.0f, i));
        if(i >=1)
        {
            if(sens)
            {
                wherewego = startp;
                wherewend = endp;
            }
            else
            {
                wherewego = endp;
                wherewend = startp;
            }
            i = i % 1;
            sens = !sens;
            
        }

    }
    IEnumerator Waiting()
    {
        while (true)
        {
            RpcSendIt(i,sens);
            yield return new WaitForSeconds(1);
            
        }
    }
 
}
