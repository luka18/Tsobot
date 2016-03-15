using UnityEngine;
using System.Collections;

public class TeleportPort : MonoBehaviour {

    [SerializeField] GameObject into;


    void OnTriggerEnter(Collider col)
    {
        col.transform.position = into.transform.position;
    }
}
