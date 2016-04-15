using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class OnFinishLoad : NetworkBehaviour {
    [SyncVar(hook = "OnLoadChange")]
    int numloaded = 0;

    void Start()
    {
       numloaded++;
    }

    private void OnLoadChange(int lol)
    {
        numloaded = lol;
        print("onhook");
        if(numloaded >= Network.connections.Length)
        {
            StartCoroutine(waitplz());
            print("should open");
        }
    }
    IEnumerator waitplz()
    {
        yield return new WaitForSeconds(1);
        transform.GetComponent<OpenTheDoor>().Open();
    }

}
