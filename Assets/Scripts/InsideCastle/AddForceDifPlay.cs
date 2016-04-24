using UnityEngine;
using System.Collections;

public class AddForceDifPlay : MonoBehaviour
{

    public int force;

    void Start()
    {
    }

    void OnCollisionEnter(Collision col)
    {
        Debug.DrawRay(transform.position, transform.up, Color.red, 4);
        if (col.transform.tag == "Player")
        {
            
            col.transform.GetComponent<RB2>().SetControl(false);
            col.rigidbody.velocity = new Vector3(0, 0, 0);
            col.rigidbody.AddForce(transform.up * force, ForceMode.VelocityChange);
            
            
        }
    }
}