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
        print("I COLLIDED");
        if (collision.transform.tag == "Player")
        {
            print("WITH A PLAYER");
            if(collision.gameObject == myLp.gameObject)
            {
                myLp.CmdGetTouchedChecker(maincas);
            }
        }
    }

}
