using UnityEngine;
using System.Collections;

public class MainCastle : MonoBehaviour {
    [SerializeField]
    Transform Checker;
    Vector3 startpos;
    GameObject MainLevel;
    GameObject CurrentObj;
    GameObject localp;

    [SerializeField]
    GameObject boubou;

    [SerializeField]
    Transform BouRed;
    [SerializeField]
    Transform BouBlu;
    [SerializeField]
    Transform BouGreen;
    [SerializeField]
    Transform BouPurple;
    [SerializeField]
    Transform CubeRed;
    [SerializeField]
    Transform CubeBlu;
    [SerializeField]
    Transform CubeGreen;
    [SerializeField]
    Transform CubePurple;

    int currentLvl;
    Vector3 casedepart;
    public bool tester;

    Vector3 BottomLeft;
    Vector3 BottomRight;
    Vector3 TopLeft;
    Vector3 TopRight;
    Vector3 Resetpos;

	// Use this for initialization
	void Start () {
        MainLevel = gameObject;
        currentLvl = 0;
        CurrentObj = gameObject;
        StartGame();
        BottomLeft = new Vector3(-14, 2, -14);
        BottomRight = new Vector3(14, 2, -14);
        TopLeft = new Vector3(-14, 2, 14);
        TopRight = new Vector3(14, 2, 14);
        Resetpos = new Vector3(0, -5, 0);

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
        localp.transform.position = new Vector3(0, 0.2f+boubou.transform.position.y, 0);
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
            RedoLevel();
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
            print(CurrentObj + "currentobj");
            CurrentObj = CurrentObj.transform.parent.gameObject;
            currentLvl -= 1;
        }
    }

    void RedoLevel()
    {

        switch(currentLvl)
        {
            case 1: // REDCUBE THIS TIME
                BouRed.localPosition = Resetpos;
                CubeGreen.localPosition = new Vector3(-10, -5, -10);
                CubeRed.localPosition = new Vector3(BottomLeft.x, 3.2f, BottomLeft.z);
                BouBlu.localPosition = BottomRight;
                BouBlu.localEulerAngles = new Vector3(0, 180, 0);
                BouGreen.localPosition = TopLeft;
                BouGreen.localEulerAngles = new Vector3(0, 90, 0);
                BouPurple.localPosition = TopRight;
                BouPurple.localEulerAngles = new Vector3(0, 135, 0);
                break;

            case 2:
                CubeRed.position = new Vector3(-10, -5, 10);
                BouPurple.localPosition = Resetpos;
                BouGreen.localPosition = TopRight;
                BouGreen.localEulerAngles = new Vector3(0, 180, 0);
                BouBlu.localPosition = BottomLeft;
                BouBlu.localEulerAngles = new Vector3(0, -90, 0);
                BouRed.localPosition = BottomRight;
                BouRed.localPosition = new Vector3(0, 235, 0);
                CubeBlu.localPosition = new Vector3(TopLeft.x, 3.2f, TopLeft.z);
                break;
        }
    }
}
