using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

    private bool close = false;
    float i = 0;
     float rate = 1.5f;
    bool open = false;

    float z1;
    Vector3 startangle;

    bool forceclose = false;
    //295 total open
	// Update is called once per frame
    void Start()
    {
        startangle = transform.rotation.eulerAngles;
    }


    public void Transition()
    {
        if (!close)
        {
            z1 = transform.localEulerAngles.z;
        }
        open = false;
        close = true;
        
    }

    public void JustClose()
    {
        print("JustCalled");
        forceclose = true;
        Transition();
    }
    public void JustOpen()
    {
        open = true;
        close = false;
    }

	void Update () {
        if(open)
        {
            i += Time.deltaTime*rate;
            transform.localEulerAngles = new Vector3(0, 0, Mathf.LerpAngle(0, 280, i));

        }
        if(close)
        {
            i += Time.deltaTime * rate;
            transform.localEulerAngles = new Vector3(0, 0, Mathf.LerpAngle(z1, 359, i));
        }
        if(i>1)
        {
            if (close)
            {
                close = false;
                if (!forceclose)
                {
                    open = true;
                }
                i = 0;
            }
            else
            {
                open = false;
                i = 0;
            }
            
        }
    }




}
