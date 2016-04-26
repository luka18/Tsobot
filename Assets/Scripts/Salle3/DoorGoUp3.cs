using UnityEngine;
using System.Collections;

public class DoorGoUp3 : MonoBehaviour {
    [SerializeField]
    GameObject door;


	public void GoScript()
    {
        print("Pushed");
        StartCoroutine(GoUp()); // GO Up est appelé quand le bouton est appuyé
    }
    IEnumerator GoUp()
    {
        /*while(condition)
        {
        ---------CODE
        ca fais comme une boucle ou comme une update si tu veux.

        Il faut que la porte monte puis reste un peu de temps et
        redescende il faut aussi qu'on puisse pas appuyé tant que la coroutine est pas terimé
        au niveau du temps d'ouverture tu fait en sorte qu'on puisse rentré que quand on a appuyé
        pendant le trajet allez salut

        --------CODE
        yield return null // laisse ca en bas du while
        }*/
        yield return null; //a enlevé quand ta finis la fonc
    }

}
