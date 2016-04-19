using UnityEngine;
using System.Collections;

public class ChainScript : MonoBehaviour {
    PlayerCalls lp;

    [SerializeField]
    PontLevis lv;

    [SerializeField]
    int MyNum;

    public void Setting(GameObject localp)
    {
        lp = localp.GetComponent<PlayerCalls>();
    }

    public void Kill()
    {
        Destroy(transform.gameObject);
        lv.GetTouched(MyNum);
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.transform.tag =="Carryable")
        {
            print("Detected");
            lp.CmdKillChain(gameObject);
        }
    }



}
