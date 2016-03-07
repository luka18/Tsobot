using UnityEngine;
using System.Collections;

public class ButtonPressednorm : MonoBehaviour
{

    public int timeleft;
    private bool goagain = true;

    public void press()
    {
        if(goagain)
            StartCoroutine(mycor());
     
    }
        
    IEnumerator mycor()
    {
        goagain = false;
        transform.localScale = new Vector3(0.4f, 0.01f, 0.4f);
        print("pressedwow");
        yield return new WaitForSeconds(timeleft);
        transform.localScale = new Vector3(0.4f, 0.07f, 0.4f);
        goagain = true;
    }
}
