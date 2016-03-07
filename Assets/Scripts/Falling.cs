using UnityEngine;
using System.Collections;

public class Falling : MonoBehaviour
{
    public float speed = 0.5f;
    public float distomove = 4.5f;
    private Vector3 startposy;
    private Vector3 endposy;
    private bool islerping = false;
    private float i = 0;
    private bool comeback;


    // Use this for initialization
    void Start()
    {
        startposy = transform.position;
    }
    void StartLerping()
    {
        islerping = true;
        endposy = startposy - transform.up * distomove;

    }
    void FixedUpdate()
    {
        if(islerping)
        {
            i += Time.deltaTime*speed;
            transform.position = Vector3.Lerp(startposy, endposy, i);
            
            if(i>=1.0f)
            {
                if (comeback)
                {
                    islerping = false;
                }
                else
                {
                    Vector3 save = startposy;
                    startposy = endposy;
                    endposy = save;
                    i = 0;
                    comeback = true;
                    print("wow");
                }

            }
        }
    }


    void OnCollisionEnter()
    {
        
       StartLerping();
        
    }
    
   
    
    

}
