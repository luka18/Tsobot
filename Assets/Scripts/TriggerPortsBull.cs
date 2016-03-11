using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TriggerPortsBull : NetworkBehaviour {
    ButtonsColor bt;
    [ClientRpc]
    private void RpcGoNext()
    {
        bt.NextLevel();
    }

    [Command]
    private void CmdGoNext()
    {
        RpcGoNext();
    }
    void Start()
    {
         bt = FindObjectOfType<ButtonsColor>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "Ball")
        {
            GoNext();
            Destroy(col);
            
        }
    }
    public void GoNext()
    {
        CmdGoNext();
    }
}
