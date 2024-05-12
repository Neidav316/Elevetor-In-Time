using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MovingEnemy : MonoBehaviour, HurtableObjects
{
    NavMeshAgent agent;
    public GameObject humanoidObject;
    Animator animator;
    ParticleSystem particle;
    GameObject target;
    float lastAttack;
    bool canAttack = true;

    public float health = 6f;
    public float attackDelay = 1.2f;
    public Transform attackPosition;
    public LayerMask playerLayer;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = humanoidObject.GetComponentInChildren<Animator>();
        particle = GetComponent<ParticleSystem>();
        lastAttack = Time.time;
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(canAttack && target != null)
        {
            agent.SetDestination(target.transform.position);
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            if(distanceToTarget >= 5.6f && distanceToTarget <= 25 )
            {
                agent.isStopped = false;
                animator.SetBool("IsWalking",true);
            }
            else if(distanceToTarget > 25)
            {
                agent.isStopped = true;
                animator.SetBool("IsWalking",false);
            }
            else if(distanceToTarget < 5.6f)
            {
                agent.isStopped = true;
                animator.SetBool("IsWalking", false);
                if(Time.time - lastAttack >= attackDelay)
                {
                    lastAttack = Time.time;
                    animator.SetTrigger("Attack");
                    StartCoroutine(Attack());
                }
            }
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.2f);
        Collider[] hitPlayer = Physics.OverlapSphere(attackPosition.transform.position,playerLayer);
        foreach(Collider player in hitPlayer)
        {
            
            if(player.gameObject.TryGetComponent<PlayerBehaviour>(out PlayerBehaviour playerComponent))
                playerComponent.TakeDamage(1);
        }
    }

    public void TakeDamage(float amount)
    {
        if(health <= 0)
            return;
        health -= amount;
        particle.Emit(20);
        if(health <= 0)
            StartCoroutine(Death());
    }

    public IEnumerator Death()
    {
        canAttack = false;
        agent.isStopped = true;
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
