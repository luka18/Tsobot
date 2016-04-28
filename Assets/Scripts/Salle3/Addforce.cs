using UnityEngine;
using System.Collections;

public class Addforce : MonoBehaviour
{

    void OnTriggerEnter(Collision col)
    {
        col.rigidbody.velocity = new Vector3(col.rigidbody.velocity.x * 10, col.rigidbody.velocity.y * 10, col.rigidbody.velocity.z * 10);
    }
}