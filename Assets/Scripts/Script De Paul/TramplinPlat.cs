using UnityEngine;
using System.Collections;

public class TramplinPlat : MonoBehaviour
{

    public int force;

    void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Player")
        {
            col.rigidbody.velocity = new Vector3(col.rigidbody.velocity.x, 0, col.rigidbody.velocity.z);
            col.rigidbody.AddForce((transform.up) * force, ForceMode.VelocityChange);

        }
    }
}
