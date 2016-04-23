using UnityEngine;
using System.Collections;

public class TriggerPlan : MonoBehaviour {
    PlayerCalls myLp;

    [SerializeField]
    GameObject maincas;

    void Setting(GameObject lp)
    {
        myLp = lp.GetComponent<PlayerCalls>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            if(collision.gameObject == myLp.gameObject)
            {
                myLp.CmdGetTouchedChecker(maincas);
            }
        }
    }

}
