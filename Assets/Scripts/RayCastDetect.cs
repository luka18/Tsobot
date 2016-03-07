using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class RayCastDetect : NetworkBehaviour {
    private Transform cam;
    [SyncVar]
    private NetworkIdentity objNetId;

    [SerializeField]
    Animator2 animate;
    
    public ButtonsColor btc;
    private bool carrying = false;
    private Quaternion rot;
    private NetworkIdentity ObjCarry;
    private RB2 MyRB2;

    void Start()
    {
        cam = transform.FindChild("Camera");
        MyRB2 = transform.GetComponent<RB2>();
    }
    //------------------------------------------RPC-----------------------------------------
    [ClientRpc]
    void RpcDrop(GameObject obj, int typeball)
    {
        obj.GetComponentInParent<ButtonsColor>().dropball(typeball);
    }

    [ClientRpc]
    void RpcPressOnce(NetworkIdentity ID, int num)
    {
        GameObject obj = ID.gameObject;
        obj.GetComponent<ButtonPressedOnce>().press();
        obj.GetComponent<CallMeColors>().call(num);
    }

    [ClientRpc]
    void RpcPress(GameObject obj)
    {
        obj.GetComponent<ButtonPressed>().press();
    }
    [ClientRpc]
    void RpcBaton(GameObject obj, int i)
    {
        obj.transform.GetComponentInParent<Batonnets>().Plays(i);
    }
    [ClientRpc]
    void RpcWaves(GameObject obj)
    {
        obj.GetComponent<StartWaves>().go();
    }
    [ClientRpc]
    void RpcCarry(NetworkIdentity obj)
    {
        Carry(obj);
    }
    [ClientRpc]
    void RpcUnCarry(NetworkIdentity obj)
    {
        UnCarry(obj);
    }
    // ----------------------------------------------------- COMMAND---------------------------------------------
    [Command]
    void CmdPress(GameObject obj)
    {
        objNetId = obj.GetComponent<NetworkIdentity>();
        objNetId.AssignClientAuthority(connectionToClient);
        RpcPress(obj);
        objNetId.RemoveClientAuthority(connectionToClient);
    }

    [Command]
    void CmdPressOnce(NetworkIdentity ID, int k)
    {
        RpcPressOnce(ID, k);
    }
    [Command]
    void CmdDrop(GameObject obj, int typeball)
    {
        RpcDrop(obj, typeball);
    }
    [Command]
    void CmdBaton(GameObject obj, int i)
    {
        RpcBaton(obj, i);
    }
    [Command]
    void CmdWaves(GameObject obj)
    {
        RpcWaves(obj);
    }
    [Command]
    void CmdCarry(NetworkIdentity obj)
    {
        RpcCarry(obj);
    }
    [Command]
    void CmdUnCarry(NetworkIdentity obj )
    {
       
        RpcUnCarry(obj);
    }


    //-----------------NO NETWORK FROM HERE-------------------

    void UnCarry(NetworkIdentity NetID)
    {
        GameObject car = NetID.gameObject;
        
        car.GetComponent<NetworkTransform>().enabled = true;
        car.transform.localPosition = new Vector3(0, 2, 2f);
        car.transform.parent = null;
        car.GetComponent<Rigidbody>().isKinematic = false;
        car.transform.rotation = rot;
        carrying = false;
        car.transform.tag = "Portal";

    }
    
    void Carry(NetworkIdentity NetID)
    {
        
        GameObject car = NetID.gameObject;
        car.tag = "Untagged"; 
        car.GetComponent<Rigidbody>().isKinematic = true;
        car.transform.SetParent(transform);
        car.GetComponent<NetworkTransform>().enabled = false;
        carrying = true;
        rot = car.transform.rotation;
        StartCoroutine(mycor2(car,1));
    }
    //Animation Carry
    IEnumerator mycor2(GameObject car, float i)
    {

        yield return new WaitForSeconds(0.2f);
        car.transform.localPosition = new Vector3(0,3, 0);
    }






    // Update is called once per frame
    void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position + new Vector3(0, 2.0f, 0), cam.transform.forward * 3, Color.black, 1.0f);
            if (carrying)
            {
                Debug.DrawRay(transform.position , transform.forward, Color.black, 1.0f);
                if (!Physics.Raycast(transform.position+new Vector3(0,1,0), transform.forward, out hit, 3.0f))
                {

                    CmdUnCarry(ObjCarry);
                    MyRB2.Carry = false;
                    animate.CmdUnCarry(transform.GetComponent<NetworkIdentity>());
                }
            }


            
            else if ((Physics.Raycast(transform.position + new Vector3(0, 2.0f, 0), cam.transform.forward, out hit, 3.0f)))
            {
                if(hit.transform.tag == "Portal")
                {
                    animate.CmdCarry(transform.GetComponent<NetworkIdentity>());
                    ObjCarry = hit.transform.GetComponent<NetworkIdentity>();
                    CmdCarry(hit.transform.GetComponent<NetworkIdentity>());
                    MyRB2.Carry = true;
                    
                }
                if (hit.transform.tag == "Button")
                {
                    animate.CmdPush(transform.GetComponent<NetworkIdentity>());
                    switch (hit.transform.name)
                    {
                        case "1Batton":
                            CmdBaton(hit.transform.gameObject, 1);
                            break;
                        case "2Batton":
                            CmdBaton(hit.transform.gameObject, 2);
                            break;
                        case "3Batton":
                            CmdBaton(hit.transform.gameObject, 3);
                            break;
                        case "Bouton bleu":
                            CmdPress(hit.transform.gameObject);
                            CmdDrop(hit.transform.gameObject, 1);
                            break;
                        case "Bouton violet":
                            CmdPress(hit.transform.gameObject);
                            CmdDrop(hit.transform.gameObject, 2);
                            break;
                        case "Bouton vert":
                            CmdPress(hit.transform.gameObject);
                            CmdDrop(hit.transform.gameObject, 3);
                            break;
                        case "Bouton rouge":
                            CmdPress(hit.transform.gameObject);
                            CmdDrop(hit.transform.gameObject, 4);
                            break;
                        case "BoutonRed":
                            //CmdPressOnce(hit.transform.gameObject,3);
                            CmdPressOnce(hit.transform.GetComponent<NetworkIdentity>(), 3);
                            break;
                        case "BoutonBleu":
                            //hit.transform.GetComponent<ButtonPressedOnce>().press();
                            //CmdPressOnce(hit.transform.gameObject,1);
                            CmdPressOnce(hit.transform.GetComponent<NetworkIdentity>(), 1);
                            break;
                        case "BoutonVert":
                            //CmdPressOnce(hit.transform.gameObject,2);// 2 = any time you want 1 = one time
                            CmdPressOnce(hit.transform.GetComponent<NetworkIdentity>(), 2);
                            break;
                        case "BoutonViolet":
                            //CmdPressOnce(hit.transform.gameObject,0);
                            CmdPressOnce(hit.transform.GetComponent<NetworkIdentity>(), 0);
                            break;
                        case "BoutonGreen":
                            CmdPress(hit.transform.gameObject);
                            CmdWaves(hit.transform.gameObject);
                            break;
                    }
                }


            }


        }

    }
}
