using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {
    [SerializeField]
    Behaviour[] componentsToDisable;
    [SerializeField]
    GameObject PrefCan;

    Camera sceneCamera;


    [Command]
    void CmdCan()
    {
        GameObject obj = (GameObject)Instantiate(PrefCan);
        NetworkServer.SpawnWithClientAuthority(obj, gameObject);

    }

        void Start()
    {
        CmdCan();

        if(!isLocalPlayer)
        {
            for (int i =0; i <componentsToDisable.Length; i++)
            {
                print("mdr");
                print(componentsToDisable[i].name);
                componentsToDisable[i].enabled = false;
            }
            


        }
        else
        {
            transform.GetChild(2).gameObject.SetActive(false);
            print("inst local player");
            sceneCamera = Camera.main;
            print(sceneCamera);
            if (sceneCamera!=null)
            {
                sceneCamera.gameObject.SetActive(false);
            }
        }

    }

    /*void OnDisable()
    {
        sceneCamera = Camera.main;
        print("OnDisable");
        sceneCamera.gameObject.SetActive(true);
    }*/

}
