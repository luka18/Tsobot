using UnityEngine;
using System.Collections;

public class PlateformMouvement : MonoBehaviour
{
    Vector3 start;
    Vector3 newstart;
    Vector3 end;
    float i = 0;
    float speed2 = 0.1f;
    public float startx = 18;
    public float frein = 50;
    // Use this for initialization
    void Start()
    {
        start = transform.position;
        end = transform.position;
        end.x += startx;
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(start, end, i);
        i = i + (speed2 / frein);
        if (i >= 1)
        {
            newstart = end;
            end = start;
            start = newstart;
            i = 0;
        }
    }
}