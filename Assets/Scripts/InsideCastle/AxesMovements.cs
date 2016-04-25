using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class AxesMovements : MonoBehaviour {

    float i;
    Vector3 startp;
    Vector3 endp;
    [SerializeField]
    Vector3 Forcevector;
    PlayerCalls pc;
    void Setting(GameObject lp )
    {
        pc = lp.GetComponent<PlayerCalls>();
        pc.CmdWhatI(gameObject);
        print(i);
    }

	// Use this for initialization
	void Start () {
        print("transpo" + transform.eulerAngles);
        startp = new Vector3(40, 0, 0);
        endp = new Vector3(-40,0,0);
        //i = Mathf.InverseLerp(40, 320, transform.eulerAngles.x);
        //GetComponent<NetworkTransform>().enabled = false;
        
       
	}
    public float GetI()
    {
        return i;
    }
    public void SetI(float k)
    {
        i = k;
        print("i've been modified"+i);
    }
	// Update is called once per frame
	void Update () {

        i += Time.deltaTime;
        transform.eulerAngles = Vector3.Lerp(startp, endp, Mathf.SmoothStep(0.0f, 1.0f, i));
        if(i >=1)
        {
            Vector3 save = startp;
            startp = endp;
            endp = save;
            i = 0;
        }

    }

 
}
