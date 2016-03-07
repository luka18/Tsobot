using UnityEngine;
using System.Collections;

public class TeleportPortInv : MonoBehaviour {

    [SerializeField] GameObject into;


    void OnTriggerEnter(Collider col)
    {
        Rigidbody cor = col.GetComponent<Rigidbody>();

        print(cor.velocity+"lol");
        cor.velocity = cor.velocity*(-1);
        print(cor.velocity);
        print("gogoteleportation");
        col.transform.position = into.transform.position;

    }   
}
