﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class SpawnRedRoom : NetworkBehaviour {
    [SerializeField] GameObject PrefCar;
    [SerializeField] GameObject Jumper;
    [SerializeField] GameObject Balance;
    [SerializeField]
    GameObject trig;
    [SerializeField]
    ButtonsColor bt;
    private int CurrentLvl = 1;

    List<GameObject> LvlObj = new List<GameObject>();
    /*
    [Command]
    void CmdJumper(Vector3 pos, int Rot)
    {
        GameObject obj = (GameObject)Instantiate(Jumper, pos, Quaternion.Euler(0, Rot, 0));
        LvlObj.Add(obj);
        NetworkServer.Spawn(obj);
        
    }
    [Command]
    void CmdBalance(Vector3 pos, int Rot)
    {
        GameObject obj = (GameObject)Instantiate(Balance, pos, Quaternion.Euler(0, Rot, 0));
        LvlObj.Add(obj);
        NetworkServer.Spawn(obj);
        bt.SeriRef(obj.transform.GetChild(7).gameObject);
        GameObject tri = (GameObject)Instantiate(trig, new Vector3(-6.673f, 16.92f, 47.5f), Quaternion.Euler(0, 0, 0));
        LvlObj.Add(tri);
        NetworkServer.Spawn(tri);
        tri.transform.parent = obj.transform;
    }

    [Command]
    void CmdCar(Vector3 pos,int Rot)
    {
       GameObject obj =  (GameObject)Instantiate(PrefCar, pos,Quaternion.Euler(0,Rot,0));
       LvlObj.Add(obj);
       NetworkServer.Spawn(obj);
    }
    
    void Start()
    {
        print("s");
        if(isServer)
        {
            //Blu();
        }
    }

    public void Purple()
    {
        CmdCar((new Vector3(6, 12, 43.5f)), 0);
        
        CmdJumper((new Vector3(14, 12, 43.5f)), 0);
        CmdJumper((new Vector3(6, 12, 51.5f)), 0);
    }
    public void DestroyLevel()
    {
        foreach (GameObject k in LvlObj)
        {
            NetworkServer.Destroy(k);
        }
    }
    
    public void Blu()
    {
        CmdCar((new Vector3(6, 12, 51.5f)), 90);
        CmdCar((new Vector3(14, 12, 51.5f)), 90);
        CmdCar((new Vector3(14, 12, 41.5f)), 0);
        CmdJumper((new Vector3(6, 12, 41.5f)), 0);
    }

    public void Green()
    {
      
        CmdCar((new Vector3(16, 12, 41.5f)), 270);
        CmdCar((new Vector3(10, 12, 41.5f)), 0);
        CmdCar((new Vector3(4, 12, 41.5f)), 270);
        CmdCar((new Vector3(4, 12, 47.5f)), 270);
        CmdCar((new Vector3(10, 12, 53.5f)), 270);
        CmdCar((new Vector3(16, 12, 53.5f)), 270);
        CmdJumper((new Vector3(16, 12, 47.5f)), 0);
        
    }

    public void Red()
    {
        CmdJumper((new Vector3(14, 12, 51.5f)),0);
        CmdJumper((new Vector3(14, 12, 43.5f)), 0);
        CmdJumper((new Vector3(6, 12, 51.5f)), 0);
        CmdJumper((new Vector3(6, 12, 43.5f)), 0);
        CmdBalance((new Vector3(-7, 12.68f, 47.5f)), 0);
        
    }
    
    public void NextLevel()
    {
        if (isServer)
        {

            CurrentLvl++;
            if (CurrentLvl < 5)
            {
                DestroyLevel();
                switch (CurrentLvl)
                {
                    case 2:
                        Purple();
                        break;
                    case 3:
                        Green();
                        break;
                    case 4:
                        Red();
                        break;
                }
            }
        }
     
    }
    */
}



