using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class BoutonToFunc : NetworkBehaviour {
    bool Pushed = false;
    [SerializeField]
    OpenTheDoor TheDoor;

    public void Open()
    {
        TheDoor.CmdOpen();
    }
   
    
}
