using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BartenderBehaviour : MonoBehaviour
{
    Animator animator;
    public GameObject[] despawnObjects;
    public GameObject[] spawnInObjects;

    public GameObject textBox;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void OnTriggerEnter(Collider collider)
    {
        if( collider.gameObject.tag == "Player")
        {
            animator.SetTrigger("Talking");
            textBox.active = true;
            StartCoroutine(triggerObjects());
        }
    }

    IEnumerator triggerObjects()
    {
        yield return new WaitForSeconds(2);
        foreach(GameObject gameObject in despawnObjects)
            {
                if(!gameObject.IsDestroyed())
                    Destroy(gameObject);
            }
        foreach(GameObject gameObject in spawnInObjects)
            {
                if(!gameObject.IsDestroyed())
                    gameObject.active = true;
            }
    }
}
