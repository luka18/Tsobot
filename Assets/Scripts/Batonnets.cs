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
                    MeshRenderer render = transform.GetChild(advanced).GetComponent<MeshRenderer>();
                    render.material = blu;
                    advanced++;
                }
                if (advanced == 10)//win
                {
                    howmany = 0;
                    button.SetActive(true);
                    CmdActivateButton();
                    TotalStop = true;
                }
                if (advanced >= 7)// lose 
                {
                    redplays = (10 - advanced);
                }
                redplays = (10 - advanced) % 4;
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
        playin = false;

    }
     public static int gethowmany
    {
        get { return howmany; }
        set { howmany = value; }
    }

}
