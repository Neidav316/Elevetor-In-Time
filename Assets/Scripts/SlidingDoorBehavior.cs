using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlidingDoorBehavior : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource =GetComponent<AudioSource>();
        animator= GetComponent<Animator>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        audioSource.Play();
        animator.SetBool("OpenState", true);
    }

    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("OpenState",false);
    }

}
