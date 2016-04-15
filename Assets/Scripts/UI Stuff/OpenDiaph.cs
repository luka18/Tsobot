using UnityEngine;
using System.Collections;

public class OpenDiaph : MonoBehaviour {

    [SerializeField]
    MainRotate rot;

	// Use this for initialization
	void Start () {

        DontDestroyOnLoad(gameObject);
	}
	
    void OnLevelWasLoaded()
    {
        rot.JustOpen();
        StartCoroutine(ToDes());
        
    }
    IEnumerator ToDes()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
