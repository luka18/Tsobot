﻿using UnityEngine;
using System.Collections;

public class ChainScript : MonoBehaviour {
    PlayerCalls lp;

    [SerializeField]
    PontLevis lv;

    [SerializeField]
    int MyNum;

    [SerializeField]
    bool StartDisabled;

    AudioSource aud;

    public void Setting(GameObject localp)
    {
        lp = localp.GetComponent<PlayerCalls>();
        if(StartDisabled)
        {
            gameObject.SetActive(false);
        }
    }

    public void Kill()
    {
        Destroy(transform.gameObject);
        lv.GetTouched(MyNum);
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.transform.tag =="Carryable")
        {
            print("Detected");
            lp.CmdKillChain(gameObject);
        }
    }

}
