using UnityEngine;
using System.Collections;

public class AddForcePlayerMat : MonoBehaviour {

    public int force;

    void Update()
    {
        Debug.DrawRay(transform.position, transform.up, Color.red, 1);
    }
    
    void OnCollisionEnter(Collision col)
    {
        
        if (col.transform.tag == "Player")
        {
            col.transform.GetComponent<RB2>().SetControl(false);
            col.rigidbody.velocity = new Vector3(0, 0, 0);
            col.rigidbody.AddForce((transform.up) * force, ForceMode.VelocityChange);
            //Debug.DrawRay(transform.position, transform.up * force, Color.blue, 4.0f);
            //col.rigidbody.velocity = new Vector3(col.rigidbody.velocity.x*transform.forward.x, col.rigidbody.velocity.y, col.rigidbody.velocity.z*transform.forward.z);
        }
    }
}
