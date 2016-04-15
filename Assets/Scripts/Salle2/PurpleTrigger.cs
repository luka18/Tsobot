using UnityEngine;
using System.Collections;

public class PurpleTrigger : MonoBehaviour {

	void Start () {
	
	}
	

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag != "EditorOnly")
        {
            col.transform.position = new Vector3(-17, 0.25f, -18);
        }
    }
}
