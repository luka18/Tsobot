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

    bool sens;

	// Use this for initialization
	void Start () {
        print("transpo" + transform.eulerAngles);
        startp = new Vector3(40, 0, 0); // POSITIF TO NEGATIF SENS = TRUE
        endp = new Vector3(-40,0,0);
        sens = false;
        wherewego = startp;
        wherewend = endp;

        
        //GetComponent<NetworkTransform>().enabled = false;
        if (isServer)
        {
            StartCoroutine(Waiting());
        }
        else
        {
            GetComponent<NetworkTransform>().transformSyncMode = NetworkTransform.TransformSyncMode.SyncNone;
            i = Mathf.InverseLerp(40, 320, transform.eulerAngles.x);
        }
       
	}
    [ClientRpc]
    void RpcSendIt(float k, bool mdr)
    {
        print("                                      PRE I: "+i);
        i = k;
        sens = mdr;
        print("POST I: "+i);
    }
	// Update is called once per frame
	void Update () {
        i += Time.deltaTime;
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
            
            print("i" + i);
            i = i % 1;
            print("imod2" + i);
            print("sens:+" + sens);
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
