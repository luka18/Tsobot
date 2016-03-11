using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;



public class Chat : MonoBehaviour {

    [SerializeField] 

    Text Text;

    string msg;

    bool ChatOn;

    List<string> list;

	// Use this for initialization
	void Start ()
    {

        list = new List<string>();
        for (int i = 0; i<7;i++)
        {
            list.Add("");
        }

    }

    // Update is called once per frame
    void Update ()
    {
        

        if (ChatOn)
        {
            foreach (char c in Input.inputString)
            {
                msg += c;
                print(msg);
            }            
            //Text.text = msg;
        }
        if (Input.GetButtonDown("Chat"))
        {
            ChatOn = true;
        }
        if (Input.GetButtonDown("Enter_Chat"))
        {
            ChatOn = false;
            list.Add(msg);
            msg = "";
            //Text.text = msg;
            list.RemoveAt(0);
            Text.text = list[0] + "\n" + list[1] + "\n" + list[2] + "\n" + list[3] + "\n" + list[4] + "\n" + list[5] + "\n" + list[6] + "\n";
        }




    }
    /*
    private void OnGUI()
    {
        GUILayout.Box(msg, GUILayout.Height(350));
        GUILayout.BeginHorizontal();
        msg = GUILayout.TextField(msg);
        print(msg);
        GUILayout.EndHorizontal();
    }*/
}
