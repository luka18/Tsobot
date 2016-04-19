using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class RayCastDetect1 : NetworkBehaviour
{
    private Transform cam;
    private NetworkIdentity objNetId;


    [SerializeField]
    Animator2 animate;

    public ButtonsColor btc;
    private bool carrying = false;
    private Quaternion rot;
    private NetworkIdentity ObjCarry;
    private RB2 MyRB2;
    int layermask;

    void Start()
    {
        cam = transform.FindChild("Camera");
        MyRB2 = transform.GetComponent<RB2>();
        layermask = 1 << 9;
        layermask = ~layermask;
        print("s");
    }
    //----------------------------------------------RPC-----------------------------------------
    [ClientRpc]
    void RpcSound(GameObject obj)
    {
        obj.GetComponent<PullSound>().PlaysSound();
    }



    //-------------------------------------------COMMAND-----------------------------------------
    [Command]
    void CmdSound(GameObject obj)
    {
        RpcSound(obj);
    }


    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position + new Vector3(0, 2.0f, 0), cam.transform.forward * 3, Color.black, 1.5f);
            if (carrying)
            {
                Debug.DrawRay(transform.position, transform.forward, Color.black, 1.0f);
                if (!Physics.Raycast(transform.position + new Vector3(0, 1, 0), transform.forward, out hit, 3.0f))
                {

                    //CmdUnCarry(ObjCarry);
                    MyRB2.Carry = false;
                    animate.CmdUnCarry(transform.GetComponent<NetworkIdentity>());
                }
            }



            else if ((Physics.Raycast(transform.position + new Vector3(0, 2.0f, 0), cam.transform.forward, out hit, 3.5f, layermask)))
            {
                if (hit.transform.tag == "Portal")
                {
                    CmdSound(hit.transform.gameObject);
                }


            }

        }
    }
}
