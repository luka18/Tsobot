using UnityEngine;
using System.Collections;

public class LocalP : MonoBehaviour {

    GameObject localp;

    [SerializeField]
    Chat ch;

	public GameObject Getlocal()
    {
        return localp;
    }
    public void Setlocal(GameObject obj)
    {
        localp = obj;
        ch.Setting();
    }
}
