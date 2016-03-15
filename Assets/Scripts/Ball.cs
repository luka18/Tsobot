using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    void OnCollisionEnter(Collision col)
    {
        if(col.transform.tag  !="Portal")
        {
            Destroy(gameObject);
        }
        
    }
}
