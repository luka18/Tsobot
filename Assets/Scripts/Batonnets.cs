using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Batonnets : NetworkBehaviour {

    private static int howmany;
    private int advanced = 0;
    public Material blu;
    public Material red;
    public Material grey;
    private int redplays;
    private int i = 0;
    private float wait = 0;
    private bool stop = true;
    private bool playin = false;
    private bool TotalStop = false;

    [SerializeField] GameObject button;

    [Command]
    void CmdActivateButton()
    {
        RpcActivate();
    }
    

    [ClientRpc]
    void RpcActivate()
    {
        button.SetActive(true);
    }


	// Use this for initialization
    public void Plays(int num)
    {
        if (!TotalStop)
        {
            if (!playin)
            {
                playin = true;
                for (int i = 0; i < num; i++)
                {
                    print("le i : " + i);
                    MeshRenderer render = transform.GetChild(advanced).GetComponent<MeshRenderer>();
                    render.material = blu;
                    advanced++;
                    print("add: " + advanced);
                }
                if (advanced == 10)//win
                {
                    print("win");
                    howmany = 0;
                    stop = false;
                    button.SetActive(true);
                    CmdActivateButton();
                    TotalStop = true;
                }
                if (advanced >= 7)// lose 
                {
                    redplays = (10 - advanced);
                }
                redplays = (10 - advanced) % 4;
                print("redplays" + redplays);
                if (redplays == 0)
                    redplays = 1;
                if (!TotalStop)
                {
                    StartCoroutine(mycor2());
                }
            }

        }
    }

    IEnumerator mycor2()
    {

        while (i < redplays)
        {
            yield return new WaitForSeconds(1);
            transform.GetChild(advanced).GetComponent<MeshRenderer>().material = red;
            advanced++;
            i++;
        }
        i = 0;
        if (advanced == 10)
        {

            for (int k = 0; k < 10; k++)
            {
                transform.GetChild(k).GetComponent<MeshRenderer>().material = grey;
                advanced = 0;
            }
        }
        print("passed");
        playin = false;
        print(playin);

    }

/*
    // Update is called once per frame
    void Update () {
        if(howmany !=0)
        {
            if (stop)
            {
                for (int i = 0; i < howmany; i++)
                {
                    print("le i : " + i);
                    MeshRenderer render = transform.GetChild(advanced).GetComponent<MeshRenderer>();
                    render.material = blu;
                    advanced++;
                    print("add: "+advanced);
                }
                if(advanced==10)//win
                {
                    print("win");
                    howmany = 0;
                    stop = false;
                    button.SetActive(true);
                }
                if (advanced >= 7)// lose 
                {
                    redplays = (10 - advanced);
                }

                redplays = (10-advanced) % 4;
                if (redplays == 0)
                    redplays = 1;
                stop = false;
                print("redplays: "+ redplays);
            }
            latency();
            

        }
	}
    private void latency()
    {
        if(i<redplays && wait >1)
        {
            MeshRenderer render2 = transform.GetChild(advanced).GetComponent<MeshRenderer>();
            render2.material = red;
            advanced++;
            wait = 0;
            i++;
        }
        if(i == redplays && wait>1)
        {
            i = 0;
            if(advanced ==10)
            {
                print("lost");
                for(int k = 0; k<10; k++)
                {
                    transform.GetChild(k).GetComponent<MeshRenderer>().material = grey;
                    advanced = 0;
                }
            }
        }
        wait += Time.deltaTime;

    }
    */
     public static int gethowmany
    {
        get { return howmany; }
        set { howmany = value; }
    }

}
