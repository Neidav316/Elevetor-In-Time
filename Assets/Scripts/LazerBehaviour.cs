using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LazerBehaviour : MonoBehaviour
{
    
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<HurtableObjects>(out HurtableObjects hurtableComponent))
        {
            hurtableComponent.TakeDamage(1);
        }
            
        Destroy(gameObject);
    }

}
