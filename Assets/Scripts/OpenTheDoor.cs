using UnityEngine;
using System.Collections;

public class OpenTheDoor : MonoBehaviour {

     public int rate;
    Vector3 Lstart;
    Vector3 Rstart;
    void Start()
    {
        Lstart = transform.GetChild(0).localPosition;
        Rstart = transform.GetChild(1).localPosition;
    }

    public void Open()
    {
        StartCoroutine(mycor2());
    }

    IEnumerator mycor2()
    {
        float i = 0;
        float rate2 = 1 / rate;
        GameObject left = transform.GetChild(0).gameObject;
        GameObject right = transform.GetChild(1).gameObject;
        while(i<1)
        {
            left.transform.localPosition = Vector3.Lerp(Lstart, new Vector3(4.28f,0.83f,0.9f), i);
            right.transform.localPosition = Vector3.Lerp(Rstart, new Vector3(-4.28f, 0.83f, 0.9f), i);
            i += Time.deltaTime;
            yield return null;
        }

    }



}
