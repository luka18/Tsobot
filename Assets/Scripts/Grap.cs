using UnityEngine;
using System.Collections;

public class Grap : MonoBehaviour {

    Rigidbody rb;
    Transform cam;
    RB2 rb2;

    // Use this for initialization
    void Start()
    {
        rb2 = transform.GetComponent<RB2>();
        cam = transform.GetChild(0).transform;
        rb = transform.GetComponent<Rigidbody>();
        print(rb);
    }

    // Update is called once per frame
    void Update()
    {
        print(rb);
        RaycastHit hit;
        if (Input.GetButtonDown("Grab"))
        {
            if (Physics.Raycast(transform.position, Vector3.forward, 100))
            {
                rb2.SetControl(false);
                Debug.DrawRay(transform.position, transform.forward, Color.black, 1.0f);
                rb.AddForce(cam.forward * 15, ForceMode.VelocityChange);
            }
        }

    }
}

