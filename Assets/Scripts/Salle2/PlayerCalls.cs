using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Collections;

public class PlayerCalls : NetworkBehaviour {

    [SerializeField]
    List<GameObject> ListObj;

    [SerializeField]
    ArrayList OnLine;


    [ClientRpc]
    public void RpcCall(GameObject obj)
    {
        obj.SendMessage("AfterNet");
    }


    [Command]
    public void CmdCall(GameObject obj)
    {
        RpcCall(obj);
    }
 

}
