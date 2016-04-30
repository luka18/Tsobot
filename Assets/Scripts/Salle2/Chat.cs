using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class Chat : MonoBehaviour
{

    [SerializeField]

    Text Text;

    [SerializeField]

    InputField inputfield;

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
    void Start()
    {
        list = new List<string>();
        for (int i = 0; i < 10; i++)
        {
            list.Add("");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ChatOn)
        {
            print("azcaec" + inputfield.gameObject.name);
            //inputfield.ActivateInputField();
            msg = inputfield.text;
            Myrb2.LockForChat();
        }
        if (Input.GetKeyDown(InChat))
        {
            //print("ChatOn");
            ChatOn = true;
        }

        if (ChatOn && Input.GetButtonDown("Enter_Chat"))
        {

            if (!cheatcode())
            {
                pc.Send(msg);
                print("aaaa");
                //inputfield.DeactivateInputField();
                //inputfield.enabled = false;
                print("bbbb");

            }
            inputfield.text = "";
            msg = "";
            Myrb2.UnlockForChat();
        }
    }

    bool cheatcode()
    {

        if (msg == "/bird")
        {
            if (!Myrb2.Cheat)
            {
                Myrb2.Cheat = true;
                print('a');
            }
            else
            {
                Myrb2.Cheat = false;
            }

            return true;

        }
        return false;

    }

    public void ChatModif(string str)
    {
        ChatOn = false;
        list.Add(str);

        list.RemoveAt(0);

        string a = "";
        for (int i = 0; i < 10; i++)
        {
            a += list[i] + "\n";
        }
        Text.text = a;
    }

}