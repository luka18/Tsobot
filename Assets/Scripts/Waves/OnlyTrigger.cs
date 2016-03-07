using UnityEngine;
using System.Collections;

public class OnlyTrigger : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "Player")
        {
            col.transform.position = new Vector3(15, 0.5f, 15);
        } 
    }
}
