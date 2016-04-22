using UnityEngine;
using System.Collections;

public class RockScript : MonoBehaviour {

    public bool InHand;
    [SerializeField]
    GameObject rock;
    [SerializeField]
    GameObject explo;

	// Use this for initialization
	void Start () {
        InHand = false;

	}
    
    void OnCollisionEnter()
    {
        if(InHand)
        {
            InHand = false;
            Instantiate(explo, transform.position, Quaternion.identity);
            transform.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            transform.position = new Vector3(153.29f, 3.66f, 48.33f);
            
        }
    }
    


}
