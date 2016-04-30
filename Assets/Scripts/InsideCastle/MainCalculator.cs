using UnityEngine;
using System.Collections;

public class MainCalculator : MonoBehaviour {

    int[,,] Tab0;
    public struct Cdonne
    {
        public int x, y;

        public Cdonne(int p1, int p2)
        {
            x = p1;
            y = p2;
        }
    }
    int toadd;
    [SerializeField]
    Material blue;
    [SerializeField]
    Material def;
    [SerializeField]
    DigitalRuby.ThunderAndLightning.LightningParticleSpellScript elecparticle;
    [SerializeField]
    CallLightning calllight;

    int currentsomme;
    DigitalRuby.ThunderAndLightning.LightningBoltPathScript lightpath;
    

    Cdonne coord;
    AudioSource aud;

    Cdonne Tofind;
    // Use this for initialization
    void Start () {
        Tab0 = new int[4, 4,2] { { { 1, 0 }, { 0, 3 }, { 0, 1 }, { 0, 6 } }, { { 0, 2 }, { 0, 5 }, { 0, 4 }, { 0, -3 } }, { { 0, -4 }, { 0, 5 }, { 0, -2 }, { 0, 1 } }, { { 0, 8 }, { 0, 2 }, { 0, 3 }, { 0, 8 } } };
        coord = new Cdonne(0, 0);
        lightpath = GetComponent<DigitalRuby.ThunderAndLightning.LightningBoltPathScript>();
        
        currentsomme = 0;
        toadd = 1;
        Tofind = new Cdonne(3, 3);
        aud = GetComponent<AudioSource>();
        
        
	}

    public void Left()
    {
        if (coord.x - 1 >= 0  )
        {
            coord.x -= 1;
            if (Tab0[coord.x, coord.y, 0] == 0)
            {
                toadd++;
                Tab0[coord.x, coord.y, 0] = toadd;
                currentsomme += Tab0[coord.x, coord.y, 1];
                lightpath.LightningPath.List.Add(GetGO(coord.x, coord.y));
                GetGO(coord.x, coord.y).GetComponent<Renderer>().material = blue;
            }
            else if (Tab0[coord.x, coord.y, 0] == toadd - 1)
            {
                toadd--;
                Tab0[coord.x + 1, coord.y, 0] = 0;
                currentsomme -= Tab0[coord.x + 1, coord.y, 1];
                lightpath.LightningPath.List.RemoveAt(lightpath.LightningPath.List.Count - 1);
                GetGO(coord.x + 1, coord.y).GetComponent<Renderer>().material = def;
            }
            else
                coord.x += 1;
            CheckMaths();
            print("currentscore" + currentsomme);
            print("currentpos" + coord.x + coord.y);
        }
    }
    public void Right()
    {
        if (coord.x + 1 < 4)
        {
            coord.x += 1;
            if (Tab0[coord.x, coord.y, 0] == 0)
            {
                toadd++;
                Tab0[coord.x, coord.y, 0] = toadd;
                currentsomme += Tab0[coord.x, coord.y, 1];
                lightpath.LightningPath.List.Add(GetGO(coord.x,coord.y));
                GetGO(coord.x,coord.y).GetComponent<Renderer>().material = blue;
            }
            else if (Tab0[coord.x, coord.y, 0] == toadd-1)
            {
                toadd--;
                Tab0[coord.x-1, coord.y, 0] = 0;
                currentsomme -= Tab0[coord.x-1 , coord.y, 1];
                lightpath.LightningPath.List.RemoveAt(lightpath.LightningPath.List.Count - 1);
                GetGO(coord.x-1,coord.y).GetComponent<Renderer>().material = def;
            }
            else
                coord.x -= 1;
            CheckMaths();
            print("currentsomme" + currentsomme);
            print("currentpos" + coord.x + coord.y);
        }
    }
    public void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(winnieannim());
        }
    }
    public void Up()
    {
        if (coord.y - 1 >= 0)
        {
            coord.y -= 1;
            if (Tab0[coord.x, coord.y, 0] == 0)
            {
                toadd++;
                Tab0[coord.x, coord.y, 0] = toadd;
                currentsomme += Tab0[coord.x, coord.y, 1];
                lightpath.LightningPath.List.Add(GetGO(coord.x, coord.y));
                GetGO(coord.x, coord.y).GetComponent<Renderer>().material = blue;
            }
            else if (Tab0[coord.x, coord.y, 0] == toadd - 1)
            {
                toadd--;
                Tab0[coord.x, coord.y + 1, 0] = 0;
                currentsomme -= Tab0[coord.x, coord.y + 1, 1];
                lightpath.LightningPath.List.RemoveAt(lightpath.LightningPath.List.Count - 1);
                GetGO(coord.x, coord.y + 1).GetComponent<Renderer>().material = def;
            }
            else
                coord.y += 1;
            CheckMaths();
            print("currentscore" + currentsomme);
            print("currentpos" + coord.x + coord.y);
        }
    }
    public void Down()
    {
        if (coord.y + 1 < 4)
        {
            coord.y += 1;
            if (Tab0[coord.x, coord.y, 0] == 0)
            {
                toadd++;
                Tab0[coord.x, coord.y, 0] = toadd;
                currentsomme += Tab0[coord.x, coord.y, 1];
                lightpath.LightningPath.List.Add(GetGO(coord.x, coord.y));
                GetGO(coord.x, coord.y).GetComponent<Renderer>().material = blue;
            }
            else if (Tab0[coord.x, coord.y, 0] == toadd - 1)
            {
                toadd--;
                Tab0[coord.x, coord.y - 1, 0] = 0;
                currentsomme -= Tab0[coord.x, coord.y - 1, 1];
                lightpath.LightningPath.List.RemoveAt(lightpath.LightningPath.List.Count - 1);
                GetGO(coord.x, coord.y - 1).GetComponent<Renderer>().material = def;
            }
            else
                coord.y -= 1;
            CheckMaths();

            print("currentsomme" + currentsomme);
            print("currentpos" + coord.x + coord.y);
        }
    }
    void CheckMaths()
    {
        if (coord.x == 3 && coord.y == 2 || coord.y == 3 && coord.x == 2)
        {
            if (currentsomme == Tab0[Tofind.x, Tofind.y, 1])
            {
                //do wini stuff
                lightpath.LightningPath.List.Add(GetGO(3, 3));
                aud.Play();
                elecparticle.ActivateSpell();
                elecparticle.CastSpell();
            }
        }
    }
    IEnumerator winnieannim()
    {
        elecparticle.ActivateSpell();
        elecparticle.CastSpell();
        yield return new WaitForSeconds(0.4f);
        print("GO ?");
        calllight.LightningStrike(new Vector3(302, 100, -13), new Vector3(302, 37, -13));
        yield return new WaitForSeconds(0.4f);
        calllight.LightningStrike(new Vector3(290, 100, -12), new Vector3(290, 36, -12));
        yield return new WaitForSeconds(0.4f);
        calllight.LightningStrike(new Vector3(271, 100, -13), new Vector3(271, 35, -13));
        yield return new WaitForSeconds(0.4f);
        calllight.LightningStrike(new Vector3(271, 100, -13), new Vector3(271, 35, -13));
        elecparticle.StopSpell();
        elecparticle.DeactivateSpell();
    }


   

    GameObject GetGO(int x, int y)
    {
        return transform.GetChild(x).GetChild(y).gameObject;
    }
}
 