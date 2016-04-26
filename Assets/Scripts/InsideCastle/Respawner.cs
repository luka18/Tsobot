using UnityEngine;
using System.Collections;

public class Respawner : MonoBehaviour {

    private Vector3 spawner;
	
    void Start()
    {
        spawner = new Vector3(-5, 0.5f, 0);
    }
    public Vector3 Spawner
    {
        get { return spawner; }
        set { spawner = value; }
    }

    void OnTriggerEnter(Collider col)
    {
        print("collided"+col.transform.tag);
        if(col.transform.tag =="Player")
        {
            col.transform.position = spawner;
        }
    }
}
