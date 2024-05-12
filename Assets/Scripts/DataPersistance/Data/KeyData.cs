
[System.Serializable]
public class KeyData
{
    public bool collected;
    public bool used; 

    public KeyData()
    {
        collected = false;
        used = false;
    }
    public KeyData(bool collected, bool used)
    {
        this.collected = collected;
        this.used = used;
    }
    

}
