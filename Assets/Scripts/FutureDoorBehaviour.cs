using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FutureDoorBehaviour : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;

    public bool locked = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if(!locked) //&& DataPersistanceManager.Instance.GetGameData().keysCollected)
        {
            animator.SetBool("isOpen",true);
            audioSource.Play();
        }
    }   
    void OnTriggerExit(Collider collider)
    {
        if(!locked)
        {
            animator.SetBool("isOpen",false);
            audioSource.Play();
        }
    }
}
