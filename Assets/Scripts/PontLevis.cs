using UnityEngine;
using System.Collections;

public class PontLevis : MonoBehaviour {

    bool[] levelarray;

    [SerializeField]
    GameObject[] ToSpawn;

	// Use this for initialization
	void Start () {
        levelarray = new bool[] { false, false, false, false, false, false, false };
	
	}
	

    public void GetTouched(int i)
    {
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
         
    }

    IEnumerator TurnOn()
    {
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
}
