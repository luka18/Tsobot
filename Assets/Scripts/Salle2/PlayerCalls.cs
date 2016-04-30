using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Collections;

public class PlayerCalls : NetworkBehaviour {

    Chat lechat;
    void Start()
    {
        lechat = GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<Chat>();
        print("Chat" + lechat.gameObject.name);
    }

     [Command]

     void Cmd_com(string msg)
     {
         Rpc_com(msg);
     }

     [ClientRpc]

     void Rpc_com(string msg)
     {
         lechat.ChatModif(msg);
     }

     public void Send(string msg)
     {
         Cmd_com(msg);
     }
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
    [Command]
    public void CmdGetTouchedChecker(GameObject obj)
    {
        RpcGetTouchedChecker(obj);
    }
    [ClientRpc]
    void RpcGetTouchedChecker(GameObject obj)
    {
        obj.GetComponent<MainCastle>().Reset();
    }

}
