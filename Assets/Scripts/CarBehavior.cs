using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarBehavior : MonoBehaviour
{
    public GameObject carTarget;
    NavMeshAgent agent;
    Animator animator;
        // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, carTarget.transform.position);
       // if (Input.GetKeyDown(KeyCode.W))
        //{
            // computes the path and moves NPC to the target
            agent.SetDestination(carTarget.transform.position);

       // }
        if (distance < 3)
            agent.isStopped = true;

        

    }
}
