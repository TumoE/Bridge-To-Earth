using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVision : MonoBehaviour
{
    public static PlayerVision Script;

    [SerializeField] private Camera _camera;

    [SerializeField] private float _viewDistance;
    [SerializeField] private LayerMask _visionMask;

    private UseableScript _pickedItem;
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
        View();
    }
    
    private void View()
    {
        Vector3 direction = Quaternion.Euler(0, transform.rotation.y, 0) * transform.forward;
        Ray ray = new Ray(transform.position, direction);

        UnPickObject();

        if (Physics.Raycast(ray, out RaycastHit hit, _viewDistance, _visionMask))
        {
            if (hit.transform)
            {
                hit.transform.gameObject.TryGetComponent<UseableScript>(out _pickedItem);
                if (!_pickedItem) return;

                _playerAction.Useable = _pickedItem;
                _pickedItem.Select();
                return;
            }
        }
       
    }

    public void UnPickObject()
    {
        if (_pickedItem)
        {
            _pickedItem.UnSelect();
            _pickedItem = null;
            _playerAction.Useable = null;
        }
    }
    
    private void ScriptInit()
    {
        _playerAction = PlayerActionManager.Script;
    }

}
