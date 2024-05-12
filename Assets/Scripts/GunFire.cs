using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour
{

    float torque = 0.2f;

    public GameObject eye; // main camara
    public GameObject target; // an object that emits light where it hit 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            
            RaycastHit hit;

            if(Physics.Raycast(eye.transform.position, eye.transform.forward, out hit))
            {
                target.transform.position = hit.point;
            }
        }
    }
}
