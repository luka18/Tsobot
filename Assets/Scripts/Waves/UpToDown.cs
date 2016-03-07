using UnityEngine;
using System.Collections;

public class UpToDown : MonoBehaviour {

    public int speed = 3;
    private float go;

    private Vector3 startpos;
    void Start()
    {
        startpos = transform.position;
    }

    void Update()
    {
        go = speed * Time.deltaTime;
        transform.Translate(0, -go, 0);
        if (transform.position.y < -10.25)
        {
            transform.position = startpos;
            gameObject.SetActive(false);
        }
    }
}