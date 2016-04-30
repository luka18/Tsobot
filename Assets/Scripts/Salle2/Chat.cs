using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class Chat : MonoBehaviour {

    [SerializeField] 
    Text Text, TextCurrentMsg;

    PlayerCalls pc;

    RB2 Myrb2;

    string msg;

    bool ChatOn;


    KeyCode InChat = KeyCode.Y;

    List<string> list;
    List<string> player_list;


    public KeyCode SChat
    {
        set { InChat = value; }
    }


    private static Chat _instance = null;
    public static Chat Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        DontDestroyOnLoad(gameObject);
        
    }
    

    public void Setting(GameObject obj)
    {
        GameObject lol = obj;
        pc = lol.GetComponent<PlayerCalls>();
        
        Myrb2 = pc.GetComponent<RB2>();
    }

	// Use this for initialization
	void Start ()
    {
        list = new List<string>();
        for (int i = 0; i<10;i++)
        {
            list.Add("");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ChatOn)
        {

            foreach (char c in Input.inputString)
            {
                msg += c;
            }
            TextCurrentMsg.text = msg;
        }
        if (Input.GetKeyDown(InChat))
        {
            print("ChatOn");
            ChatOn = true;
        }

        if (ChatOn && Input.GetButtonDown("Enter_Chat"))
        {
            pc.Send(msg);
        }
    }
    public void ChatModif( string str)
    {
        ChatOn = false;
        list.Add(str);
        msg = "";
        list.RemoveAt(0);
        TextCurrentMsg.text = "";
        string a = "";
        for (int i = 0; i < 10; i++)
        {
            a += list[i] + "\n";
        }
        Text.text = a;
    }

   /* [Command]

    void Cmd_com (string msg)
    {
        Rpc_com(msg);
    }

    [ClientRpc]

    void Rpc_com (string msg)
    {
        ChatModif(msg);
    }*/
}
