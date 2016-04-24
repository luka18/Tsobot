using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class RayCastDetect : NetworkBehaviour {
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

    string lasttag;

     int salle = 2;

    void Start()
    {
        cam = transform.FindChild("Camera");
        MyRB2 = transform.GetComponent<RB2>();
        layermask = 1 << 9;
        layermask = ~layermask;
        print("s");
       
    }
    //------------------------------------------RPC-----------------------------------------
    [ClientRpc]
    void RpcDrop(GameObject obj, int typeball)
    {
        obj.GetComponentInParent<ButtonsColor>().dropball(typeball);
    }
    [ClientRpc]
    void RpcPressOnce(NetworkIdentity ID)
    {
        GameObject obj = ID.gameObject;
        obj.GetComponent<ButtonPressedOnce>().press();
        
    }
    [ClientRpc]
    void RpcCallMeCol(NetworkIdentity ID, int num)
    {
        GameObject obj = ID.gameObject;
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
    void RpcCarry(NetworkIdentity obj,Vector3 i)
    {
        Carry(obj, i);
    }
    [ClientRpc]
    void RpcUnCarry(NetworkIdentity obj,string s,int force)
    {
        UnCarry(obj,s,force);
    }
    [ClientRpc]
    void RpcDoor2(GameObject obj)
    {
        obj.GetComponent<BoutonToFunc>().Open();
    }
    // ----------------------------------------------------- COMMAND---------------------------------------------
    [Command]
    void CmdDoor2(GameObject obj)
    {
        RpcDoor2(obj);
    }



    [Command]
    void CmdPress(GameObject obj)
    {
        objNetId = obj.GetComponent<NetworkIdentity>();
        objNetId.AssignClientAuthority(connectionToClient);
        RpcPress(obj);
        objNetId.RemoveClientAuthority(connectionToClient);
    }

    [Command]
    void CmdPressOnce(NetworkIdentity ID)
    {
        RpcPressOnce(ID);
    }
    [Command]
    void CmdCallMeCol(NetworkIdentity ID, int k)
    {
        RpcCallMeCol(ID, k);
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
    void CmdCarry(NetworkIdentity obj,Vector3 i)
    {
        RpcCarry(obj,i);
    }
    [Command]
    void CmdUnCarry(NetworkIdentity obj, string s, int force)
    {

        RpcUnCarry(obj,s,force);
    }



    //-----------------NO NETWORK FROM HERE-------------------

    void UnCarry(NetworkIdentity NetID,string s,int force)
    {
        GameObject car = NetID.gameObject;
        car.GetComponent<NetworkTransform>().enabled = true;
        car.transform.localPosition = new Vector3(0, 2, 2f);
        car.transform.parent = null;
        Rigidbody carbody = car.GetComponent<Rigidbody>();
        carbody.isKinematic = false;
        car.transform.rotation = rot;
        if(force != 0)
        {
            carbody.AddForce(cam.transform.forward * 30 ,ForceMode.VelocityChange);
            car.GetComponent<RockScript>().InHand = true;
        }

        carrying = false;
        car.transform.tag = s;
    }
    
    void Carry(NetworkIdentity NetID,Vector3 i)
    {

        GameObject car = NetID.gameObject;
        car.tag = "Untagged";
        car.GetComponent<Rigidbody>().isKinematic = true;
        car.transform.SetParent(transform);
        car.GetComponent<NetworkTransform>().enabled = false;
        carrying = true;
        rot = car.transform.rotation;

        car.transform.localPosition = i;
    }
    //Animation Carry
    /*IEnumerator mycor2(GameObject car, float i)
    {

        yield return new WaitForSeconds(0.2f);
        car.transform.localPosition = i;
    }
    */

    //----------------------------------------------------SALLE2--------------------------------

    [ClientRpc]
    void RpcSound(GameObject obj)
    {
        print("obj" + obj.transform.name);
        obj.GetComponent<PullSound>().PlaysSound();
    }
    [ClientRpc]
    void RpcMelody(GameObject obj)
    {
        obj.GetComponent<MasterSound>().PlayTheSound();
    }
    [ClientRpc]
    void RpcOpenDoor(GameObject obj)
    {
        obj.GetComponent<OpenTheDoor>().Open();
        obj.GetComponent<PlaySound>().PlayAud();
    }
    [ClientRpc]
    void RpcThrowSound(GameObject obj)
    {
        obj.GetComponentInChildren<AudioManager>().Throw();
    }

    [Command]
    void CmdSound(GameObject obj)
    {
        print(obj.name);
        RpcSound(obj);
    }
    [Command]
    void CmdPlayMelody(GameObject obj)
    {
        RpcMelody(obj);
    }
    [Command]
    public void CmdOpenDoor(GameObject obj)
    {
        RpcOpenDoor(obj);
    }
    [Command]
    void CmdThrowSound(GameObject obj)
    {
        RpcThrowSound(obj);
    }


    // Update is called once per frame
    void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            print("here");
            RaycastHit hit;
            Debug.DrawRay(transform.position + new Vector3(0, 2.0f, 0), cam.transform.forward * 3, Color.black, 1.5f);
            if (carrying)
            {
                Debug.DrawRay(transform.position , transform.forward, Color.black, 1.0f);
                if (!Physics.Raycast(transform.position+new Vector3(0,1,0), transform.forward, out hit, 3.0f))
                {
                    CmdThrowSound(gameObject);
                    CmdUnCarry(ObjCarry,lasttag,1);
                    MyRB2.Carry = false;
                    animate.CmdUnCarry(transform.GetComponent<NetworkIdentity>());
                }
            }


            
            else if ((Physics.Raycast(transform.position + new Vector3(0, 2.0f, 0), cam.transform.forward, out hit, 3.5f,layermask)))
            {
                if(salle == 1)
                { 
                    if(hit.transform.tag == "Portal")
                    {
                        lasttag = "Portal";
                        animate.CmdCarry(transform.GetComponent<NetworkIdentity>());
                        ObjCarry = hit.transform.GetComponent<NetworkIdentity>();
                        CmdCarry(hit.transform.GetComponent<NetworkIdentity>(), new Vector3(0,3,0));
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
                                    CmdPressOnce(hit.transform.GetComponent<NetworkIdentity>());
                                    CmdCallMeCol(hit.transform.GetComponent<NetworkIdentity>(), 3);
                                    break;
                                case "BoutonBleu":
                                    //hit.transform.GetComponent<ButtonPressedOnce>().press();
                                    //CmdPressOnce(hit.transform.gameObject,1);
                                    CmdPressOnce(hit.transform.GetComponent<NetworkIdentity>());
                                    CmdCallMeCol(hit.transform.GetComponent<NetworkIdentity>(), 1);
                                    break;
                                case "BoutonVert":
                                    //CmdPressOnce(hit.transform.gameObject,2);// 2 = any time you want 1 = one time
                                    CmdPressOnce(hit.transform.GetComponent<NetworkIdentity>());
                                    CmdCallMeCol(hit.transform.GetComponent<NetworkIdentity>(), 2);
                                    break;
                                case "BoutonViolet":
                                    //CmdPressOnce(hit.transform.gameObject,0);
                                    CmdPressOnce(hit.transform.GetComponent<NetworkIdentity>());
                                    CmdCallMeCol(hit.transform.GetComponent<NetworkIdentity>(), 0);
                                    break;
                                case "BoutonGreen":
                                    CmdPress(hit.transform.gameObject);
                                    CmdWaves(hit.transform.gameObject);
                                    break;
                                case "BoutonWaiting":
                                    CmdPressOnce(hit.transform.GetComponent<NetworkIdentity>());
                                    CmdDoor2(hit.transform.gameObject);
                                    break;
                            }
                        }
                }
                else if(salle == 2)
                {
                    print("salle2");
                    if(hit.transform.tag =="Portal")
                    {
                        print("NAME"+hit.transform.name);
                        CmdSound(hit.transform.gameObject);
                    }
                    if(hit.transform.tag == "GetSetting")
                    {
                        print("workedbut");
                        CmdPlayMelody(hit.transform.gameObject);
                    }
                    if(hit.transform.tag == "Carryable")
                    {
                        lasttag = "Carryable";
                        animate.CmdCarry(transform.GetComponent<NetworkIdentity>());
                        ObjCarry = hit.transform.GetComponent<NetworkIdentity>();
                        CmdCarry(hit.transform.GetComponent<NetworkIdentity>(), new Vector3(0,2.5f,0));
                        MyRB2.Carry = true;
                        
                    }
                }


            }


        }

    }
}
