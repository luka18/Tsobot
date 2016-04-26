using UnityEngine;
using System.Collections;

public class Portail : MonoBehaviour {
    [SerializeField]
    Material red;
    [SerializeField]
    Respawner lol;
    bool passed = false;
    [SerializeField]
    Vector3 spawnpoint;
	
    void OnTriggerEnter(Collider col)
    {
        if(col.tag =="Player")
        {
            if(!passed)
            {
                transform.GetChild(0).GetComponent<Renderer>().material = red;
                passed = true;
                lol.Spawner = spawnpoint;
            }
        }
    }
}
