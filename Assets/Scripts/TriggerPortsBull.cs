using UnityEngine;
using System.Collections;

public class TriggerPortsBull : MonoBehaviour {
    [SerializeField]
    BallPorts bp;

    void OnTriggerEnter()
    {
        bp.GoNext();
    }
}
