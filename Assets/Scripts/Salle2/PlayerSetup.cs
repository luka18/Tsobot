using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {
    [SerializeField]
    Behaviour[] componentsToDisable;
    [SerializeField]
    GameObject PrefCan;


    LocalP localp;

    Camera sceneCamera;


    void Start()
    {
 
        print("s");
        if(!isLocalPlayer)
        {
            for (int i =0; i <componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
            


        }
        else
        {
            GameObject.FindGameObjectWithTag("LocalP").GetComponent<LocalP>().Setlocal(gameObject);
            transform.GetChild(2).gameObject.SetActive(false);
            sceneCamera = Camera.main;
            if (sceneCamera!=null)
            {
                sceneCamera.gameObject.SetActive(false);
            }
        }

    }

}
