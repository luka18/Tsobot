using UnityEngine;
using System.Collections;
using DigitalRuby.ThunderAndLightning;

public class OnStartStuffPlayer : MonoBehaviour {


    LightningBoltPathScript lbp;


    // Use this for initialization
    void Start () {

        GameObject Lmanag = transform.GetChild(3).gameObject;
        lbp = Lmanag.GetComponent<LightningBoltPathScript>();
        lbp.LightningPath.List[0] = transform.gameObject;
	}
	
}
