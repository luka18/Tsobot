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
        print("nextLevel");
    }

    [Command]
    private void CmdGoNext()
    {
        RpcGoNext();
        print("cmdggonext");
    }

    void OnCollisionEnter(Collision col)
    {

        if(col.transform.tag == "Ball")
        {
            print("IN THE PORTAL");
            print(col.transform.name);
            print("InServer");
            CmdGoNext();
            Spawnred.NextLevel();
            //CmdGoNext();
        }
    }

}
