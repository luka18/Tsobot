using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour
{
    public Vector3 respawn;

    void OnCollisionEnter(Collision col)
    {        
        col.transform.position = respawn;        
    }
}