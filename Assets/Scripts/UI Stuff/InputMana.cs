using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class InputMana : MonoBehaviour {


    private bool changingkey;

    private float Sensi = 5.0f;

    List<KeyCode> ToChange = new List<KeyCode>();
    private bool InMain = true;

    enum KP
    {
        Act,
        Grap,
        Jump,
        Sprint,
        Crouch,
        Chat,
    };
    KP kchang;

    RB2 MyRb2;
    Grap MyGrap;
    Chat MyChat;
    ColorBlindFilter colfilt;

    private bool Ycheck = false;

    [SerializeField]
    Text[] TexTab = new Text[6];
    [SerializeField]
    Image[] ImTab = new Image[6];

    [SerializeField]
    GrapChanges graphc;

    int index;

    int ToChangeColor;
    
    private static InputMana _instance = null;
    public static InputMana Instance
    {
        get { return _instance; }
    }

    public int ToChangeCol
    {
        set { ToChangeColor = value;
            ChangeCol();
        }
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
    

    void Setting(GameObject lp)
    {
        MyRb2 = lp.GetComponent<RB2>();
        MyGrap = lp.GetComponent<Grap>();
        colfilt = lp.GetComponentInChildren<ColorBlindFilter>();
        MyChat = GameObject.FindGameObjectWithTag("UI").GetComponent<Chat>();
        InMain = false;
        Apply();

        ChangeCol();
        print("have applyed");
    }

    // Update is called once per frame
    void Update()
    {
        if (changingkey)
        {
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(kcode))
                {
                    changingkey = false;
                    TexTab[index].text = kcode.ToString();
                    Color ic = ImTab[index].color;
                    ic.a = 0.06f;
                    ImTab[index].color = ic;
                    if (InMain)
                        ToChange.Add(kcode);
                    else
                    {
                        GotTheKeyYoh(kcode);
                    }
                }
            }
        }
    }


    public void OnClick(int k)
    {
        Color ic = ImTab[index].color;
        ic.a = 0.06f;
        ImTab[index].color = ic;
        index = k;
        Color ik = ImTab[index].color;
        ik.a = 0.5f; 
        ImTab[index].color = ik;
        kchang = (KP)k;
        changingkey = true;
    }
    

    void GotTheKeyYoh(KeyCode ka)
    {
        switch(kchang)
        {
            case KP.Act:
                MyRb2.SAct = ka;
                break;
            case KP.Grap:
                MyGrap.SGrap = ka;
                break;
            case KP.Jump:
                MyRb2.SJump = ka;
                break;
            case KP.Sprint:
                MyRb2.SSprint = ka;
                break;
            case KP.Crouch:
                MyRb2.SCrouch = ka;
                break;
            case KP.Chat:
                MyChat.SChat = ka;
                break;
        }
    }
    void Apply()
    {
        foreach(KeyCode k in ToChange)
        {
            GotTheKeyYoh(k);
        }
        OnSensiChange(Sensi);
        OnInvertY(Ycheck);
        ToChange.Clear();

    }
    public void OnSensiChange(float i)
    {
        print(i);
        Sensi = i;
        if (!InMain)
        {
            MyRb2.SMS = Sensi;
        }
    }
    public void OnInvertY(bool b)
    {
        Ycheck = b;
        if(!InMain)
        {
            if (b)
                MyRb2.SYMS = -Sensi;
            else
                MyRb2.SYMS = Sensi;
        }

    }

    public void ChangeCol()
    {

        if (colfilt != null)
        {
            colfilt.enabled = true;
            colfilt.mode = (ColorBlindMode)ToChangeColor;
            colfilt.SetColor();
        }
        else
        {
            graphc.ChangeColor();
        }

    }

}

    

