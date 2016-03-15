using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class BoutonToFunc : NetworkBehaviour {
    [SerializeField]
    OpenTheDoor TheDoor;

    public void Open()
    {
        TheDoor.CmdOpen();
    }
   
    
}
