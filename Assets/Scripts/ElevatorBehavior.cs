using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElevatorBehavior : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;
    public GameObject ui;
    public bool playerIsInside = false;
    public string sceneTarget;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

}
