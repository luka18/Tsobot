using UnityEngine;
using System.Collections;

public class OnTrigEnterPlayer : MonoBehaviour
{
    [SerializeField]
    Vector3 To;

    void OnTriggerEnter(Collider col)
    {
        if(col.tag =="Player")
        {
            col.transform.position = To;
        }
    }

}

