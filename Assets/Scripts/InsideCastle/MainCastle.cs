using UnityEngine;
using System.Collections;

public class MainCastle : MonoBehaviour {
    [SerializeField]
    Transform Checker;
    Vector3 startpos;
    GameObject MainLevel;
    GameObject CurrentObj;
    GameObject localp;

    int currentLvl;
    Vector3 casedepart;
    public bool tester;

	// Use this for initialization
	void Start () {
        MainLevel = gameObject;
        currentLvl = 0;
        CurrentObj = gameObject;
        StartGame();

	}
    void Setting(GameObject lp)
    {
        localp = lp;
    }
    void Update()
    {
        if(tester)
        {
            NextLevel();
            tester = false;
        }
    }
	
   
    IEnumerator GoCheck()
    {
        Vector3 startpos = Checker.position;
        Vector3 endpos = new Vector3(startpos.x, startpos.y + 3, startpos.z);
        float i = 0;
        while (i < 1)
        {
            Checker.position = Vector3.Lerp(startpos, endpos, i);
            i += Time.deltaTime*1.1f;
            yield return null;
        }
        i = 0;
        yield return new WaitForSeconds(2);
        while (i<1)
        {
            Checker.position = Vector3.Lerp(endpos, startpos, i);
            i += Time.deltaTime * 1.1f;
            yield return null;
        }
        print(Checker.position + "CHECKPOS");
        NextLevel();
    }
    IEnumerator GoUp()
    {
        print(CurrentObj.transform.name);
        Vector3 startp = CurrentObj.transform.position;
        Vector3 endp = new Vector3(startp.x, startp.y + 3, startp.z);
        float i = 0;
        while(i<1)
        {
            CurrentObj.transform.position = Vector3.Lerp(startp, endp, i);
            i += Time.deltaTime/2;
            yield return null;
        }
        StartCoroutine(WaitToNext());
    }
    IEnumerator WaitToNext()
    {
        yield return new WaitForSeconds(6);
        StartCoroutine(GoCheck());
    }
    
    public void NextLevel()
    {
        localp.transform.position = new Vector3(0, 0.2f, 0);
        currentLvl += 1;
        if(currentLvl == 5)
        {
            //We won do winnie stuff
        }
        else
        {
            if (currentLvl != 1)
            {
                CurrentObj = CurrentObj.transform.GetChild(0).gameObject;
            }
            print("CURRENTLVL" + CurrentObj.name);
            StartCoroutine(GoUp());
        }
    }
    public void StartGame()
    {
        StartCoroutine(WaitToNext());
    }

    public void Reset()
    {
        localp.transform.position = casedepart;
        StopAllCoroutines();
        Checker.position = new Vector3(0, -0.1f, 0);
        while(currentLvl !=0)
        {
            CurrentObj.transform.localPosition = new Vector3(0, 0, 0);
            
            CurrentObj = CurrentObj.transform.parent.gameObject;
            currentLvl -= 1;
        }
    }
}
