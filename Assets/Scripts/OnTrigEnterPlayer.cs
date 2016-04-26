using UnityEngine;
using System.Collections;

public class OnTrigEnterPlayer : MonoBehaviour
{
    [SerializeField]
    Respawner respawnpoint;
    [SerializeField]
    AudioSource aud;

    void OnTriggerEnter(Collider col)
    {
        if(col.tag =="Player")
        {
            col.transform.position = respawnpoint.Spawner;
            print(respawnpoint.Spawner);
            if(aud!=null)
            {
                aud.Play();
            }
        }
    }

}

