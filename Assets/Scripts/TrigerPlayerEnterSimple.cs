using UnityEngine;
using System.Collections;

public class TrigerPlayerEnterSimple : MonoBehaviour {
    [SerializeField]
    Vector3 dest;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            col.transform.position = dest;
        }
    }

}