using UnityEngine;
using System.Collections;

public class OnTrigEnterPlayer : MonoBehaviour
{
    [SerializeField]
    Vector3 To;
    [SerializeField]
    AudioSource aud;

    void OnTriggerEnter(Collider col)
    {
        if(col.tag =="Player")
        {
            col.transform.position = To;
            if(aud!=null)
            {
                aud.Play();
            }
        }
    }

}

