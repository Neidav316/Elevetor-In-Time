using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplanishItemBehaviour : MonoBehaviour
{
    public GameObject ReplanishItemObject;
    AudioSource audioSource;
    Collider collider;
    void Start()
    {
        collider = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            audioSource.Play();
            this.collider.enabled = false;
            ReplanishItemObject.SetActive(false);
            GameEventManeger.OnHealthItemCollected();
        }
    }

}
