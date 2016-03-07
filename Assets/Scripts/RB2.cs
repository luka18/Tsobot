using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class RB2 : MonoBehaviour
{
    
    Rigidbody Myrigidbody;
    float vertical;
    float horizontal;
    public float speed = 5.0f;
    public float sprintspeed = 7.5f;
    public float JumpHeight = 20.0f;
    private float jump = 0.0f;
    private bool slowdown = false;
    private Vector3 forcetoadd;
    private Vector3 desiredmove;
    
    public int aircontrol = 2;
    
    
    // LA VISION
    private float rotLR;
    private float Mousespeed = 5.0f;
    private float upDownRange = 60.0f; // Maximum range to go from the middle to the bottom/top 
    private float verticalRotation;
    private Transform cam;
    private Transform Head;
    private Transform Robot;
    private bool ff = false;
    

    //GROUNDCHECK
     bool grounded = false;
    private BoxCollider box1, box2;
    private bool cangoup = true;
    public int maxSlope = 65;
    private bool cancontrol = true;

    //CROUCHING
    bool Crouched = false;
    private float deltat = 0;
    private bool goinup = false;
    private float timetogoup = 0;
    private bool GoOnce = true;
    //scripts
    public HeadTtrigger ht;

    //Animation
    [SerializeField]
    Animator2 animate;
    private bool jumping = false;
    float TimeToLand = 0.3f;
    private float Timebuff;
    private bool once = true;
    private bool carry;

    private NetworkIdentity MyNetID;

    //Shootin
    
    //LES BOUTONS
    public List<GameObject> ButtonList = new List<GameObject>();


    private BoxCollider Mycollider;
    public bool Carry
    {
        get { return carry; }
        set { carry = value; }
    }

    // Use this for initialization
    void Start()
    {
        Myrigidbody = GetComponent<Rigidbody>();
        cam = transform.FindChild("Camera");
        Mycollider = GetComponent<BoxCollider>();
        Robot = transform.Find("IDDLE OFI");
        print("one");
        //anim = transform.FindChild("IDDLE OFI").GetComponent<Animator>();
        MyNetID = transform.GetComponent<NetworkIdentity>();

        //Head = transform.Find("Iddle4").transform.Find("joint7").transform.Find("joint4").transform.Find("mnr_120:mnr_120:neck").transform.Find("polySurface104").GetComponent<Transform>();
        print("two");
        
        

    }

    public bool GetControl()
    {
        return cancontrol;
    }

    public void SetControl(bool cani)
    {
        cancontrol = cani;
    }

    // Update is called once per frame
    void Update()
    {

        print(cancontrol+"cani");
        Mouselook();

        if(!grounded )
        {
            if (!once)
            {
                Timebuff = Time.time;
                print("TOP");
                once = true;
            }
        }
        else if(once)
        {
            print("BUFFING");
            if(Time.time-Timebuff >TimeToLand)
            {
                print("SHOULD ANIMATED LOL");
                if (!carry&& !Crouched)
                {
                    animate.CmdLand(MyNetID);
                }
                    once = false;
                
            }
            
        }
       

        if (Input.GetButtonDown("Jump") & grounded & (!Crouched))
        {
            
            jump = JumpHeight;
            if (!carry)
            {
                animate.CmdJump(MyNetID);
            }
            
        } 



        if (Input.GetButtonDown("Crouch"))
        {

            animate.CmdCrouch(MyNetID);
            print("crouched");

            Crouched = true;
            Mycollider.size = new Vector3(1, 2.0f, 1);
            ht.CrouchPlease();
           
            if (grounded)
            {
                speed = 2.5f;
            }
            
            
            
        }

        if (!(Input.GetButton("Crouch"))&& Crouched)
        {
            if (HeadTtrigger.goup && grounded)
            {
                transform.Translate(0, 0.25f, 0);
                ht.UnCrouchPlease();
                animate.CmdUnCrouch(MyNetID);
                speed = 5.0f;
                Mycollider.size = new Vector3(1, 2.5f, 1);
                
                Crouched = false;
                goinup = true;
       
            }
        }

        /*if(goinup)
        {
            deltat += Time.deltaTime*1.2f;
            
            Mycollider.size = Vector3.Lerp(new Vector3(1, 2,1), new Vector3(1, 2.5f, 1), deltat);
            

            if (deltat >1)
            {
                deltat = 0;
                goinup = false;
            }
                

        }*/



        if (speed == 10)
        {
            if (horizontal != 0 & vertical>0)
                speed = 5.0f;
        }
      
        if (Input.GetButton("Sprint"))
        {
            if (grounded & vertical > 0 & horizontal == 0 & !Crouched)
            {
                speed = sprintspeed;
                cancontrol = false;
            }
           
          
        }
        if (Input.GetButtonUp("Sprint") || slowdown) 
        {
            
            speed = 5.0f;
            slowdown = false;
            cancontrol = true;
           
        }

        // les variable à update chaque frame

    }
    void FixedUpdate()
    {

        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        if (grounded)
        {
            desiredmove = (transform.forward * vertical + transform.right * horizontal) * speed;
            desiredmove = Vector3.ClampMagnitude(desiredmove, speed);
            forcetoadd = new Vector3(desiredmove.x - Myrigidbody.velocity.x, jump, desiredmove.z - Myrigidbody.velocity.z);
            Myrigidbody.AddForce(forcetoadd, ForceMode.VelocityChange);

        }

        else if (cancontrol)
        {
            desiredmove = (transform.forward * vertical + transform.right * horizontal) * speed;
            desiredmove = Vector3.ClampMagnitude(desiredmove, speed);
            forcetoadd = new Vector3(desiredmove.x - Myrigidbody.velocity.x, 0, desiredmove.z - Myrigidbody.velocity.z) / 5;
            Myrigidbody.AddForce(forcetoadd, ForceMode.VelocityChange);
            print("in jump");
        }

        
        
      
        jump = 0.0f;
        // les debug vecteur movement
        Debug.DrawRay(desiredmove + new Vector3(0, 1, 0), forcetoadd, Color.white, 1.0f);
        Debug.DrawRay(transform.position + new Vector3(0, 1, 0), desiredmove, Color.green, 1.0f);
        Debug.DrawRay(Myrigidbody.transform.position + new Vector3(0, 1, 0), Myrigidbody.velocity, Color.red, 1.0f);



    }
    void Mouselook()
    {
        float rotLeftRight = Input.GetAxis("Mouse X") * Mousespeed;
        transform.Rotate(0, rotLeftRight, 0);
        verticalRotation -= Input.GetAxis("Mouse Y") * Mousespeed;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
   
        
        cam.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);


        if (grounded && !Crouched)
        {
            
            
            //Robot.localRotation = Quaternion.Euler(0, 0, -verticalRotation / 15);
        }
           


    }




    /*void OnCollisionStay(Collision coll)
    {
        print(coll.contacts.Length + "len");
        foreach (ContactPoint contact in coll.contacts)
        {
            print(contact.otherCollider.name+ "COLLIDED ME");
            if(Vector3.Angle(contact.normal,Vector3.up)<maxSlope)
            {
                
                if(grounded&&jumping)
                {
                    animate.CmdLand(MyNetID);
                    jumping = false;
                }
                
                grounded = true;
            }
            Debug.DrawRay(contact.point, contact.normal, Color.white,4);
        }
    }*/

    void OnCollisionStay(Collision coll)
        {
            
            if(Vector3.Angle(coll.contacts[0].normal,Vector3.up)<maxSlope)
            {
                if(coll.transform.tag!= "NoGrounded")
                    grounded = true;
            }
        }

    void OnCollisionExit()
    {
        grounded = false;
    }
    



    
}


    
    