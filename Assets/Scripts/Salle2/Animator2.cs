using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Animator2 : NetworkBehaviour
{
    [SerializeField] private Animator anim;

    //-----------------------RPC----------------------------
    [ClientRpc]
    void RpcPush(NetworkIdentity ID)
    {
        Push(ID);
    }
    [ClientRpc]
    void RpcJump(NetworkIdentity ID)
    {
        Jump(ID);
    }
    [ClientRpc]
    void RpcLand(NetworkIdentity ID)
    {
        Land(ID);
    }
    [ClientRpc]
    void RpcCarry(NetworkIdentity ID)
    {
        Carry(ID);
    }
    [ClientRpc]
    void RpcUnCarry(NetworkIdentity ID)
    {
        UnCarry(ID);
    }
    [ClientRpc]
    void RpcCrouch(NetworkIdentity ID)
    {
        Crouch(ID);
    }
    [ClientRpc]
    void RpcUnCrouch(NetworkIdentity ID)
    {
        UnCrouch(ID);
    }



    



    //-------------------------COMMAND--------------------
    [Command]
     public void CmdPush(NetworkIdentity ID)
    {
        RpcPush(ID);
    }
    [Command]
    public void CmdJump(NetworkIdentity ID)
    {
        RpcJump(ID);
    }
    [Command]
    public void CmdLand(NetworkIdentity ID)
    {
        RpcLand(ID);
    }
    [Command]
    public void CmdCarry(NetworkIdentity ID)
    {
        RpcCarry(ID);
    }
    [Command]
    public void CmdUnCarry(NetworkIdentity ID)
    {
        RpcUnCarry(ID);
    }
    [Command]
    public void CmdCrouch(NetworkIdentity ID)
    {
        RpcCrouch(ID);
    }


    [Command]
    public void CmdUnCrouch(NetworkIdentity ID)
    {
        RpcUnCrouch(ID);
    }

    void Start()
    {
        print("s");
    }


    void Land(NetworkIdentity ID)
    {
        ID.gameObject.GetComponent<Animator2>().anim.Play("Land");
    }
    void Jump(NetworkIdentity ID)
    {
        ID.gameObject.GetComponent<Animator2>().anim.Play("Jump");
    }

    void Push(NetworkIdentity ID)
    {
        ID.gameObject.GetComponent<Animator2>().anim.Play("Push");
    }
    void Carry(NetworkIdentity ID)
    {
        ID.gameObject.GetComponent<Animator2>().anim.Play("Carry");
    }
    void UnCarry(NetworkIdentity ID)
    {
        ID.gameObject.GetComponent<Animator2>().anim.Play("UnCarry");
    }
    void Crouch(NetworkIdentity ID)
    {
        ID.gameObject.GetComponent<Animator2>().anim.Play("Crouch");
    }
    void UnCrouch(NetworkIdentity ID)
    {
        ID.gameObject.GetComponent<Animator2>().anim.Play("UnCrouch");
    }
}
    