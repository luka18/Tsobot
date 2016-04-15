using UnityEngine;
using System.Collections;

public class MainRotate : MonoBehaviour {

    void Start()
    {
        gameObject.SetActive(false);
        gameObject.SetActive(true);
        
    }

	public void Transtition()
    {
        foreach(Rotate k in transform.GetComponentsInChildren<Rotate>())
        {
            k.Transition();
        }
    }
    public void JustClose()
    {
        foreach (Rotate k in transform.GetComponentsInChildren<Rotate>())
        {
            k.JustClose();
        }
    }
    public void JustOpen()
    {
        foreach (Rotate k in transform.GetComponentsInChildren<Rotate>())
        {
            k.JustOpen();
        }
    }


}
