using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;


public class Chat : NetworkBehaviour {

    [SerializeField] 
    Text Text, TextCurrentMsg;

    [SerializeField]
    LocalP lp;

    PlayerChat pc;

    RB2 Myrb2;

    string msg;

    bool ChatOn;

    NetworkClient myClient;

    List<string> list;
    List<string> player_list;

    public void Setting()
    {
        GameObject lol = lp.Getlocal();
        pc = lol.GetComponent<PlayerChat>();
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
        if (Input.GetButtonDown("Chat"))
        {
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

    [Command]

    void Cmd_com (string msg)
    {
        Rpc_com(msg);
    }

    [ClientRpc]

    void Rpc_com (string msg)
    {
        ChatModif(msg);
    }
}
