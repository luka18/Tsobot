using UnityEngine;
using System.Collections;

public class BackToFront : MonoBehaviour {
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
        transform.Translate(0, 0, go);
        if (transform.position.z > 25)
        {
            transform.position = startpos;
            gameObject.SetActive(false);
        }
    }
}
