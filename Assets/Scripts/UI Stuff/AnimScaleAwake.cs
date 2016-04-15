using UnityEngine;
using System.Collections;

public class AnimScaleAwake : MonoBehaviour {


    RectTransform rec;
    float width;
    float height;
    float maxwidth;
    float maxheight;

     float rate = 3;

    void Start()
    {
        rec = transform.GetComponent<RectTransform>();
        width = rec.localScale.x;
        height = rec.localScale.y;
        maxwidth = width * 1.20f;
        maxheight = height * 1.20f;
        rec.localScale = new Vector2(0, 0);
    }

    void OnEnable()
    {

        StartCoroutine(CorAnim());
        print("awaken anim");
    }
    void OnDisable()
    {
        rec.localScale = new Vector2(0, 0);
    }

    public void Anim()
    {
        StartCoroutine(CorAnim());
    }


    IEnumerator CorAnim()
    {
        yield return new WaitForEndOfFrame();
        print("ppp");
        float i = 0;
        bool go = true;
        while (go)
        {
            if (i>1)
            {
                go = false;
            }
            float x = Mathf.Lerp(0, maxwidth, i);
            float y = Mathf.Lerp(0, maxheight, i);
            rec.localScale = new Vector2(x, y);
            i += Time.deltaTime * rate;
            yield return null;
        }
        go = true;
        i = 0;
        while (go)
        {
            if(i>1)
            {
                go = false;
            }
            float x = Mathf.Lerp(maxwidth, width, i);
            float y = Mathf.Lerp(maxheight, height, i);
            rec.localScale = new Vector2(x, y);
            i += Time.deltaTime * rate*5;
            yield return null;
        }

    }
}
