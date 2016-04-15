using UnityEngine;
using System.Collections;

public class AddForceMat : MonoBehaviour {
    public int force;

    void Update()
    {
        Debug.DrawRay(transform.position, (transform.forward +transform.up*2) * force, Color.blue, 4.0f);
    }


    void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Ball")
        {
            col.rigidbody.velocity = new Vector3(0, 0, 0);
            col.rigidbody.AddForce((transform.forward + transform.up*2) * force, ForceMode.VelocityChange);
            //Debug.DrawRay(transform.position, transform.up * force, Color.blue, 4.0f);
            //col.rigidbody.velocity = new Vector3(col.rigidbody.velocity.x*transform.forward.x, col.rigidbody.velocity.y, col.rigidbody.velocity.z*transform.forward.z);
        }
    }
}
