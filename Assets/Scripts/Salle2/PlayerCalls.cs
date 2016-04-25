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
    [Command]
    public void CmdWhatI(GameObject obj)
    {
        RpcWhatI(obj, obj.GetComponent<AxesMovements>().GetI());
    }
    [ClientRpc]
    void RpcWhatI(GameObject obj, float i)
    {
        obj.GetComponent<AxesMovements>().SetI(i);
    }
}
