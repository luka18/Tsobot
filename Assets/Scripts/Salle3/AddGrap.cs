using UnityEngine;
using System.Collections;


public class AddGrap : MonoBehaviour
{
    public int i;
    Grap grap;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            grap = col.GetComponent<Grap>();
            print(col.name);
            print(grap);
            grap.AddGrap(i);
        }
    }
}