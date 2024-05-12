using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DoorMotion : MonoBehaviour, IDataPersistance
{
    [SerializeField] private string doorId;
    [ContextMenu("Generate guide for id")]
    private void GenerateGuide()
    {
        doorId = System.Guid.NewGuid().ToString();
    }

    Animator animator;
    AudioSource soundSource;
    public AudioClip doorOpenClip;
    public AudioClip doorCloseClip;
    public AudioClip doorLockedClip;
    public bool isLocked = false;
    private  DoorData doorData = new();

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); // connection to animator in unity
        soundSource  = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("Player") && isLocked
          && DataPersistanceManager.Instance.GetGameData().keysCollected.Values.Count(keyData => !keyData.used && keyData.collected) > 0
        )
        {
            GameEventManeger.OnKeyUsed();
            // foreach( KeyData keyData in DataPersistanceManager.instance.GetGameData().keysCollected.Values)
            // {
            //     if(!keyData.used)
            //     {
            //         keyData.used = true;
            //         break;
            //     }
            // }
            isLocked = false;
            doorData.usedKeyOn=true;
        }
       

        if (isLocked)
        {
            animator.Play("Locked Door");
            soundSource.clip = doorLockedClip;
            soundSource.Play();
        }
        else
        {
            animator.SetBool("OpenState",true);
            soundSource.clip = doorOpenClip;
            soundSource.PlayDelayed(0.4f);
        }
    }
        private void OnTriggerExit(Collider collider)
    {
        if (!isLocked)
        {
            animator.SetBool("OpenState",false);
            soundSource.clip = doorCloseClip;
            soundSource.PlayDelayed(0.6f);
        }
    }

    public void LoadData(GameData gameData)
    {
        // if(isLocked)
        // {
        //     gameData.doorsUnlocked.TryGetValue(doorId, out doorData);
        //     if(doorData.usedKeyOn)
        //         isLocked = false;
        // }
    }

    public void Savedata(GameData gameData)
    {
        // if(isLocked || doorData.usedKeyOn)
        // {
        //     if(gameData.doorsUnlocked.ContainsKey(doorId))
        //         gameData.doorsUnlocked.Remove(doorId);
        //     gameData.doorsUnlocked.Add(doorId, doorData);
        // }
    }
}
