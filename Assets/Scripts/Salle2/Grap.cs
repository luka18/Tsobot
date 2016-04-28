using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using DigitalRuby.ThunderAndLightning;

public class Grap : NetworkBehaviour

{

    Rigidbody rb;
    Transform cam;
    Vector3 reelcam;
    RB2 rb2;
    bool collision = false;
    bool grapping = false;
    int layermask;

    public float smooth ;

    GameObject hitpoint;

    AudioSource aud;

    KeyCode InGrap = KeyCode.Mouse0;

    [SerializeField]
    private int NumOfGrap;

    NumGrap grapui;

    //lighting bolt
    LightningBoltPathScript lbp;

    ParticleSystem particle;
    float i;
    public KeyCode SGrap
    {
        set { InGrap = value; }
    }

    // Use this for initialization
    void Start()
    {
        rb2 = transform.GetComponent<RB2>();
        cam = transform.GetChild(0).transform;
        rb = transform.GetComponent<Rigidbody>();
        layermask = 1 << 9;
        layermask = ~layermask;
        print("s");
        GameObject Lmanag = transform.GetChild(3).gameObject;
        lbp = Lmanag.GetComponent<LightningBoltPathScript>();
        lbp.LightningPath.List[0] = transform.gameObject;
        i = 0.0f;
        //Lmanag.transform.parent = null;
        particle = transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>();
        ResetGrap();
        grapui = GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<NumGrap>();


        //aud = transform.GetChild(3).GetComponent<AudioSource>();
    }

    public void ResetGrap()
    {
        NumOfGrap = 2;
        if (grapui != null)
            grapui.SetNum(NumOfGrap);
    }

    public void AddGrap(int i)
    {
        NumOfGrap += 1;
        if (grapui != null)
            grapui.SetNum(NumOfGrap);

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(InGrap))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + new Vector3(0, 2.0f, 0), cam.transform.forward, out hit, 40,layermask))
            {
                if (hit.transform.tag == "Grappable")
                {
                    particle.Emit(4);
                    if (NumOfGrap > 0)
                    {
                        NumOfGrap -= 1;
                        Debug.Log(hit.transform.tag);
                        reelcam = cam.forward;
                        grapping = true;
                        hitpoint = new GameObject();
                        hitpoint.transform.position = hit.point;
                        lbp.LightningPath.List[1] = hitpoint;
                        CmdLightning(hit.point);
                        if (grapui != null)
                            grapui.SetNum(NumOfGrap);
                    }
                    particle.Emit(4);
                    //aud.Play();
                }
            }
        }
        if (Input.GetKeyUp(InGrap) || collision)
        {
            print("relache");
           
            grapping = false;
            collision = false;
            lbp.LightningPath.List[1] = null;
            
            Destroy(hitpoint);
            CmdLightOff();
            //aud.Stop();
        }
        if (grapping)
        {
            rb2.SetControl(false);
            
            Debug.DrawRay(transform.position, transform.forward, Color.black, 1.0f);
            rb.AddForce((reelcam * 20 - rb.velocity)*smooth, ForceMode.VelocityChange);
            
           
        }
    }

    [ClientRpc]
    void RpcLightning(Vector3 pos)
    {
        if (!isLocalPlayer)
        {
            hitpoint = new GameObject();
            hitpoint.transform.position = pos;
            lbp.LightningPath.List[1] = hitpoint;
            particle.Emit(4);
        }
    }
    [ClientRpc]
    void Rpcoff()
    {
        if (!isLocalPlayer)
        {
            lbp.LightningPath.List[1] = null;
            Destroy(hitpoint);
        }
    }

    
    [Command]
    void CmdLightning(Vector3 pos)
    {

        RpcLightning(pos);   
    }
    [Command]
    void CmdLightOff()
    {
        Rpcoff();
    }


    void OnCollisionEnter()
    {
        if(grapping)
            collision = true;
    }
}



