using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class GrapChanges : MonoBehaviour {
  
    [SerializeField]
    Text Tosupp;

    [SerializeField]
    private GameObject PreRes;

    [SerializeField]
    private Transform resback;

    [SerializeField]
    private RectTransform superlist;

    [SerializeField]
    private RectTransform superlist2;

    [SerializeField]
    Toggle tog;

    [SerializeField]
    InputMana InMana;

    List<GameObject> ListRes = new List<GameObject>();

    [SerializeField]
    GameObject[] QualityTab;

    [SerializeField]
    ColorBlindFilter colorscript;

    ColorBlindFilter Camerafilter0;

    private Resolution chosenres;
    private int indexres;
    Resolution[] restab;
    private bool fullscreen;

    int colortochange;

    private bool colorb;
    private bool drawing;
    float defpausy;

    int QualSet;


    public void Awake()
    {
        chosenres = Screen.currentResolution;
        fullscreen = Screen.fullScreen;
        restab = Screen.resolutions;
        QualSet = QualitySettings.GetQualityLevel();
        defpausy = superlist.sizeDelta.y;
        drawing = false;
        tog.isOn = fullscreen;
    }


    public void Delete()
    {
        foreach (GameObject k in ListRes)
        {

            Destroy(k);
        }
        ListRes.Clear();
        AddCurrent();
    }

    public void AddCurrent()
    {
        GameObject Res = (GameObject)Instantiate(PreRes, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
        Res.transform.SetParent(resback, false);
        ListRes.Add(Res);
        Res.transform.GetChild(0).GetComponent<Text>().text = " " + chosenres.width.ToString() + " x " + chosenres.height.ToString();
        Res.GetComponent<Button>().onClick.AddListener(() => OnClickRes());

    }


    public void OnGraphics()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }



    void OnClickRes()
    {
        Delete();
        if (!drawing)
        {

            DrawResolution();
        }
        else
        {
            drawing = false;
            superlist.sizeDelta = new Vector2(superlist.sizeDelta.x, defpausy);
        }

    }

    public void DrawResolution()
    {
        drawing = true;
        int adding = 0;

        for (int i = 0; i < restab.Length; i++)
        {
            print(i);
            GameObject Res = (GameObject)Instantiate(PreRes, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
            Res.transform.SetParent(resback, false);
            ListRes.Add(Res);
            Res.transform.GetChild(0).GetComponent<Text>().text = restab[i].ToString();
            int index = i;
            Res.GetComponent<Button>().onClick.AddListener(() => OnResChose(index));
            adding += 30;

        }
        if (adding > 134)
            adding = 134;
        superlist.sizeDelta = new Vector2(superlist.sizeDelta.x, superlist.sizeDelta.y + adding);
    }

    public void ApplyRes()
    {
        print(chosenres.ToString());
        Screen.SetResolution(chosenres.width, chosenres.height, fullscreen);
        QualitySettings.SetQualityLevel(QualSet, true);

       
         
        InMana.ToChangeCol = colortochange;
        

    }

    public void OnResChose(int ind)
    {
        drawing = false;
        indexres = ind;
        chosenres = restab[ind];
        superlist.sizeDelta = new Vector2(superlist.sizeDelta.x, defpausy);
        superlist.GetComponent<ScrollRect>().verticalNormalizedPosition = 1;
        Delete();
    }

    public void GetQuality(int i)
    {
        foreach (GameObject k in QualityTab)
        {
            k.GetComponent<Text>().color = new Color(225, 225, 225);
        }
        QualityTab[i - 1].GetComponent<Text>().color = new Color(240, 0, 0);
        QualSet = i;
    }

    public void OnFullScreen(bool k)
    {
        fullscreen = k;
        
    }

    public void ChangeColor()
    {
        ColorBlindFilter[] ColorFilt = FindObjectsOfType(typeof(ColorBlindFilter)) as ColorBlindFilter[];
        print("cololen"+ColorFilt.Length);
        ColorFilt[0].mode = (ColorBlindMode)colortochange;
        ColorFilt[0].SetColor();
        
        if (Camerafilter0 == null)
            Camerafilter0 = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ColorBlindFilter>();
        if(colortochange != 0)
        {
            Camerafilter0.mode = (ColorBlindMode)colortochange;
            Camerafilter0.SetColor();
            Camerafilter0.enabled = true;
        }
        else
        {
            Camerafilter0.enabled = false;
        }
       
    }
    void OnLevelWasLoaded(int level)
    {
        if (level == 0)
            ChangeColor();

    }

    public void OnClickColorB(int i)
    {
        if(colorb)
        {
            superlist2.GetComponent<ScrollRect>().vertical = false;
            superlist2.sizeDelta = new Vector2(superlist2.sizeDelta.x, 43.5f);
            superlist2.GetChild(0).GetChild(i).SetSiblingIndex(0);
            colortochange = i;
            colorb = false;
            superlist2.GetComponent<ScrollRect>().verticalNormalizedPosition = 1;
        }
        else
        {
            superlist2.GetComponent<ScrollRect>().vertical = true;
            superlist2.sizeDelta = new Vector2(superlist2.sizeDelta.x, 250);
            superlist2.GetChild(0).GetChild(0).SetSiblingIndex(colortochange);
            colorb = true;
        }
        
    }


}