using UnityEngine;
using System.Collections;
using DigitalRuby.ThunderAndLightning;

public class Grap : MonoBehaviour

{

    Rigidbody rb;
    Transform cam;
    Vector3 reelcam;
    RB2 rb2;
    bool collision = false;
    bool grapping = false;
    int layermask;

    GameObject hitpoint;


    //lighting bolt
    LightningBoltPathScript lbp;

    ParticleSystem particle;

    // Use this for initialization
    void Start()
    {
        rb2 = transform.GetComponent<RB2>();
        cam = transform.GetChild(0).transform;
        rb = transform.GetComponent<Rigidbody>();
        layermask = 1 << 9;
        layermask = ~layermask;


        GameObject Lmanag = transform.GetChild(3).gameObject;
        lbp = Lmanag.GetComponent<LightningBoltPathScript>();
        lbp.LightningPath.List[0] = transform.gameObject;

        Lmanag.transform.parent = null;
        particle = transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Grab"))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + new Vector3(0, 2.0f, 0), cam.transform.forward, out hit, 100,layermask))
            {
                if (hit.transform.tag == "Grappable")
                {
                    Debug.Log(hit.transform.tag);
                    reelcam = cam.forward;
                    grapping = true;
                    hitpoint = new GameObject();
                    hitpoint.transform.position = hit.point;
                    lbp.LightningPath.List[1] = hitpoint;

                    particle.Emit(4);

                    print(particle.name);
                }
            }
        }
        if (Input.GetButtonUp("Grab") || collision)
        {
            rb2.SetControl(true);
            grapping = false;
            collision = false;
            lbp.LightningPath.List[1] = null;
            Destroy(hitpoint);
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
        if(grapping)
            collision = true;
    }
}



