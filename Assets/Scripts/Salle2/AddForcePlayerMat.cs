using UnityEngine;
using System.Collections;

public class AddForcePlayerMat : MonoBehaviour {

    public int force;
    public bool Sound;
    private AudioSource aud;


    void Start()
    {
        aud = transform.GetComponent<AudioSource>();
    }
    
    void OnCollisionEnter(Collision col)
    {
        Debug.DrawRay(transform.position, transform.up,Color.red,4);
        if (col.transform.tag == "Player")
        {
            col.transform.GetComponent<RB2>().SetControl(false);
            col.rigidbody.velocity = new Vector3(0, 0, 0);
            col.rigidbody.AddForce((transform.up) * force, ForceMode.VelocityChange);
            if(Sound)
            {
                aud.Play();
            }
        }
    }
}
