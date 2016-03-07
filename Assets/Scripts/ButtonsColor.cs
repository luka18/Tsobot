using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ButtonsColor : NetworkBehaviour {

    [SerializeField] private Material bludef;
    [SerializeField] private Material viodef;
    [SerializeField] private Material gredef;
    [SerializeField] private Material reddef;
    [SerializeField] private Material bluemi;
    [SerializeField] private Material vioemi;
    [SerializeField] private Material greemi;
    [SerializeField] private Material redemi;

    [SerializeField] GameObject bluball;
    [SerializeField] GameObject purpleball;
    [SerializeField] GameObject greenball;
    [SerializeField] GameObject redball;

    [SerializeField] GameObject button;


    GameObject refe;
    [SerializeField]
    GameObject greenteleport;
    [SerializeField] GameObject portsin;
    [SerializeField] GameObject portsout;
    [SerializeField]
    GameObject WallJump;

    [SerializeField] GameObject unsolved;
    [SyncVar] int currentcolor =1;

    private float wait;
    void Start()//to test
    {
        NextLevel();
        NextLevel();
        NextLevel();
        
    }





    public void Update()
    {
        print(currentcolor);
    }


    public void emicolor(int color)
    {
        switch (color)
        {
            case 1: //bleu
                
                transform.GetChild(0).GetComponent<MeshRenderer>().material = bluemi;
                break;
            case 2:
                if (currentcolor > 1)
                    print("golol ?");
                    transform.GetChild(1).GetComponent<MeshRenderer>().material = vioemi;
                break;
            case 3: 
                if(currentcolor >2)
                    transform.GetChild(2).GetComponent<MeshRenderer>().material = greemi;
                break;
            case 4:
                if(currentcolor>3)
                    transform.GetChild(3).GetComponent<MeshRenderer>().material = redemi;
                break;

        }
    }
    public void defcolor()
    {
        switch (currentcolor)
        {
            case 1:
                transform.GetChild(0).GetComponent<MeshRenderer>().material = bludef;
                break;
            case 2:
                transform.GetChild(1).GetComponent<MeshRenderer>().material = viodef;
                break;
            case 3:
                transform.GetChild(2).GetComponent<MeshRenderer>().material = gredef;
                break;
            case 4:
                transform.GetChild(3).GetComponent<MeshRenderer>().material = reddef;
                break;
        }
    }

    public void NextLevel()
    {
        if(currentcolor == 1)
        {
            WallJump.SetActive(true);
        }
        else
        {
            WallJump.SetActive(false);
        }
        if(currentcolor == 2)
        {
            greenteleport.SetActive(true);
        }
        else
        {
            greenteleport.SetActive(false);
        }
        if (currentcolor < 4)
        {
            

            print("IN NEXT LVL" + currentcolor);
            portsin.transform.GetChild(currentcolor - 1).gameObject.SetActive(false);
            portsout.transform.GetChild(currentcolor - 1).gameObject.SetActive(false);
            defcolor();
            currentcolor += 1;
            portsin.transform.GetChild(currentcolor - 1).gameObject.SetActive(true);
            portsout.transform.GetChild(currentcolor - 1).gameObject.SetActive(true);
            emicolor(currentcolor);
        }
        else
        {
            print("VIIIIIIIIIIIIIIIIIIIIIIIIIIIICTOIRE");
            button.SetActive(true);
        }
       
    }

    public void SeriRef(GameObject spawnnn)
    {
        refe = spawnnn;
    }

    public void dropball(int num)
    {
        if (currentcolor == num)
        {
            if (Time.time > wait)
            {
                switch (num)
                {
                    case 1:
                        Instantiate(bluball, new Vector3(10, 19, 47.5f), new Quaternion());
                        break;
                    case 2:
                        ((GameObject)Instantiate(purpleball, new Vector3(20, 18.5f, 33.5f), new Quaternion())).GetComponent<Rigidbody>().AddForce(0, 0, 15, ForceMode.VelocityChange);
                        break;
                    case 3:
                        Instantiate(greenball, new Vector3(22.6f, 19.5f, 58), new Quaternion());
                        break;
                    case 4:
                        ((GameObject)(Instantiate(redball, refe.transform.position, new Quaternion()))).GetComponent<Rigidbody>().AddForce(7, 0, 0, ForceMode.VelocityChange);
                        break;

                }
            wait = Time.time + 3;
        }
    }


    }

}
