using UnityEngine;
using System.Collections;

public class AddForceMat : MonoBehaviour {
    public int force;

    void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Ball")
        {
            col.rigidbody.velocity = new Vector3(0, 0, 0);
            col.rigidbody.AddForce((transform.forward + transform.up*2) * force, ForceMode.VelocityChange);
            
        }
    }
}
