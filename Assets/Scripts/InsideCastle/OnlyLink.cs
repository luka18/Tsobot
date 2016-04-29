using UnityEngine;
using System.Collections;

public class OnlyLink : MonoBehaviour {

    [SerializeField]
    MainCalculator mc;

    public void SendMc(int i)
    {
        switch(i)
        {
            case 0:
                mc.Left();
                break;
            case 1:
                print("pushedright");
                mc.Right();
                break;
            case 2:
                mc.Up();
                break;
            case 3:
                mc.Down();
                break;
        }
    }
}
