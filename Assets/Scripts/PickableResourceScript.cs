using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableResourceScript : MonoBehaviour
{
    [SerializeField] private Resource _resource;
    [SerializeField] private AnimationCurve _pickCurve;
    [SerializeField] private AnimationCurve _scaleCurve;
    
    private BoxCollider _collider;
    private PlayerVision _playerVision;
    private PlayerInventory _playerInventory;
    private PlayerActionManager _playerAction;

    private Vector3 _startScale;
    private Vector3 _startPosition;

    private float _pickTime;
    private bool _canPick;

    private void Start()
    {
        ScriptInit();
    }

    private void Update()
    {
        Pick();
    }

    private void Pick()
    {
        if (!_canPick) return;

        _pickTime = Mathf.Clamp01(_pickTime + Time.deltaTime * 2);

        transform.position = Vector3.Lerp(_startPosition, _playerAction.RightHand.position, _pickCurve.Evaluate(_pickTime));
        transform.localScale = Vector3.Lerp(_startScale, _startScale/10, _scaleCurve.Evaluate(_pickTime));

        if (_pickTime >= 1)
        {
            if (_resource.isTool) _playerInventory.AddTool(_resource.name);
            else _playerInventory.AddItem(_resource.id);

            _playerVision.UnPickObject();
            Destroy(gameObject);
        }

    }

    private void ScriptInit()
    {
        _playerVision = PlayerVision.Script;
        _playerInventory = PlayerInventory.Script;
        _playerAction = PlayerActionManager.Script;
        _collider = GetComponent<BoxCollider>();
    }

    public bool CanPick
    {
        get { return _canPick; }
        set 
        {
            _collider.enabled = false;
            _startScale = transform.localScale;
            _startPosition = transform.position;
            _canPick = value; 
        }
    }
    public Resource GetResource
    {
        get { return _resource; }
    }
}
