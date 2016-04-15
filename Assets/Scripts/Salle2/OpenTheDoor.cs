using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class OpenTheDoor : NetworkBehaviour {

    public int rate;
    Vector3 Lstart;
    Vector3 Rstart;
    private bool opened = false;
    void Start()
    {
        Lstart = transform.GetChild(0).localPosition;
        Rstart = transform.GetChild(1).localPosition;
        print("s");
    }
    
    public void Open()
    {
        if (!opened)
        {
            StartCoroutine(mycor2());
        }
    }


    IEnumerator mycor2()
    {
        float i = 0;
        GameObject left = transform.GetChild(0).gameObject;
        GameObject right = transform.GetChild(1).gameObject;
        opened = true;
        while(i<1)
        {
            left.transform.localPosition = Vector3.Lerp(Lstart, new Vector3(4.28f,0.83f,0.9f), i);
            right.transform.localPosition = Vector3.Lerp(Rstart, new Vector3(-4.28f, 0.83f, 0.9f), i);
            i += Time.deltaTime;
            yield return null;
        }

    }
    public IEnumerator Close()
    {
        float i = 0;
        GameObject left = transform.GetChild(0).gameObject;
        GameObject right = transform.GetChild(1).gameObject;
        Vector3 LStart = left.transform.position;
        Vector3 RStart = right.transform.position;
        opened = true;
        while (i < 1)
        {
            left.transform.localPosition = Vector3.Lerp(LStart, Lstart, i);
            right.transform.localPosition = Vector3.Lerp(RStart, Rstart, i);
            i += Time.deltaTime;
            yield return null;
        }
    }



}
