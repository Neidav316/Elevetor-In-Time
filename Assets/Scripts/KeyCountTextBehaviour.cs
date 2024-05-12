using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class KeyCountTextBehaviour : MonoBehaviour, IDataPersistance
{
    private  int KeyCount = 0;
    private TextMeshProUGUI keyCountText;
    private void Awake()
    {
        keyCountText = this.GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {
        UpdateKeyCount(0);
    }
    void OnEnable()
    {
        GameEventManeger.KeyCollected += OnKeyCollected;
        GameEventManeger.KeyUsed += SubKeyCount;
    }
    void OnDisable()
    {
        GameEventManeger.KeyCollected -= OnKeyCollected;
        GameEventManeger.KeyUsed -= SubKeyCount;
    }

    public void LoadData(GameData gameData)
    {
        // int tempCount = 0;
        // foreach(KeyValuePair<string,KeyData> pair in gameData.keysCollected)
        // {
        //     if (pair.Value.collected && !pair.Value.used  )
        //         tempCount++;
        // }
        // UpdateKeyCount(tempCount);
    }

    public void Savedata(GameData gameData)
    {
        // no need to save from here
    }

    private void UpdateKeyCount(int value)
    {
        KeyCount = value;
        keyCountText.SetText("Key Count: "+KeyCount);
    }
    public void OnKeyCollected()
    {
        UpdateKeyCount(KeyCount+1);
    }
    public void SubKeyCount()
    {
        UpdateKeyCount(KeyCount-1);
    }
    public int GetKeyCount()
    {
        return KeyCount;
    }

}
