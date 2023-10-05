using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionManager : MonoBehaviour
{
    public static PlayerActionManager Script;
    public enum Tools
    {
        None = 0,
        Machete = 1,
        Axe = 2,
        Pickaxe = 3,
    }

    [SerializeField] private GameObject[] _toolsModels;

    [SerializeField] private Transform _rightHand;
    [SerializeField] private Animator _animator;

    [SerializeField] private Tools _activeTool;

    private UiManager _ui;
    private UseableScript _useable;
    private PlayerMovement _playerMovement;
    private PlayerAudioManager _playerAudio;
    private PlayerInventory _playerInventory;

    private int _selectedToolIndex = -1;

    private void Awake()
    {
        Script = this;
    }

    private void Start()
    {
        ScriptInit();
        ChangeToolFromSave();
    }

    private void Update()
    {
        MouseAndKeyBoardAction();
    }

    private void MouseAndKeyBoardAction()
    {
        if (!Values.CanPlay) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (_useable)
            {
                if ((int)_useable.ResourceType == (int)_activeTool)
                {
                    _animator.SetInteger(Values.TypeTag, (int)_activeTool);
                    _useable.Use();
                    _playerMovement.Stop();
                    ActivateToolModel((int)_activeTool - 1);
                    _playerAudio.PlaySound((int)_activeTool - 1);
                    Values.CanMove = false;
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (_useable)
            {
                if (_useable.ResourceType == 0)
                {
                    _playerAudio.PlaySound(Sounds.Sound.Put);
                    _useable.Use();
                    ActivateToolModel(-1);
                }
            }
        }

        ChangeToolWithKeyBoard();
    }

    private void ChangeToolFromSave()
    {
        if (_selectedToolIndex < 0) return;
        SelectTool(_selectedToolIndex);
    }
    public void ChangeToolFromPicking(int index)
    {
        SelectTool(index);
    }
    private void ChangeToolWithKeyBoard()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && _playerInventory.Inventory.GetToolCount() >= 1)
        {
            ChangeTool(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && _playerInventory.Inventory.GetToolCount() >= 2)
        {
            ChangeTool(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && _playerInventory.Inventory.GetToolCount() >= 3)
        {
            ChangeTool(3);
        }
    }

    private void ChangeTool(int toolIndexType)
    {
        int toolIndex = toolIndexType-1;
        string toolName = System.Enum.GetName(typeof(Tools), toolIndexType);
        if (_playerInventory.Inventory.GetToolName(toolIndex) == toolName)
        {
            SelectTool(toolIndex);
        }
    }

    private void SelectTool(int index)
    {
        _ui.SelectTool(index);
        _activeTool = (Tools)index+1;
        ActivateToolModel(index);
        _selectedToolIndex = index;
    }
    private void ActivateToolModel(int index)
    {
        for (int i = 0; i < _toolsModels.Length; i++)
        {
            _toolsModels[i].SetActive(false);
        }

        if(index >= 0) _toolsModels[index].SetActive(true);
    }
    private void ScriptInit()
    {
        _ui = UiManager.Script;
        _playerMovement = PlayerMovement.Script;
        _playerAudio = PlayerAudioManager.Script;
        _playerInventory = PlayerInventory.Script;
    }

    public int SelectedToolIndex 
    { 
        get { return _selectedToolIndex; }
        set
        {
            if( value >= 0 && value < _toolsModels.Length)
            {
                _selectedToolIndex = value;
            }
        }
    }
    public UseableScript Useable
    {
        get { return _useable; }
        set { _useable = value; }
    }
    public Animator PlayerAnimator
    {
        get { return _animator; }
    }
    public Transform RightHand
    {
        get { return _rightHand; }
    }

}
