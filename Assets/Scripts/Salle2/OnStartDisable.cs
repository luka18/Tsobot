﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class OnStartDisable : MonoBehaviour {

	void Start()
    {
        foreach(NetworkTransform k in transform.gameObject.GetComponentsInChildren<NetworkTransform>())
        {
            k.enabled = false;
        }
    }

}
