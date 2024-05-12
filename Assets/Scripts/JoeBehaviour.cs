using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JoeBehaviour : MonoBehaviour
{
    public GameObject target;
    NavMeshAgent agent;
    Animator animator;
    LineRenderer line;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if(Input.GetKeyDown(KeyCode.Q)) 
        {
            // computes the path and moves NPC to the target
            agent.SetDestination(target.transform.position);

            animator.SetInteger("State", 1); // walk animation
        }
        if(distance<3)
        {
            agent.isStopped = true;
            animator.SetInteger("State", 0); // Idle animation
        }
        if (!agent.isStopped)
        {
            line.positionCount = agent.path.corners.Length;
            line.SetPositions(agent.path.corners);
        }

    }
}
