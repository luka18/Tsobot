using UnityEngine;
using System.Collections;

public class LeftToRightt : MonoBehaviour
{
    
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
        transform.Translate(go, 0, 0);
        if (transform.position.x > 38)
        {
            transform.position = startpos;
            gameObject.SetActive(false);
        }
    }

}

