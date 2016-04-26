using UnityEngine;
using System.Collections;

public class DoorGoUp3 : MonoBehaviour {
    [SerializeField]
    GameObject door;
    Vector3 startpos;   
    Vector3 endpos;      
    float speed = 0.5F;

    public void GoScript()
    {
        print("Pushed");
        StartCoroutine(GoUp()); // GO Up est appelé quand le bouton est appuyé
    }
    IEnumerator GoUp()
    {
         float i = 0;                
        while (i <= 1)
        {
            door.transform.position = Vector3.Lerp(startpos, endpos, i);           
            i +=Time.deltaTime * speed;            
            yield return null;            
        }        
        yield return new WaitForSeconds(3);
        print(i);
        i = 0;               
        while (i <= 1)
        {            
            door.transform.position = Vector3.Lerp(endpos, startpos, i);
            i +=Time.deltaTime*speed;            
            yield return null;              
                 
        }
    }
    void Start()
    {
        startpos = door.transform.position;
        endpos = new Vector3(startpos.x, startpos.y + 8, startpos.z);

    }
}

      
