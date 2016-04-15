using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Openthedoor2 : NetworkBehaviour {
    public int rate;
    Vector3 Lstart;
    Vector3 Rstart;
    [SerializeField]
    Material red;
    private bool opened = false;
    void Start()
    {
        Lstart = transform.GetChild(0).localPosition;
        Rstart = transform.GetChild(1).localPosition;
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
        for(int k = 2; k< transform.childCount-1;k++)
        {
            transform.GetChild(k).GetComponent<MeshRenderer>().material = red;
        }
        transform.GetChild(transform.childCount - 1).gameObject.SetActive(true);
        opened = true;
        while (i < 1)
        {
            left.transform.localPosition = Vector3.Lerp(Lstart, new Vector3(4.28f, 0.83f, 0.9f), i);
            right.transform.localPosition = Vector3.Lerp(Rstart, new Vector3(-4.28f, 0.83f, 0.9f), i);
            i += Time.deltaTime;
            yield return null;
        }

    }




}