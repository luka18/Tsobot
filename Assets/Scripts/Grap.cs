using UnityEngine;
using System.Collections;

public class Grap : MonoBehaviour

{

    Rigidbody rb;
    Transform cam;
    Vector3 reelcam;
    RB2 rb2;
    bool collision = false;
    bool grapping = false;
    int layermask;



    // Use this for initialization
    void Start()
    {
        rb2 = transform.GetComponent<RB2>();
        cam = transform.GetChild(0).transform;
        rb = transform.GetComponent<Rigidbody>();
        layermask = 1 << 9;
        layermask = ~layermask;
    }

    // Update is called once per frame
    void Update()
    {

        //print(rb);
        if (Input.GetButtonDown("Grab"))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + new Vector3(0, 2.0f, 0), cam.transform.forward, out hit, 100,layermask))
            {
                print(hit.transform.name);
                if (hit.transform.tag == "Grappable")
                {
                    Debug.Log(hit.transform.tag);
                    reelcam = cam.forward;
                    grapping = true;
                }
            }
        }
        if (Input.GetButtonUp("Grab") || collision)
        {
            rb2.SetControl(true);
            grapping = false;
            collision = false;
        }
        if (grapping)
        {
            rb2.SetControl(false);
            Debug.DrawRay(transform.position, transform.forward, Color.black, 1.0f);
            rb.AddForce(reelcam * 15 - rb.velocity, ForceMode.VelocityChange);

        }
    }
    void OnCollisionEnter()
    {
        collision = true;
    }
}



