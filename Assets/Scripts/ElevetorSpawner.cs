using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevetorSpawner : MonoBehaviour
{
    ParticleSystem particles;
    Animator animator;
    public GameObject elevetor;
    public bool toSpawnIn;
    bool activated = false;
    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        animator = GetComponent<Animator>();
        
        elevetor.active = !toSpawnIn;
    }

    public void Activate()
    {
        if(activated)
            return;
        if(toSpawnIn)
        {
            elevetor.active = toSpawnIn;
            animator.SetTrigger("Spawn In");
            particles.Play();
        }
        else
        {

            StartCoroutine(Despawn());
        }
    }

    IEnumerator Despawn()
    {
        animator.SetTrigger("Spawn Out");
        particles.Play();
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }


    
}
