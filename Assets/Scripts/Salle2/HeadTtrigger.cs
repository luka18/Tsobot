﻿using UnityEngine;
using System.Collections;

public class HeadTtrigger : MonoBehaviour {
    private int OnMyhead = 0;
    public static bool goup = true;
    BoxCollider boxcol;

    // Use this for initialization
    void Start () {
        boxcol = GetComponent<BoxCollider>();
	}
    public void CrouchPlease()
    {
        boxcol.center = new Vector3(0, 0.25f, 0);
    }

    public void UnCrouchPlease()
    {
        boxcol.center = new Vector3(0, 0.0f, 0);
    }
    void OnTriggerEnter()
    {
        OnMyhead += 1;
        goup = false;
    }
    void OnTriggerExit()
    {
        OnMyhead -= 1;
        if (OnMyhead == 0)
            goup = true;

    }
	
}
