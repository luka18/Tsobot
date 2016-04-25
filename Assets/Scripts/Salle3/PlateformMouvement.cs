using UnityEngine;
using System.Collections;

public class PlateformMouvement : MonoBehaviour
{
    public Vector3 start;
    public Vector3 newstart;
    public Vector3 end;
    public float i = 0;
    public float speed = 0.1f;
    // Use this for initialization
    void Start()
    {
        start = transform.position;
        end = transform.position;
        end.x += 18;
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(start, end, i);
        i = i + (speed / 200);
        if (i >= 1)
        {
            newstart = end;
            end = start;
            start = newstart;
            i = 0;
        }
    }
}