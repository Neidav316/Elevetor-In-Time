using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractSystem : MonoBehaviour
{
    Transform interactPoint;

    // Start is called before the first frame update
    void Start()
    {
        interactPoint = GameObject.Find("InteractPoint").transform;    
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Linecast(gameObject.transform.position, interactPoint.transform.position, out RaycastHit hit))
        {
            if (hit.collider.gameObject.TryGetComponent<Interactable>(out Interactable component))
            {
                GameEventManeger.OnInteract();
                if (Input.GetKeyDown(KeyCode.E))
                    component.interact();
            }

        }
        else
            GameEventManeger.OnNotInteract();
    }
}
