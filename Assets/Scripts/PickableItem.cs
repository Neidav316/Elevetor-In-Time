using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickableItem : MonoBehaviour, Interactable
{
    public GameObject item;
    public void interact()
    {
        DataPersistanceManager.Instance.AddItem(item);
        Destroy(this.gameObject);
    }
}
