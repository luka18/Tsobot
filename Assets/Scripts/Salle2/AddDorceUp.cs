using UnityEngine;
using System.Collections;

public class AddDorceUp : MonoBehaviour {
    [SerializeField] public int force;

    void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Ball")
        {
            col.rigidbody.velocity = new Vector3(col.rigidbody.velocity.x, 0, col.rigidbody.velocity.z);
            col.rigidbody.AddForce(( transform.up ) * force, ForceMode.VelocityChange);
            //Debug.DrawRay(transform.position, transform.up * force, Color.blue, 4.0f);
            //col.rigidbody.velocity = new Vector3(col.rigidbody.velocity.x*transform.forward.x, col.rigidbody.velocity.y, col.rigidbody.velocity.z*transform.forward.z);
        }
    }
}
