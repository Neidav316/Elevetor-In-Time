using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersistentObjectManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static PersistentObjectManager instance = null;
    private static int gold = 0;
    private static int keyCount = 0;
    public Text goldText;
    public Text keyText;
    private void Awake()  //runs before Start function
    {
        if (instance == null)
        {
            instance = this;
        }
        else  //instance is not null
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject); //in any case
      //  goldText.text = "Gold: " + gold;  //update the left gold counter
        keyText.text = "Key Count = "+keyCount;
    }
    public void SetGold(int g)
    {
        gold=g;

    }
    public void SetKeys(int k)
    {
        keyCount = k;
        keyText.text = "Key Count = "+ keyCount;
    }
    public void addKeyCount() {SetKeys(keyCount+1);}
    public void subtractKeyCount() {SetKeys(keyCount-1);}
    public int getKeyCount() {return keyCount;}
}
