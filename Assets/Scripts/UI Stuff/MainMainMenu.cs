using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Networking;

public class MainMainMenu : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    List<GameObject> limode = new List<GameObject>();

    NetworkManager netman;

    [SerializeField]
    GameObject notdes;

    [SerializeField]
    MainRotate Transi;



    GameObject set;
    [SerializeField]
    SwapScene SwapSc;

    enum Mode
    {
        Play,
        Multi,
        Settings,
        Quit,
        None
    };

    MainMenu multi;
    SelectLvl selectlvl;
    Settings settinscript;

    Mode GM = Mode.None;



	void Start () {
        selectlvl = limode[0].GetComponent<SelectLvl>();
        multi = limode[1].GetComponent<MainMenu>();
        
        if (limode[2] != null)
            set = limode[2];
        print("HOLOLO" + set);
        settinscript = set.GetComponent<Settings>();
        netman = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkManager>();
        multi.getmanag(netman); 
        
	}

    public void GimmeSet(GameObject imset)
    {
        print("IVEGOTTHESTUFFYOH");
        set = imset;
        print("set" + set);
    }
	
    

	void DisableActiveGM()
    {
        
        switch(GM)
        {
            case Mode.Play:
                {
                    limode[0].SetActive(false);
                    break;
                }
            case Mode.Multi:
                {
                    limode[1].SetActive(false);
                    break;
                }
            case Mode.Settings:
                {
                    set.SetActive(false);
                    break;
                }
            
        }
    }
  
    void ActivateStuff()
    {
        notdes.SetActive(true);
        SwapSc.SetActCan();
    }


    public void Play()
    {
        if (GM == Mode.Multi)
            netman.StopMatchMaker();
        if(GM == Mode.Play)
        {
            if (!limode[0].activeInHierarchy)
            {
                SwapSc.OverlayCan();
                ActivateStuff();
                set.SetActive(false);
                SwapSc.SetParent(); // SET PARENT SETTINGS TO BOBSCANVAS
                print(set.transform.parent.name);
                netman.StartHost();
            }
            else
            {
                print("yay");
            }
        }
        else
        {
            print("wtfman");
            Transi.JustClose();
            StartCoroutine(Cor700mil(Mode.Play));

        }

    }


    public void Multiplayer()
    {
        
        if (GM == Mode.Multi)
        {
            if (!limode[1].activeInHierarchy)
            {
                
                limode[1].SetActive(true);
                limode[1].GetComponent<MainMenu>().Enablematchmaking();

            }
            else
            {
                multi.ForceAnim();
                print("forced");   
            }
        }
        else
        {
            Transi.Transtition();
            StartCoroutine(Cor700mil(Mode.Multi));
        }
    }
    public void Settings()
    {
        if (GM == Mode.Multi)
            netman.StopMatchMaker();
        if (GM == Mode.Settings)
        {
            if (!set.activeInHierarchy)
            {
                set.SetActive(true);
            }
            else
            {
                settinscript.ForceAnim();
            }
        }
        else
        {
            Transi.Transtition();
            StartCoroutine(Cor700mil(Mode.Settings));

        }

    }
    public void Quit()
    {
        if (GM == Mode.Multi)
            netman.StopMatchMaker();
        Application.Quit();
        print("quitting");
    }

    IEnumerator mil()
    {
        yield return new WaitForSeconds(0.7f);
        
    }

    IEnumerator Cor700mil(Mode lol)
    {
        yield return new WaitForSeconds(0.7f);


        DisableActiveGM();
        switch(lol)
        {
            case Mode.Play:
                {
                    GM = Mode.Play;
                    Play();
                    break;
                }
            case Mode.Multi:
                {
                    GM = Mode.Multi;
                    Multiplayer();
                    break;
                }
            case Mode.Settings:
                {
                    GM = Mode.Settings;
                    Settings();
                    break;
                }
            case Mode.Quit:
                {
                    Quit();
                    break;
                }

        }
    }

}
