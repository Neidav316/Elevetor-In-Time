using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public List<GameObject> itemsInHand;
    // public int coins;
    public SerializableDictionary<string, KeyData> keysCollected;
    public SerializableDictionary<string, DoorData> doorsUnlocked;

    public GameData()
    {
        itemsInHand = new List<GameObject>();
        keysCollected = new SerializableDictionary<string, KeyData>();
        doorsUnlocked = new SerializableDictionary<string, DoorData>();
    }

}
