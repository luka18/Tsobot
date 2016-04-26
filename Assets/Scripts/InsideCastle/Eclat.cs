using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Eclat : NetworkBehaviour {

    Transform Left;
    Transform Right;

    Vector3 startpos;
    Vector3 endpos;
    [SerializeField]
    uint speed;
    float i = 0;
    bool sens;
    ParticleSystem go;
	// Use this for initialization
	void Start () {
        Left = transform.GetChild(0);
        Right = transform.GetChild(1);
        go = transform.GetChild(2).GetComponent<ParticleSystem>();
        startpos = Right.eulerAngles;
        endpos = new Vector3(6, 0, 0);
        sens = true;
        print("startpos" + startpos);
        print("Endp" + endpos);
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
    void Update()
    {
        i += Time.deltaTime * speed;
        
        if (sens)
        {
            Right.eulerAngles = Vector3.Lerp(startpos, endpos, 1f - Mathf.Cos(i * Mathf.PI * 0.5f));
            Left.eulerAngles = -Right.eulerAngles;
      
        }
        else
        {
           
            Right.eulerAngles = Vector3.Lerp(endpos, startpos, Mathf.Sin(i * Mathf.PI * 0.5f));
            Left.eulerAngles = -Right.eulerAngles;
        }
        if (i >= 1)
        {
            if(sens)
                go.Emit(5);
            i = i % 1;
            sens = !sens;
        }

    }
    IEnumerator Waiting()
    {
        while (true)
        {
            RpcSendIt(i, sens);
            yield return new WaitForSeconds(1);

        }
    }

	
	
}
