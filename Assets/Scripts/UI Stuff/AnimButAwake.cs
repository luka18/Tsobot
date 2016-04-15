using UnityEngine;
using System.Collections;

public class AnimButAwake : MonoBehaviour {


    RectTransform rec;
    float width;
    float height;
    float maxwidth;
    float maxheight;

     float rate = 2;

    void Start()
    {
        rec = transform.GetComponent<RectTransform>();
        width = rec.sizeDelta.x;
        height = rec.sizeDelta.y;
        maxwidth = width * 1.20f;
        maxheight = height * 1.20f;
        rec.sizeDelta = new Vector2(0, 0);
    }

    void OnEnable()
    {
        StartCoroutine(CorAnim());
        print("awaken but");
    }
    void OnDisable()
    {
        rec.sizeDelta = new Vector2(0, 0);
    }

    public void Anim()
    {
        StartCoroutine(CorAnim());
    }


    IEnumerator CorAnim()
    {
        yield return new WaitForEndOfFrame();
        bool go = true;
        float i = 0;
        while(go)
        {
            if (i > 1)
                go = false;
            float x = Mathf.Lerp(0, maxwidth, i);
            float y = Mathf.Lerp(0, maxheight, i);
            rec.sizeDelta = new Vector2(x, y);
            i += Time.deltaTime*rate;
            yield return null;
        }
        go = true;
        i = 0;
        while(go)
        {
            if (i > 1)
                go = false;
            float x = Mathf.Lerp(maxwidth,width, i);
            float y = Mathf.Lerp(maxheight, height, i);
            rec.sizeDelta = new Vector2(x, y);
            i += Time.deltaTime * rate*5;
            yield return null;
        }
        
    }

}
