using UnityEngine;
using System.Collections;

public class PullSound : MonoBehaviour {

    AudioSource aud;
    Vector3 startp;
    [SerializeField]
    MasterSound MsSound;

    [SerializeField]
    private int MySoundNumber;

    public bool canI; 
    
	// Use this for initialization
	void Start () {
        aud = transform.GetComponent<AudioSource>();
        startp = transform.position;
	}
	public void PlaysSound()
    {
        if (canI)
        {
            print("played adio");
            aud.Play();
            StartCoroutine(DownUp());
            MsSound.GetSound(MySoundNumber);
        }
    }
   
    IEnumerator DownUp()
    {
        Vector3 endp = startp - new Vector3(0, 0.4f, 0);
        float i = 0;
        while (i < 1)
        {
            transform.position = Vector3.Lerp(startp, endp, i);
            i += Time.deltaTime*2;
            yield return null;
        }
        float j = 0;
        while(j <1)
        {
  
            transform.position= Vector3.Lerp(endp, startp,j);
            j += Time.deltaTime*5;
            yield return null;
        }
    }


}
