using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NumGrap : MonoBehaviour {
    int numgrap;
    Text numtex;
    Color red;
    Color green;
    Color yellow;
    void Start()
    {
        numtex = GetComponent<Text>();
        red = new Color(1,0,0);
        green = new Color(0,1,0);
        
        yellow = new Color(1, 0.92f, 0.016f);
    }

    public void SetNum(int lol)
    {
        if (numtex.enabled)
        {
            numgrap = lol;
            numtex.text = numgrap.ToString();
            print("                  NUM OF GRAP+" + numgrap);
            switch (numgrap)
            {
                case 0:
                    print("col0");
                    numtex.color = red;
                    break;
                case 1:
                    print("col1");
                    numtex.color = yellow;
                    break;
                case 2:
                    print("col3");
                    numtex.color = green;
                    break;
            }
        }
    }

}
