using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Script;
   
    [SerializeField] private Inventory _inventory;
    [SerializeField] private GameObject _inventoryUI;

    private UiManager _ui;
    private PlayerActionManager _playerAction;

    private void Awake()
    {
        Script = this;
    }

    private void Start()
    {
        ScriptInit();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            ShowInventory(true);
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            ShowInventory(false);
        }
    }

    public void AddItem(int resourceId)
    {
        if (_inventory.ContainsResource(resourceId))
        {
            _inventory.ChangeResourceCount(resourceId,1);
            _ui.ChangeItemField(resourceId, _inventory.GetResourceCount(resourceId));
        }
        else
        {
            _inventory.AddResource(resourceId, 1);
            _ui.AddItemIcon(resourceId, 1);
        }
    }
    public void AddTool(string toolName)
    {
        _inventory.AddTool(toolName);
        _ui.ChangeToolsIcon(_inventory.GetToolIndex(toolName));
        _playerAction.ChangeToolFromPicking(_inventory.GetToolIndex(toolName));
    }

    public void UseFromInventory(int resourceID,int count)
    {
        if (_inventory.GetResourceCount(resourceID) > 0)
        {
            _inventory.ChangeResourceCount(resourceID,-count);
            _ui.ChangeItemField(resourceID, _inventory.GetResourceCount(resourceID) );
        }
    }

    public bool HasResource(int resourceID)
    {
        if (_inventory.ContainsResource(resourceID))
        {
            return _inventory.GetResourceCount(resourceID) > 0;
        }
        return false;
    }
    public int HasCount(int resourceID)
    {
        if (_inventory.ContainsResource(resourceID))
        {
            return _inventory.GetResourceCount(resourceID);
        }
        return 0;
    }

    private void ShowInventory(bool show)
    {
        _inventoryUI.SetActive(show);
    }

    private void ScriptInit()
    {
        _ui = UiManager.Script;
        _playerAction = PlayerActionManager.Script;
    }

    public Inventory Inventory
    {
        get { return _inventory; }
    }

}

