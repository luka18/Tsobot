using UnityEngine;
using System.Collections;

public class SingleSton : MonoBehaviour {


    private static SingleSton _instance = null;
    public static SingleSton Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        DontDestroyOnLoad(gameObject);

    }
}
