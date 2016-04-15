using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LocalP : MonoBehaviour {

    GameObject localp;

    public GameObject Getlocal()
    {
        return localp;
    }
    public void Setlocal(GameObject obj)
    {
        localp = obj;
        foreach(GameObject k in GameObject.FindGameObjectsWithTag("GetSetting"))
        {
            k.SendMessage("Setting",obj);
        }
    }
}
