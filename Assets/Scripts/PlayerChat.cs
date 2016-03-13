using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerChat : NetworkBehaviour {

    Chat lechat;
    void Start()
    {
        lechat = GameObject.FindGameObjectWithTag("UI").GetComponent<Chat>();
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



}


