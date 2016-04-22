using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MasterSound : MonoBehaviour {
    [SerializeField]
    AudioClip[] SourceS;
    [SerializeField]
    GameObject op;

    List<int> MusicTry;
    AudioSource aud;
    int cur = 3;
    private RayCastDetect LpCall;
    [SerializeField]
    PullSound[] tabdesongros = new PullSound[] { };

    int[] lvl0 = new int[] { 1, 2, 1};
    int[] lvl1 = new int[] { 2, 3, 1, 2 };
    int[] lvl2 = new int[] { 1, 2 , 2, 1, 3 };
    //int[] lvl3 = new int[] { 1, 2 , 0, 3, 2, 1 };
    int[] lvl3 = new int[] { 1 };
    private bool playing;
    // Use this for initialization
    void Start() {
        aud = transform.GetComponent<AudioSource>();
        MusicTry = new List<int>();
        playing = false;
    }
    public void Setting(GameObject obj)
    {
        print("setted");
        LpCall = obj.GetComponent<RayCastDetect>();
    }

    public void GetSound(int son)
    {
        MusicTry.Add(son);
        if (TestCorrect())
        {
            cur += 1;
            print("we did it morty"+cur);
            MusicTry.Clear();
            if(cur == 4)
            {
                LpCall.CmdOpenDoor(op);
            }
        }
    }
    public void PlayTheSound()
    {
        if (!playing)
        {
            playing = true;
            StartCoroutine(PlayS(currentSong()));
        }
    }

    IEnumerator PlayS(int[] tab)
    {
        foreach(PullSound k in tabdesongros)
        {
            k.canI = false;
        }
        foreach (int k in tab)
        {
            aud.clip = SourceS[k];
            aud.Play();
            yield return new WaitForSeconds(1);
        }
        foreach (PullSound k in tabdesongros)
        {
            k.canI = true;
        }
        playing = false;
    }

    int[] currentSong()
    {
        switch(cur)
        {
            case 0:
                return lvl0;
            case 1:
                return lvl1;
            case 2:
                return lvl2;
            default:
                return lvl3;
        }
             
    }

    bool TestCorrect()
    {
        int[] song = currentSong();
        int j = 0;
        foreach (int k in MusicTry)
        {
            if (k == song[j])
            {
                j += 1;
                if (j == song.Length)
                    return true;
            }
            else
            {
                j = 0;
            }
        }
        return false;

        
    }
}
