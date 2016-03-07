using UnityEngine;
using System.Collections;

public class CallMeColors : MonoBehaviour {
    [SerializeField]
    private ButtonsToDoor bd;

    public void call(int num)
    {
        bd.AddColor(num);
    }



}
