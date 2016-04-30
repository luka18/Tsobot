using UnityEngine;
using System.Collections;


public class AddGrap : MonoBehaviour
{
    public int i;
    Grap grap;

    void OnTriggerEnter(Collider col)
    {
        grap = col.GetComponent<Grap>();
        grap.AddGrap(i);        
    }
}