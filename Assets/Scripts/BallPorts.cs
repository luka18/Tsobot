using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class BallPorts : NetworkBehaviour {
    [SerializeField] ButtonsColor bt;
    [SerializeField] SpawnRedRoom Spawnred;
    
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

    void OnCollisionEnter(Collision col)
    {
        if(col.transform.tag == "Ball")
        {
            GoNext();
            Spawnred.NextLevel();
        }
    }
    public void GoNext()
    {
        CmdGoNext();
    }

}
