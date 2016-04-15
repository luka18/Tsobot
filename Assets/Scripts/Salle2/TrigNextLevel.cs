using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class TrigNextLevel : MonoBehaviour
{
    [SerializeField]
    OpenTheDoor op;
    int numofguy;
    NetworkManager netman;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            numofguy += 1;
            IsItReady();
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            numofguy -=1;
        }
    }
    void IsItReady()
    {
        print(Network.connections.Length);
        if(numofguy >= Network.connections.Length)
        {
            op.Close();
            netman = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkManager>();
            StartCoroutine(Waiting());
        }
    }
    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(1.5f);
        netman.ServerChangeScene("Start3");
    }
}