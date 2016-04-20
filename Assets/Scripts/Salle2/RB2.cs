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
    private Vector3 forcetoadd;
    private Vector3 desiredmove;
    
    public int aircontrol = 2;
    
    
    // LA VISION
    private float rotLR;
    private float Mousespeed = 5.0f;
    private float YMS = 5.0f;
    private float upDownRange = 60.0f; // Maximum range to go from the middle to the bottom/top 
    private float verticalRotation;
    private Transform cam;
    private Transform Head;
    private bool VisionLocked;
    private CursorLockMode CamLock;
    

    //GROUNDCHECK
     bool grounded = false;
    private BoxCollider box1, box2;
    public int maxSlope = 20;
    private bool cancontrol = true;

    //CROUCHING
    bool Crouched = false;
    //scripts
    public HeadTtrigger ht;

    //Animation
    [SerializeField]
    Animator2 animate;
    float TimeToLand = 0.3f;
    private float Timebuff;
    private bool once = true;
    private bool carry;
  

    private NetworkIdentity MyNetID;

    private GameObject SettingPan;

    //Shootin

    //LES BOUTONS
    //KeyCode InGrap = KeyCode.Mouse0;
    KeyCode InAct = KeyCode.E;
    KeyCode InSprint = KeyCode.LeftShift;
    KeyCode InCrouch = KeyCode.LeftControl;
    KeyCode InJump = KeyCode.Space;

    // Set section mdr
    public KeyCode SAct
    {
        set { InAct = value; }
    }
    public KeyCode SSprint
    {
        set { InSprint = value; }
    }
    public KeyCode SCrouch
    {
        set { InCrouch = value; }
    }

    public KeyCode SJump
    {
        set { InJump = value; }
    }
    public float SMS
    {
        set { Mousespeed = value; }
    }
    public float SYMS
    {
        set { YMS = value; }
    }
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
        MyNetID = transform.GetComponent<NetworkIdentity>();

        //Transform mdr = GameObject.FindWithTag("UI").transform;
        //SettingPan = mdr.GetChild(mdr.childCount - 1).gameObject;
        CamLock = CursorLockMode.Locked;
        Cursor.lockState = CamLock;
        Cursor.visible = false;
        VisionLocked = false;
        

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
       
        Mouselook();

        if(!grounded )
        {
            if (!once)
            {
                Timebuff = Time.time;
                once = true;
            }
        }
        else if(once)
        {
            if(Time.time-Timebuff >TimeToLand)
            {
                if (!carry&& !Crouched)
                {
                    //animate.CmdLand(MyNetID);
                }
                    once = false;
                
            }
            
        }
      

        if (Input.GetKeyDown(InJump) && grounded && (!Crouched))
        {
            
            jump = JumpHeight;
            if (!carry)
            {
                animate.CmdJump(MyNetID);
            }
            
        }
        if (Input.GetKeyDown(InCrouch))
        {
            animate.CmdCrouch(MyNetID);
            Crouched = true;
            Mycollider.size = new Vector3(1, 2.0f, 1);
            ht.CrouchPlease();
            if (grounded)
            {
                speed = 2.5f;
            }
            
        }

        if (!(Input.GetKeyDown(InCrouch)) && Crouched)
        {
            if (HeadTtrigger.goup && grounded)
            {
                transform.Translate(0, 0.25f, 0);
                ht.UnCrouchPlease();
                animate.CmdUnCrouch(MyNetID);
                speed = 5.0f;
                Mycollider.size = new Vector3(1, 2.5f, 1);
                Crouched = false;
       
            }
        }
        if (speed == 10)
        {
            if (horizontal != 0 && vertical>0)
                speed = 5.0f;
        }
      
        if (Input.GetKey(InSprint))
        {
            if (grounded && vertical > 0 && horizontal == 0 && !Crouched)
            {
                speed = sprintspeed;
                cancontrol = false;
            }
           
          
        }
        if (Input.GetKeyUp(InSprint)) 
        {

            speed = 5.0f;
            cancontrol = true;
           
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeLock();
            
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
        }

        
        
      
        jump = 0.0f;
        // les debug vecteur movement
       /* Debug.DrawRay(desiredmove + new Vector3(0, 1, 0), forcetoadd, Color.white, 1.0f);
        Debug.DrawRay(transform.position + new Vector3(0, 1, 0), desiredmove, Color.green, 1.0f);
        Debug.DrawRay(Myrigidbody.transform.position + new Vector3(0, 1, 0), Myrigidbody.velocity, Color.red, 1.0f);*/



    }
    void Mouselook()
    {
        if (!VisionLocked)
        {
            float rotLeftRight = Input.GetAxis("Mouse X") * Mousespeed;
            transform.Rotate(0, rotLeftRight, 0);
            verticalRotation -= Input.GetAxis("Mouse Y") * YMS;
            verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
            cam.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        }
    }
    void ChangeLock()
    {
        if(CamLock == CursorLockMode.Locked)
        {
            Cursor.visible = true;
            CamLock = CursorLockMode.None;
            SettingPan.SetActive(true);
            VisionLocked = true;
        }
        else
        {
            Cursor.visible = false;
            CamLock = CursorLockMode.Locked;
            SettingPan.SetActive(false);
            VisionLocked = false;
            
        }
        Cursor.lockState = CamLock;

    }


    void OnCollisionStay(Collision coll)
        {
            
            if(Vector3.Angle(coll.contacts[0].normal,Vector3.up)<maxSlope)
            {
                if (coll.transform.tag != "NoGrounded")
                {
                    grounded = true;
                }
            }
            else
        {
            grounded = false;
        }
        }

    void OnCollisionExit()
    {
        grounded = false;
    }
    



    
}


    
    