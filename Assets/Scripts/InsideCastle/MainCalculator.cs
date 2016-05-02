using UnityEngine;
using System.Collections;

public class MainCalculator : MonoBehaviour {

    int[,,] Tab0;
    int[,,] Tab1;
    int[,,] Tab2;
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
    int[,,] currenttab;
    int currentsomme;
    DigitalRuby.ThunderAndLightning.LightningBoltPathScript lightpath;

    int currentlvl;
    Cdonne coord;
    AudioSource aud;
    [SerializeField]
    GameObject lvl0;
    [SerializeField]
    GameObject lvl1;
    [SerializeField]
    GameObject lvl2;

    Cdonne Tofind;
    // Use this for initialization
    void Start () {
        Tab0 = new int[4, 4,2] { { { 1, 0 }, { 0, 3 }, { 0, 1 }, { 0, 6 } }, { { 0, 2 }, { 0, 5 }, { 0, 4 }, { 0, -3 } }, { { 0, -4 }, { 0, 5 }, { 0, -2 }, { 0, 1 } }, { { 0, 8 }, { 0, 2 }, { 0, 3 }, { 0, 8 } } };
        Tab1 = new int[4, 4, 2] { { { 1, 8 }, { 0, 3 }, { 0, 1 }, { 0, 4 } }, { { 0, 2 }, { 0, 6 }, { 0, -2 }, { 0, 6 } }, { { 0, -3 }, { 0, -5 }, { 0, 3 }, { 0, -2 } }, { { 0, 4 }, { 0, 5 }, { 0, 2 }, { 0, 13 } } };
        Tab2 = new int[4, 4, 2] { { { 1, 13 }, { 0, -5 }, { 0, 3 }, { 0, 3 } }, { { 0, -4 }, { 0, 1 }, { 0, -1 }, { 0, -4 } }, { { 0, 3 }, { 0, -6 }, { 0, 2 }, { 0, -7 } }, { { 0, 5 }, { 0, 2 }, { 0, -3 }, { 0, 4 } } };

        coord = new Cdonne(0, 0);
        lightpath = GetComponent<DigitalRuby.ThunderAndLightning.LightningBoltPathScript>();
        
        currentsomme = 0;
        toadd = 1;
        Tofind = new Cdonne(3, 3);
        aud = GetComponent<AudioSource>();
        currentlvl = 0;
        currenttab = Tab0;
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
                StartCoroutine(winnieannim());
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
        NextLvl();
        calllight.LightningStrike(new Vector3(271, 100, -13), new Vector3(271, 35, -13));
        yield return new WaitForSeconds(0.4f);
        calllight.LightningStrike(new Vector3(271, 100, -13), new Vector3(271, 35, -13));
        elecparticle.StopSpell();
        elecparticle.DeactivateSpell();
    }
    public void NextLvl()
    {
        currentlvl += 1;
        switch(currentlvl)
        {
            case 1:
                currenttab = Tab1;
                lvl0.SetActive(true);
                lvl1.SetActive(true);
                break;
            case 2:
                lvl1.SetActive(false);
                lvl2.SetActive(true);
                currenttab = Tab2;
                break;
        }

    }

   

    GameObject GetGO(int x, int y)
    {
        return transform.GetChild(x).GetChild(y).gameObject;
    }
}
 