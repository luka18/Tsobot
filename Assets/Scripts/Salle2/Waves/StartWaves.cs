using UnityEngine;
using System.Collections;

public class StartWaves : MonoBehaviour {
    [SerializeField] MainWaves scipt;

    public void go()
    {
        scipt.play2();
    }

}
