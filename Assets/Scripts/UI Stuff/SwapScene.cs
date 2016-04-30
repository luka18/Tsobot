using UnityEngine;
using System.Collections;

public class SwapScene : MonoBehaviour
{

    [SerializeField]
    Transform bobscan;

    [SerializeField]
    Transform set;

    [SerializeField]
    MainMainMenu bob;

    GameObject can;

    public Transform Set
    {
        get { return set; }
    }

    void Start()
    {
        print("SWAPSCENE STARTED");
        StartCoroutine(mdr());
    }
   

    IEnumerator mdr()
    {
        
        yield return new WaitForEndOfFrame();
        can = GameObject.FindWithTag("UI");
        print("can"+can);
        print(can);
        if (can.transform.childCount == 4) // CHANGE NUM OF CHILD HERE 
        {
            print("found");
            can.GetComponent<Canvas>().enabled = false;
            // Destroy(set.gameObject);
            set = can.GetComponent<Settings>().transform;
            print("LOOOOOOOOOOOOOOOOOOOOOOO"+set.name);
            set.gameObject.SetActive(false);
            set.SetParent(bobscan);
            RectTransform lol = set.transform.GetComponent<RectTransform>();
            lol.localScale = new Vector3(1, 1, 1);
            lol.localPosition = new Vector3(0, 0,0);
            bob.GimmeSet(set.gameObject);
        }
    }
    public void SetActCan()
    {
        can.GetComponent<Canvas>().enabled = true;
        set.FindChild("QUIT2").gameObject.SetActive(true);
        set.FindChild("MAIN MENU").gameObject.SetActive(true);
    }

    public void SetParent()
    {
        set.SetParent(can.transform);
    }

    public void OverlayCan()
    {
        bobscan.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
    }

}