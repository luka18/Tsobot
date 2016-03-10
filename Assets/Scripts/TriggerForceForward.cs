using UnityEngine;
using System.Collections;

public class TriggerForceForward : MonoBehaviour {


    public int force;
    public bool Sound;
    private AudioSource aud;

    void Update()
    {
        Debug.DrawRay(transform.position, transform.up, Color.red, 1);
    }

    void Start()
    {
        aud = transform.GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.transform.tag == "Player")
        {
            col.GetComponent<RB2>().SetControl(false);
            col.GetComponent<Rigidbody>().AddForce(transform.up *force, ForceMode.VelocityChange);
            if(Sound)
            {
                aud.Play();
            }
        }
    }



}
