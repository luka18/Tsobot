using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class BoutonToFunc : NetworkBehaviour {
    [SerializeField]
    Openthedoor2 TheDoor;
    void Start()
    {
        print("s");
    }
    public void Open()
    {
        TheDoor.Open();
    }
   
    
}
