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
        localp = GameObject.FindGameObjectWithTag("LocalP").GetComponent<LocalP>();
        print(localp + "LOOOOOCALP");

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
            
            localp.Setlocal(gameObject);
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
