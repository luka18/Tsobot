using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ButtonPressedOnce : MonoBehaviour
{
    [SerializeField]
    Material OnColor;

    public int axis;
    public float min;
    public float max;

    public void press()
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
        transform.GetComponent<MeshRenderer>().material = OnColor;
        


    }
}
