using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey,TValue>,ISerializationCallbackReceiver where TValue : class
{
    [SerializeField] private List<TKey> keys = new List<TKey>();
    [SerializeField] private List<TValue> values = new List<TValue>();



    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();
        foreach (KeyValuePair<TKey,TValue> pair in this)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        this.Clear();

        if (keys.Count != values.Count)
        {
            Debug.LogError("Tried to Deserialize a SerializableDictionary, but the amount of keys ("
            +keys.Count+") does not match the number of values ("
            +values.Count+") which indicates that something went wrong.");
        }
        for (int i = 0; i< keys.Count; i++)
        {
            this.Add(keys[i],values[i]);
        }
    }

    public TValue getValueAt(int index){return this.getValueAt(index);}
    public List<TValue> GetValues(){return values;}

}
