using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateBehavior : MonoBehaviour
{
    
    public GameObject lockObject;

    AudioSource audioSource;
    Animator animator;
    bool locked = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if(locked && DataPersistanceManager.Instance.GetGameData().keysCollected.Count > 0)
        {
            locked = false;
            Destroy(lockObject);
            animator.SetTrigger("Unlock");
            audioSource.Play();
        }
        else if(locked)
            animator.SetTrigger("Locked");
        
    }
}
