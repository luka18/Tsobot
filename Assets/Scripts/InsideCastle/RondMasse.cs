using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class RondMasse : NetworkBehaviour {

    float i;
    

    [SerializeField]
    float speed;
    [SerializeField]
    bool sens;
    Vector3 startp;
    Vector3 endp;

    // Use this for initialization
    void Start()
    {
        startp = transform.eulerAngles;
        if(sens)
        {
            endp = new Vector3(startp.x, startp.y - 360, startp.z);
        }
        else
        {
            endp = new Vector3(startp.x, startp.y + 360, startp.z);
        }

        //GetComponent<NetworkTransform>().enabled = false;
        if (isServer)
        {
            StartCoroutine(Waiting());
        }
        else
        {
            GetComponent<NetworkTransform>().transformSyncMode = NetworkTransform.TransformSyncMode.SyncNone;
        }

    }
    [ClientRpc]
    void RpcSendIt(float k)
    {
        i = k;
        

    }
    // Update is called once per frame
    void Update()
    {
        i += Time.deltaTime*speed;
        transform.eulerAngles = Vector3.Lerp(startp, endp, i);
        if(i>=1)
        {
            i = i % 1;
        }

    }
    IEnumerator Waiting()
    {
        while (true)
        {
            RpcSendIt(i);
            yield return new WaitForSeconds(1);

        }
    }
}
