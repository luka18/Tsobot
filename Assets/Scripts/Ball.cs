using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    void OnCollisionEnter(Collision col)
    {
        print(col.transform.name);
        if(col.transform.tag  =="Portal")
        {
            print("touchey");
        }
        else
        {
            
            Destroy(gameObject);
        }
        
    }
}
