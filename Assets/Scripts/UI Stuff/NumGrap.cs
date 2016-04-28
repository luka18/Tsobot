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
        red = new Color(255,16,0);
        green = new Color(19, 255, 0);
        yellow = new Color(251, 255, 0);
    }

    public void SetNum(int lol)
    {
        numgrap = lol;
        numtex.text = numgrap.ToString();
        switch (numgrap)
        {
            case 0:
                numtex.color = red;
                break;
            case 1:
                numtex.color = yellow;
                break;
            case 2:
                numtex.color = green;
                break;
        }
    }

}
