using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class DataPersistanceManager : MonoBehaviour
{
    public static DataPersistanceManager Instance {get; private set;}
    [Header("File Storage Config")]
    
    [SerializeField] private string fileName;

    private GameData gameData;
    
    private List<IDataPersistance> dataPersistances;
    
    private FileDataHandler dataHandler;
    PlayerBehaviour player;

    private void Awake()  //runs before Start function
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistances = FindAllDataPersistanceObjects();
        player = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
        LoadGame();
    }

    void Update()
    {
        if(player.IsDestroyed())
            player = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
    }

    public void AddKey(string id, KeyData keydata)
    {
        gameData.keysCollected.Add(id,keydata);
    }
    public void NewGame()
    {
        this.gameData = new GameData();
    }
    public void LoadGame()
    {
        // this.gameData = dataHandler.Load();

        if(Instance.gameData == null)
        {
            Debug.LogError("No Game Data Found, Initialiaze to default settings.");
            NewGame();
        }
        else{
            foreach (IDataPersistance dataPersistanceObj in dataPersistances)
            {
                dataPersistanceObj.LoadData(gameData);
            }
        }
    }
    public void SaveGame()
    {
        foreach (IDataPersistance dataPersistanceObj in dataPersistances)
        {
            dataPersistanceObj.Savedata(gameData);
        }
        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistance> FindAllDataPersistanceObjects()
    {
        IEnumerable<IDataPersistance> dataPersistancesObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistance>();
        return new List<IDataPersistance>(dataPersistancesObjects);
    }
    public GameData GetGameData()
    {
        return gameData;
    }
    public void AddItem(GameObject item)
    {
        gameData.itemsInHand.Add(item);
        player.SetItemInHand(item);
    }

}
