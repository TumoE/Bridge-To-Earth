using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseableScript : MonoBehaviour
{
    public enum Type
    {
        None = 0,
        Bust = 1,
        Tree = 2,
        Rock = 3
    }

    [SerializeField] private MonoBehaviour _itemScript;
    [SerializeField] private Outline _outline;

    [SerializeField] private Type _resourceType;
    
    private Animator _animator;

    private void Start()
    {
        ScriptInit();
    }

    public void Select()
    {
        _outline.enabled = true;
    }
    public void UnSelect()
    {
        _outline.enabled = false;
    }

    public void Use()
    {
        if (_itemScript.GetType() == typeof(MineableResourceScript))
        {
            _itemScript.TryGetComponent(out MineableResourceScript resource);
            _animator.SetTrigger(Values.AttacTag);
            resource.TakeDamage();
        }
        else if (_itemScript.GetType() == typeof(PickableResourceScript))
        {
            _itemScript.TryGetComponent(out PickableResourceScript resource);
            _animator.SetTrigger(Values.PickTag);
            resource.CanPick = true;
        }
        else if (_itemScript.GetType() == typeof(BuildScript))
        {
            _itemScript.TryGetComponent(out BuildScript machine);
            machine.Build();
        }
        else if (_itemScript.GetType() == typeof(MachineScript))
        {
            _itemScript.TryGetComponent(out MachineScript machine);
            machine.Give();
        }
        else if (_itemScript.GetType() == typeof(FillingResourceScript))
        {
            _itemScript.TryGetComponent(out FillingResourceScript fill);
            fill.Do();
        }
        else if (_itemScript.GetType() == typeof(CourierScript))
        {
            _itemScript.TryGetComponent(out CourierScript courier);
            courier.Order();
        }
        else if (_itemScript.GetType() == typeof(KeyStationScript))
        {
            _itemScript.TryGetComponent(out KeyStationScript keyStation);
            keyStation.Open();
        }
    }

    private void ScriptInit()
    {
        _animator = PlayerActionManager.Script.PlayerAnimator;
    }

    public Type ResourceType
    {
        get { return _resourceType; }
    }
}
