using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZoneBehaviour : MonoBehaviour
{
    public ElevetorSpawner elevetorSpawner;

    void OnTriggerEnter()
    {
        elevetorSpawner.Activate();
        gameObject.active = false;
    }
}
