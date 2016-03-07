using UnityEngine;
using System.Collections;

public class ButtonsToDoor : MonoBehaviour {
    [SerializeField] private Material Red;
    [SerializeField] private Material Blu;
    [SerializeField] private Material Green;
    [SerializeField] private Material Purple;

    [SerializeField]
    OpenTheDoor TheDoor;


    private bool[] ArrayColors = new bool[] { false, false, false, false };


    private int NumberOfColor = 0;

    public void AddColor(int num) // 0 Purple 1 Blue 2 Green 3 Red
    {
        if (!ArrayColors[num])
        {
            
            ArrayColors[num] = true;

            GameObject color = transform.GetChild(num).gameObject;
            Material MatApply = WichNumber(num);
            MeshRenderer[] allmesh = color.GetComponentsInChildren<MeshRenderer>();
            foreach(MeshRenderer k in allmesh)
            {
                k.material = MatApply;
            }

            color.transform.GetChild(color.transform.childCount - 1).gameObject.SetActive(true);
            if(ArrayColors[0]&&ArrayColors[1]&&ArrayColors[2]&&ArrayColors[3])
                TheDoor.Open();
        }
       

    }

    Material WichNumber(int m)
    {
        switch(m)
        {
            case 0:
                return Purple;
            case 1:
                return Blu;
            case 2:
                return Green;
            case 3:
                return Red;
        }
        return null;
    }



}
