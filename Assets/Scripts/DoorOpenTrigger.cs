using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DoorOpenTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject door;
    Vector3 startpos;
    Vector3 endpos;
    public float speed = 0.1F;

    void OnTriggerEnter(Collider col)
    {
        print("gné ?");
        if (col.tag == "Player")
        {
            print("findya");
            col.GetComponent<PlayerCalls>().CmdOpenDoorNextlvl(gameObject);
        }
    }
    public void gopen()
    {
        GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkManager>().ServerChangeScene("InsideCastle2");
    }
}
