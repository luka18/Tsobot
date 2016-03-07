using UnityEngine;
using System.Collections;

public class TeleportPort : MonoBehaviour {

    [SerializeField] GameObject into;


    void OnTriggerEnter(Collider col)
    {
        print("gogoteleportation");
        col.transform.position = into.transform.position;
    }
}
