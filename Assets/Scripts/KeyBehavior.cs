using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;


public class KeyBehavior : MonoBehaviour, IDataPersistance
{

    [SerializeField] private string keyId;
    [ContextMenu("Generate guide for id")]
    private void GenerateGuide()
    {
        keyId = System.Guid.NewGuid().ToString();
    }
    public GameObject keyObject;
    private AudioSource audioSource;
    private KeyData keyData = new();

    Collider collider;
    void Start()
    {
        collider = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
        return;
        // if(!keyData.collected)
        //     {
                audioSource.Play();
                CollectKey();
        // }
    }
    
    private void CollectKey()
    {
        // keyData.collected = true;
        keyObject.SetActive(false);
        this.collider.enabled = false;
        DataPersistanceManager.Instance.AddKey(keyId,keyData);
        GameEventManeger.OnKeyCollected();
        
    }

    public void LoadData(GameData gameData)
    {
        // gameData.keysCollected.TryGetValue(keyId, out keyData);
        // if (keyData.collected)
        //     keyObject.SetActive(false);
    }

    public void Savedata(GameData gameData)
    {
        // if(gameData.keysCollected.ContainsKey(keyId))
        //     gameData.keysCollected.Remove(keyId);
        // gameData.keysCollected.Add(keyId, keyData);
    }
}
