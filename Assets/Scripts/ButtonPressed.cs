using UnityEngine;
using System.Collections;

public class ButtonPressed : MonoBehaviour {

    [SerializeField] private Material colo;
    [SerializeField] private Material def;

    public float max;
    public float min;
    public float timeleft;
    public int axis;

    private bool goagain = true;


    IEnumerator mycor2()
    {
        print("inside the waiting");
        goagain = false;
        yield return new WaitForSeconds(timeleft);
        print("finish waiting");
        Unpress();
        goagain = true;
    }

    public void press()
    {
        if (goagain)

        {
            switch (axis)
            {
                case 1:
                    transform.localScale = new Vector3(min, transform.localScale.y, transform.localScale.z);
                    break;
                case 2:
                    transform.localScale = new Vector3(transform.localScale.x, min, transform.localScale.z);
                    break;
                case 3:
                    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, min);
                    break;



            }
            transform.GetComponent<MeshRenderer>().material = colo;
            print("gocour");
            StartCoroutine(mycor2());
        }
    }


public void Unpress()
    {
        switch (axis)
        {
            case 1:
                transform.localScale = new Vector3(max, transform.localScale.y, transform.localScale.z);
                break;
            case 2:
                transform.localScale = new Vector3(transform.localScale.x, max, transform.localScale.z);
                break;
            case 3:
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, max);
                break;



        }
        transform.GetComponent<MeshRenderer>().material = def;
    }


}
