using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Collections;

public class PlayerCalls : NetworkBehaviour {


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
    
    [Command]
    public void CmdKillChain(GameObject obj)
    {
        RpcKillChain(obj);
    }
    [ClientRpc]
    void RpcKillChain(GameObject obj)
    {
        obj.GetComponent<ChainScript>().Kill();
    }
}
