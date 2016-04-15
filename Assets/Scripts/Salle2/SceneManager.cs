using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SceneManager : NetworkBehaviour {

    NetworkManager netman;

	// Use this for initialization
	void Start () {
        {
            netman = transform.GetComponentInParent<NetworkManager>();
            print("s");
        }
	
	}
    public void ChangeScene()
    {
        netman.ServerChangeScene("Start3");
    }


}
