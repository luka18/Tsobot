using UnityEngine;
using System.Collections;

public class PontLevis : MonoBehaviour {

    bool[] levelarray;

    [SerializeField]
    GameObject[] ToSpawn;

    AudioSource aud;
    [SerializeField]
    AudioClip[] clipaud;

    AudioSource aud2;

	// Use this for initialization
	void Start () {
        levelarray = new bool[] { false, false, false, false,false,false,false, false, false, false };
        aud = GetComponent<AudioSource>();
        aud = GetComponentInChildren<AudioSource>();
	}
	

    public void GetTouched(int i)
    {
        int a = Random.Range(0, 2);
        if (a == 0)
            aud2.clip = clipaud[0];
        else
            aud2.clip = clipaud[1];
        aud2.Play();
        
        levelarray[i] = true;
        if(levelarray[0] && levelarray[1])
        {
            levelarray[0] = false;
            levelarray[1] = false;
            ToSpawn[0].SetActive(true);
            ToSpawn[1].SetActive(true);
            StartCoroutine(TurnOn());
        }
        if(levelarray[2] &&levelarray[3])
        {
            levelarray[2] = false;
            levelarray[3] = false;
            ToSpawn[2].SetActive(true);
            ToSpawn[3].SetActive(true);
            ToSpawn[4].SetActive(true);
            StartCoroutine(TurnOn());
        }
        if(levelarray[4] && levelarray[5]&&levelarray[6])
        {
            levelarray[4] = false;
            levelarray[5] = false;
            levelarray[6] = false;
            ToSpawn[5].SetActive(true);
            ToSpawn[6].SetActive(true);
            ToSpawn[7].SetActive(true);
            StartCoroutine(TurnOn());
        }
        if(levelarray[7]&&levelarray[8]&&levelarray[9])
        {
            levelarray[7] = false;
            levelarray[8] = false;
            levelarray[9] = false;
            StartCoroutine(GoDownOnceForAll());
        }
         
    }

    IEnumerator TurnOn()
    {
        aud.Play();
        float i = 0;
        Vector3 start = transform.rotation.eulerAngles;
        Vector3 end = new Vector3(start.x, start.y, start.z + 10);
        while (i < 1)
        {
            i += Time.deltaTime;
            transform.eulerAngles =Vector3.Lerp(start, end, i);
            yield return null;
         }
    }
    IEnumerator GoDownOnceForAll()
    {
        float i = 0;
        Vector3 start = transform.rotation.eulerAngles;
        Vector3 end = new Vector3(start.x, start.y, 90);
        while (i<1)
        {
            i += Time.deltaTime;
            transform.eulerAngles = Vector3.Lerp(start, end, i);
            yield return null;
        }
        i = 0.1f;
        start = new Vector3(end.x, end.y, 85);
        while(i<1)
        {
            i += Time.deltaTime*3;
            transform.eulerAngles = Vector3.Lerp(end, start, i);
            yield return null;
        }
        i = 0.1f;
        while(i<1)
        {
            i += Time.deltaTime * 4;
            transform.eulerAngles = Vector3.Lerp(start, end, i);
            yield return null;
        }
    }
}
