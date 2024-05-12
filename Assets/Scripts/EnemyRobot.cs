using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRobot : MonoBehaviour , HurtableObjects
{
   
   float health, maxHealth = 5f;

    public AttackBotBehaviour bodyShooter;
    ParticleSystem particle;

    void Start()
    {
        health = maxHealth;
        particle = GetComponent<ParticleSystem>();
    }

    public void  TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
            StartCoroutine(Death());
    }

    public IEnumerator Death()
    {
        bodyShooter.enabled = false;
        particle.Emit(40);
        yield return new  WaitForSeconds(4);
        Destroy(this.gameObject);
    }
}

