using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class RoundMovements : NetworkBehaviour {

    float i;
    Vector3 startp;
    Vector3 endp;

    [SerializeField]
    float speed;
    [SerializeField]
    bool sens;

    // Use this for initialization
    void Start()
    {

        if (sens)
        {
            startp = transform.eulerAngles; 
            endp = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y+ 360,transform.localEulerAngles.z);
        }
        else
        {
            endp = transform.eulerAngles; 
            startp = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 360, transform.localEulerAngles.z);
        }


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
        transform.eulerAngles = Vector3.Lerp(startp, endp, i);
        

        if (i >= 1)
        {
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
