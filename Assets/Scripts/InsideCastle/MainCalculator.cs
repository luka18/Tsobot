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
    [SerializeField]
    Material blue;
    [SerializeField]
    Material def;
    int currentsomme;
    DigitalRuby.ThunderAndLightning.LightningBoltPathScript lightpath;
    Cdonne coord;
    // Use this for initialization
    void Start () {
        Tab0 = new int[4, 4,2] { { { 1, 0 }, { 0, 3 }, { 0, 1 }, { 0, 6 } }, { { 0, 2 }, { 0, 5 }, { 0, 4 }, { 0, -3 } }, { { 0, -4 }, { 0, 5 }, { 0, -2 }, { 0, 1 } }, { { 0, 8 }, { 0, 2 }, { 0, 3 }, { 0, 8 } } };
        coord = new Cdonne(0, 0);
        lightpath = GetComponent<DigitalRuby.ThunderAndLightning.LightningBoltPathScript>();
        currentsomme = 0;
	}

    public void Left()
    {
        if (coord.x - 1 >= 0  )
        {
            coord.x -= 1;
            if (Tab0[coord.x , coord.y, 0] == 0)
            {
                Tab0[coord.x , coord.y, 0] = 1;
                currentsomme += Tab0[coord.x, coord.y, 1];
                lightpath.LightningPath.List.Add(GetGO());
                GetGO().GetComponent<Renderer>().material = blue;
            }
            else if (Tab0[coord.x, coord.y, 0] == 1)
            {
                Tab0[coord.x, coord.y, 0] = 0;
                currentsomme -= Tab0[coord.x, coord.y, 1];
                lightpath.LightningPath.List.RemoveAt(lightpath.LightningPath.List.Count - 1);
            }
            print("currentscore" + currentsomme);
        }
    }
    public void Right()
    {
        if (coord.x + 1 < 4)
        {
            coord.x += 1;
            if (Tab0[coord.x, coord.y, 0] == 0)
            {
                Tab0[coord.x, coord.y, 0] = 1;
                currentsomme += Tab0[coord.x, coord.y, 1];
                lightpath.LightningPath.List.Add(GetGO());
                GetGO().GetComponent<Renderer>().material = blue;
            }
            else if (Tab0[coord.x, coord.y, 0] == 1)
            {
                Tab0[coord.x, coord.y, 0] = 0;
                currentsomme -= Tab0[coord.x , coord.y, 1];
                lightpath.LightningPath.List.RemoveAt(lightpath.LightningPath.List.Count - 1);
            }
            print("currentsomme" + currentsomme);
        }
    }
    public void Up()
    {
        if (coord.y - 1 >= 0)
        {
            coord.y -= 1;
            if (Tab0[coord.x, coord.y, 0] == 0)
            {
                Tab0[coord.x, coord.y, 0] = 1;
                currentsomme += Tab0[coord.x, coord.y, 1];
                lightpath.LightningPath.List.Add(GetGO());
                GetGO().GetComponent<Renderer>().material = blue;
            }
            else if (Tab0[coord.x, coord.y, 0] == 1)
            {
                Tab0[coord.x, coord.y, 0] = 0;
                currentsomme -= Tab0[coord.x, coord.y, 1];
                lightpath.LightningPath.List.RemoveAt(lightpath.LightningPath.List.Count - 1);
            }
            print("currentscore" + currentsomme);
        }
    }
    public void Down()
    {
        if (coord.y + 1 < 4)
        {
            coord.y += 1;
            if (Tab0[coord.x, coord.y, 0] == 0)
            {
                Tab0[coord.x, coord.y, 0] = 1;
                currentsomme += Tab0[coord.x, coord.y, 1];
                lightpath.LightningPath.List.Add(GetGO());
                GetGO().GetComponent<Renderer>().material = blue;
            }
            else if (Tab0[coord.x, coord.y, 0] == 1)
            {
                Tab0[coord.x, coord.y, 0] = 0;
                currentsomme -= Tab0[coord.x, coord.y, 1];
                lightpath.LightningPath.List.RemoveAt(lightpath.LightningPath.List.Count - 1);
            }
            print("currentsomme" + currentsomme);
        }
    }


    GameObject GetGO()
    {
        return transform.GetChild(coord.x).GetChild(coord.y).gameObject;
    }
}
