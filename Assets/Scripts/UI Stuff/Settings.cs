using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Collections;

public class Settings : MonoBehaviour {
    [SerializeField]
    GameObject GraphPanel;

    [SerializeField]
    GameObject SoundPanel;

    [SerializeField]
    GameObject KeyBoardPanel;

    [SerializeField]
    GameObject MousePanel;

    [SerializeField]
    Text graptext;

    [SerializeField]
    Text SoundText;

    [SerializeField]
    Text KeyBoardText;

    [SerializeField]
    Text MouseText;

    NetworkManager netman;

    [SerializeField]
    GameObject bobscan;

    [SerializeField]
    GameObject mainmenu;
    [SerializeField]
    GameObject Quit;

    Color basecol;
    Color basred;
    

    public void Awake()
    {
        basecol = new Color(225, 225, 225);
        basred = new Color(255, 0, 0);
        netman = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkManager>();
    }

    public void ForceAnim()
    {

    }

    void Reset()
    {
        GraphPanel.SetActive(false);
        SoundPanel.SetActive(false);
        KeyBoardPanel.SetActive(false);
        MousePanel.SetActive(false);
        graptext.color = basecol;
        SoundText.color = basecol;
        KeyBoardText.color = basecol;
        MouseText.color = basecol;
    }
    
    public void OnClickGraph()
    {
        Reset();
        graptext.color = basred;
        GraphPanel.SetActive(true);
        GraphPanel.GetComponent<GrapChanges>().AddCurrent();
    }
    public void OnClickSound()
    {
        Reset();
        SoundText.color = basred;
        SoundPanel.SetActive(true);
    }
    public void OnClickKeyBoard()
    {
        Reset();
        KeyBoardText.color = basred;
        KeyBoardPanel.SetActive(true);
    }
    public void OnClickMouse()
    {
        Reset();
        MouseText.color = basred;
        MousePanel.SetActive(true);
    }

    public void OnClickMainMenu()
    {
        
        mainmenu.SetActive(false);
        Quit.SetActive(false);
        netman.StopHost();
    }



}
