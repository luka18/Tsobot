using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    GameObject Serverpre;
    [SerializeField]
    InputField Ipseudo;
    [SerializeField]
    InputField IRName;

 
    NetworkManager manager;

    [SerializeField]
    GameObject passwordText;
    [SerializeField]
    GameObject passwordInp;

    [SerializeField]
    GameObject passwordText2;
    [SerializeField]
    GameObject passwordInp2;

    [SerializeField]
    MainRotate transi;
    [SerializeField]
    GameObject notdec;
    [SerializeField]
    SwapScene swapsc;


    List<GameObject> ListServ = new List<GameObject>();
    int ypos = -150;
    [SerializeField]
    Transform Roomback; // awesomelist

    string pseudo = "Unnamed";
    string roomname = "Unnamed";
    string password = "";
    string joinpass = "";

    int selected = -1;

    public void getmanag(NetworkManager manag)
    {
        manager = manag;
        
    }



    public void CreateOnLine()
    {
        manager.matchName = roomname;
        manager.matchHost = pseudo;
        transi.JustClose();
        swapsc.Set.name = pseudo;
        StartCoroutine(Creating());
    }

    public void ForceAnim()
    {
        foreach(AnimScaleAwake k in GetComponentsInChildren<AnimScaleAwake>())
        {
            k.Anim();
        }
        foreach (AnimButAwake k in GetComponentsInChildren<AnimButAwake>())
        {
            k.Anim();
        }
    }

    
    public void Enablematchmaking()
    {
        manager.StartMatchMaker();
        RefreshOnLine();
    }


   public void RefreshOnLine()
    {
        manager.matchMaker.ListMatches(0, 20, "", manager.OnMatchList);
        selected = -1;
        if (manager.matches != null)
        {
            foreach(GameObject serv in ListServ)
            {
                Destroy(serv);
            }
           ListServ.Clear();
            for(int k =0; k<manager.matches.Count; k++)
            {
                GameObject serv = (GameObject)Instantiate(Serverpre, new Vector3(0, ypos, 0), Quaternion.Euler(0, 0, 0));
                serv.transform.SetParent(Roomback, false);
                ListServ.Add(serv);
                int index = k;
                print(index);
                serv.transform.GetChild(0).GetComponent<Text>().text = " " + manager.matches[k].name;
                serv.transform.GetChild(1).GetComponent<Text>().text = " "+ manager.matches[k].currentSize + " / " + manager.matches[k].maxSize;
                if(!manager.matches[k].isPrivate)
                {
                    ListServ[k].transform.GetChild(2).gameObject.SetActive(false);
                }
                serv.GetComponent<Button>().onClick.AddListener(() => OnServClick(index));
                ypos -= 50;
            }
            ypos = -150;
        }
    }




    public void OnServClick(int num)
    {
        if(selected != -1)
            ListServ[selected].GetComponent<Image>().color = new Color(1, 1, 1, 0.039f);
        ListServ[num].GetComponent<Image>().color = new Color(0.86f, 0.11f, 0.11f, 0.39f);
        selected = num;
        print(selected); 
        if(manager.matches[selected].isPrivate)
        {
            passwordInp2.SetActive(true);
            passwordText2.SetActive(true);
        }
        else
        {
            passwordInp2.SetActive(false);
            passwordText2.SetActive(false);
        }
    }

    public void JoinServ()
    {
        print(selected + "s");
        if (selected != -1)
        {
            swapsc.Set.name = pseudo;
            transi.JustClose();
            StartCoroutine(Joining(selected));
        }
    }

    public void OnJoinPass(string s)
    {
        
        joinpass = s;
        print(joinpass);
    }

    public void OnPassword()
    {
        password = passwordInp.GetComponent<InputField>().text;
    }

    public void OnChangePseud()
    {
        pseudo = Ipseudo.text;
    }

    public void OnchangeRoomName()
    {
        roomname = IRName.text;
    }
    public void OnPrivate(bool k)
    {
        if (k)
        {
            
            passwordInp.SetActive(true);
            passwordText.SetActive(true);
            passwordInp.GetComponent<AnimButAwake>().Anim();
            passwordText.GetComponent<AnimScaleAwake>().Anim();
        }
        else
        {
            passwordInp.SetActive(false);
            passwordText.SetActive(false);

        }
        
    }


    IEnumerator Creating()
    {
        print("increating");
        yield return new WaitForSeconds(0.7f);
        notdec.SetActive(true);
        manager.matchMaker.CreateMatch(manager.matchName, manager.matchSize, false, password, manager.OnMatchCreate);
    }


    IEnumerator Joining(int num)
    {
        yield return new WaitForSeconds(0.7f);
        notdec.SetActive(true);
        if(!manager.matches[num].isPrivate)
            manager.matchMaker.JoinMatch(manager.matches[num].networkId, "", manager.OnMatchJoined);
        else
        {
            
            //manager.matchMaker.JoinMatch(manager.matches[num].networkId, joinpass,)
        }
    }

    void CallBackJoin(JoinMatchResponse l)
    {
        print("starting");
        foreach (var j in l.extendedInfo)
        {
            print(j);
        }
    }
    


    //SETTINGS------------------------------------------------------------------------------------------------------------------
    /*public void OnSettings()
    {
        diaph.Transtition();
        StartCoroutine(mycor2());
    }

     IEnumerator mycor2()
    {
        PlayVideo vi = VideoMana.GetComponent<PlayVideo>();
        yield return new WaitForSeconds(0.7f);
        VideoMana.SetActive(true);
        vi.PlaySet();

    }*/
}