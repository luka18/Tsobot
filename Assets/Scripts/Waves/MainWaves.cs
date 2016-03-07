using UnityEngine;
using System.Collections;

public class MainWaves : MonoBehaviour {
    private int numbofchild;
    private float timetowait = 2f;
    private float timebuffer;
    private int i = 0;
    private bool isplaying = false;

    [SerializeField] GameObject mur;

	// Use this for initialization
	void Start () {
        numbofchild = transform.childCount-1;
      
	}
    public void play2()
    {
        isplaying = true;
        mur.SetActive(true);
    }
    IEnumerator mycor3()
    {
        print("in coro");
        transform.GetChild(numbofchild).gameObject.SetActive(true);
        yield return new WaitForSeconds(8);
        mur.SetActive(false);
        transform.GetChild(numbofchild ).gameObject.SetActive(false);
        timebuffer = 0;
    }
    // Update is called once per frame
    void Update () {
        if (isplaying)
        {
            if (timebuffer > timetowait && i < numbofchild-1)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                i++;
                if (i == 4)
                {
                    timetowait = 3;
                }
                if (i == 5)
                {
                    timetowait = 1;
                    print(transform.GetChild(i - 1));
                }
                if (i == 6)
                {
                    timetowait = 3f;

                }
                if (i == 7)
                {
                    timetowait = 0.6f;
                }
                if (i == 20)
                {
                    timetowait = 1.5f;
                }

                timebuffer = 0;
            }
            timebuffer += Time.deltaTime;
            if(numbofchild-1 == i && timebuffer > 4)
            {
                print("im there");
                StartCoroutine(mycor3());
                i = 0;
                isplaying = false;
            }
        }

    }
}
