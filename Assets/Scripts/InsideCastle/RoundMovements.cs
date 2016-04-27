using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class RoundMovements : NetworkBehaviour
{

    float i;
    Vector3 startp;
    Vector3 endp;
    public Vector3 Rotation;

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
            endp = new Vector3(transform.eulerAngles.x + Rotation.x, transform.eulerAngles.y + Rotation.y, transform.localEulerAngles.z + Rotation.z);
        }
        else
        {
            endp = transform.eulerAngles;
            startp = new Vector3(transform.eulerAngles.x + Rotation.x, transform.eulerAngles.y + Rotation.y, transform.localEulerAngles.z + Rotation.z);
        }


        if (isServer)
        {
            StartCoroutine(Waiting());
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
        i += Time.deltaTime * speed;
        transform.eulerAngles = Vector3.Lerp(startp, endp, i);


        if (i >= 1)
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


