using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Script;

    [SerializeField] private UiManager _ui;
    [SerializeField] private GameManager _manager;
    [SerializeField] private CourierScript _courierScript;
    [SerializeField] private TutorialDoneScript _tutorial;
    [SerializeField] private PlayerFillings _playerfillings;
    [SerializeField] private PlayerInventory _playerInventory;
    [SerializeField] private PlayerActionManager _playerAction;

    [SerializeField] private KeyStationScript[] _keyStations;

    [SerializeField] private List<ContentData> _buildableStates;

    private bool _isTutorialDisabled;

    void Awake()
    {
        Script = this;
    }
    void Start()
    {
        if (Load())
        {
            CheckTurotial();
            FillInventoryUI();
        }
        else
        {
            _courierScript.Init();
        }
    }
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/Data.Dat", FileMode.Create);
        Data savedData = new Data();

        savedData.isTutorialDisabled = _tutorial.IsTutorialDisabled;

        savedData.toolIndex = _playerAction.SelectedToolIndex;

        savedData.playerPoistion[0] = _playerInventory.transform.position.x;
        savedData.playerPoistion[1] = _playerInventory.transform.position.y;
        savedData.playerPoistion[2] = _playerInventory.transform.position.z;

        _courierScript.GetData(out savedData.isOredred,out savedData.travelTimer);
        _playerfillings.GetData(out savedData.thirst, out savedData.hunger, out savedData.temperature
                                , out savedData.energy, out savedData.life);

        savedData.courierOfferId = Values.CourierOfferId;

        SaveKey(savedData);
        SaveInventory(savedData);
        SaveBuildingSaves(savedData);

        bf.Serialize(file, savedData);
        file.Close();
    }
    public bool Load()
    {
        if (File.Exists(Application.persistentDataPath + "/Data.Dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/Data.Dat", FileMode.Open);
            Data savedData = bf.Deserialize(file) as Data;

            _isTutorialDisabled = savedData.isTutorialDisabled;

            _playerAction.SelectedToolIndex = savedData.toolIndex;

            _playerInventory.transform.position = new Vector3(
                            savedData.playerPoistion[0], 
                            savedData.playerPoistion[1], 
                            savedData.playerPoistion[2]);

            LoadKey(savedData);
            LoadInventory(savedData);
            LoadBuildingSaves(savedData);

            _courierScript.LoadData(savedData.isOredred,savedData.travelTimer);
            _playerfillings.LoadData(savedData.thirst, savedData.hunger, savedData.temperature, savedData.energy, savedData.life);

            Values.CourierOfferId = savedData.courierOfferId;

            file.Close();

            return true;
        }
        else
        {
            return false;
        }
    }

    public void NewGame()
    {
        if (File.Exists(Application.persistentDataPath + "/Data.Dat"))
        {
            File.Delete(Application.persistentDataPath + "/Data.Dat");
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

    private void SaveBuildingSaves(Data savedData)
    {
        for (int i = 0; i < _buildableStates.Count; i++)
        {
            BuildingSaveFile buildSave = new BuildingSaveFile(_buildableStates[i]);
            savedData.buildingSaves.Add(buildSave);
        }
    }
    private void LoadBuildingSaves(Data savedData)
    {
        for (int i = 0; i < savedData.buildingSaves.Count; i++)
        {
            if (_buildableStates[i] == null || savedData.buildingSaves[i] == null) return;

            _buildableStates[i].isReady = savedData.buildingSaves[i].isReady;

            for (int j = 0; j < savedData.buildingSaves[i].value.Length; j++)
            {
                _buildableStates[i].requirements[j].value = savedData.buildingSaves[i].value[j]; 
            }

        }
    }

    private void SaveInventory(Data savedData)
    {
        savedData.tools = _playerInventory.Inventory.GetTools();
        savedData.resourceKeys = _playerInventory.Inventory.ResourcesKeys;
        savedData.resources = _playerInventory.Inventory.Resources;

    }
    private void LoadInventory(Data savedData)
    {
        for (int i = 0; i < savedData.tools.Count; i++)
        {
            _playerInventory.Inventory.AddTool(savedData.tools[i]);
        }

        _playerInventory.Inventory.ResourcesKeys = savedData.resourceKeys;
        _playerInventory.Inventory.Resources = savedData.resources;

    }
    
    private void SaveKey(Data savedData)
    {
        savedData.keyOpened = new bool[_keyStations.Length];

        for (int i = 0; i < savedData.keyOpened.Length; i++)
        {
            savedData.keyOpened[i] = _keyStations[i].IsOpned;
        }
    }
    private void LoadKey(Data savedData)
    {
        for (int i = 0; i < savedData.keyOpened.Length; i++)
        {
            _keyStations[i].IsOpned = savedData.keyOpened[i];
        }
    }
    
    private void CheckTurotial()
    {
        if (_isTutorialDisabled)
        {
            Destroy(_tutorial.transform.parent.gameObject);
        }
    }
    private void FillInventoryUI()
    {
        for (int i = 0; i < _playerInventory.Inventory.Resources.Count; i++)
        {
            int key = _playerInventory.Inventory.ResourcesKeys[i];
            _ui.AddItemIcon(key, _playerInventory.Inventory.Resources[key]);
        }

        for (int i = 0; i < _playerInventory.Inventory.GetToolCount(); i++)
        {
            int index = _playerInventory.Inventory.GetToolIndex(_playerInventory.Inventory.GetToolName(i));
            _ui.ChangeToolsIcon(index);
        }

    }
}

[Serializable]
public class Data
{
    public int money;
    public int adCount;
    public int toolIndex;
    public int courierOfferId;

    public float life;
    public float thirst;
    public float hunger;
    public float energy;
    public float temperature;

    public bool isOredred;
    public float travelTimer;

    public bool[] keyOpened;

    public bool isTutorialDisabled;
    public float[] playerPoistion = new float[3];

    public List<string> tools = new List<string>();
    public List<int> resourceKeys = new List<int>();

    public Dictionary<int, int> resources = new Dictionary<int, int>();

    public List<BuildingSaveFile> buildingSaves = new List<BuildingSaveFile>();

}
