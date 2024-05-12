using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupBehavior : MonoBehaviour
{
    public GameObject popupBubble;
    GameObject targetObject;
    bool playerNear = false;
    // Start is called before the first frame update
    
    void Start()
    {
        popupBubble.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(playerNear)
        {
            Vector3 target_dir = targetObject.transform.position - transform.position;
            Vector3 new_dir = Vector3.RotateTowards(transform.forward, target_dir, 5*Time.deltaTime, 0);
            new_dir.y = 0;
            transform.rotation = Quaternion.LookRotation(new_dir);
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            targetObject = collider.gameObject;
            playerNear = true;
            popupBubble.SetActive(true);

        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            playerNear = false;
            popupBubble.SetActive(false);
        }
    }

}
