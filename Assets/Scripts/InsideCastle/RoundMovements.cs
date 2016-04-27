using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class RoundMovements : NetworkBehaviour {

    float i;
    Vector3 startp;
    Vector3 endp;
    public Vector3 rotation;

    [SerializeField]
    float speed;
    [SerializeField]
    bool sens;

    // Use this for initialization
    void Start()
    {

        {
<<<<<<< HEAD

            if (sens)
            {
                startp = transform.eulerAngles;
                endp = new Vector3(transform.eulerAngles.x + rotation.x, transform.eulerAngles.y + rotation.y, transform.localEulerAngles.z + rotation.z);
            }
            else
            {
                endp = transform.eulerAngles;
                startp = new Vector3(transform.eulerAngles.x + rotation.x, transform.eulerAngles.y + rotation.y, transform.localEulerAngles.z + rotation.z);
            }


            if (isServer)
            {
                StartCoroutine(Waiting());
            }


=======
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
>>>>>>> origin/master
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
