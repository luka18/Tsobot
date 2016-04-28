using UnityEngine;
using System.Collections;

public class UiFinder : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        GameObject ui=  GameObject.FindWithTag("UI");
        if (ui != null)
        {
            ui.transform.GetComponentInChildren<NumGrap>().enabled = true;
        }
	
	}
	
}
