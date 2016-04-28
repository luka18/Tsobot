using UnityEngine;
using System.Collections;

public class AddForceTop : MonoBehaviour {
    [SerializeField]
    int force;
    [SerializeField]
    bool sens;
    Vector3 tosend;
    void Start()
    {
        if (sens)
        {
            tosend = new Vector3(0, 0.5f, 1);
        }
        else
            tosend = new Vector3(0, 0.5f, -1);
    }
    void OnCollisionEnter(Collision col)
    {
        
        if(col.transform.tag =="Player")
        {
            Rigidbody lol= col.transform.GetComponent<Rigidbody>();
            col.transform.GetComponent<RB2>().SetControl(false);
            lol.AddForce(tosend * force, ForceMode.VelocityChange);
        }
    }
}
