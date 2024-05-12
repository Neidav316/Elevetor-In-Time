using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBotBehaviour : MonoBehaviour
{
    public GameObject mainBody;
    public GameObject lazerBullet;
    public GameObject[] guns = new GameObject[2];
    public float lazerVelocity = 100f;
    GameObject targetObject;
    AudioSource audioSource;
    bool playerInSight = false;
    int gunIndex = 0; 
    float cooldown = 1f;
    float lastShot;

    void Start()
    {
        lastShot = Time.time;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(playerInSight)
        {
            Vector3 target_dir = targetObject.transform.position - mainBody.transform.position;
            Vector3 new_dir = Vector3.RotateTowards(transform.forward, target_dir, 2*Time.deltaTime, 0);
            new_dir.y = 0;
            transform.rotation = Quaternion.LookRotation(new_dir);
            
            if(Time.time - lastShot >= cooldown)
            {
                lastShot = Time.time;
                gunIndex = 1 - gunIndex;
                ShootLazer();
            }
        }
    }
    public void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            targetObject = collider.gameObject;
            playerInSight = true;
        }
    }
    public void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            playerInSight = false;
        }
    }

    public void ShootLazer()
    {
        GameObject ball = Instantiate(lazerBullet, guns[gunIndex].transform.position,  
                                                     guns[gunIndex].transform.rotation);
        ball.GetComponent<Rigidbody>().velocity = guns[gunIndex].transform.forward * lazerVelocity;
        audioSource.Play();
    }

}
