using UnityEngine;
using System.Collections;

public class TeleportPortInv : MonoBehaviour {

    [SerializeField] GameObject into;


    void OnTriggerEnter(Collider col)
    {
        Rigidbody cor = col.GetComponent<Rigidbody>();
        cor.velocity = cor.velocity*(-1);
        col.transform.position = into.transform.position;

    }   
}
