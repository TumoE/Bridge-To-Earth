using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildScript : MonoBehaviour
{
    [SerializeField] private ContentData _buildState;

    [SerializeField] private List<Renderer> _renderers;

    [SerializeField] private RequirementsCanvasScript _requirementsCanvas;

    [SerializeField] private GameObject _readyObject;
    [SerializeField] private Material _unstructuredMaterial;

    private List<Material> _originalMateials = new List<Material>();

    private PlayerInventory _playerInventory;
    private SaveLoadManager _saveLoadScript;

    private int _pointer;

    void Start()
    {
        ScriptInit();
        SetBuiltMaterials();
        LoadBuilding();
    }
    void LoadBuilding()
    {
        if(_buildState.isReady)
        {
            Instantiate(_readyObject, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else
        {
            for (int i = 0; i < _buildState.requirements.Count; i++)
            {
                int complitedPartsCount = _buildState.requirements[i].maxCount - _buildState.requirements[i].value;
                for (int j = 0; j < complitedPartsCount; j++)
                {
                    ChangeMaterial();
                }
            }
        }
    }

    public void Build()
    {
        Resource neededResource = _buildState.requirements[_pointer].resource;
        int count = _buildState.requirements[_pointer].value;

        if (count > 0)
        {
            if (_playerInventory.HasResource(neededResource.id))
            {
                _playerInventory.UseFromInventory(neededResource.id,1);

                _buildState.requirements[_pointer].value--;

                if (_buildState.requirements[_pointer].value <= 0)
                {
                    _buildState.requirements.RemoveAt(_pointer);
                    _requirementsCanvas.RemoveRequirementInfo(_pointer);
                }

                ChangeMaterial();
                CheckEndOfBuild();

                if (_buildState.requirements.Count > 0)
                {
                    _requirementsCanvas.ChangeCanvasInfo(_buildState.requirements);
                }
            }
        }
    }
    private void ChangeMaterial()
    {
        _renderers[0].material = _originalMateials[0];
        _renderers.RemoveAt(0);
        _originalMateials.RemoveAt(0);
    }
    private void CheckEndOfBuild()
    {
        if (_buildState.requirements.Count <= 0)
        {
            _buildState.isReady = true;
            Instantiate(_readyObject, transform.position, transform.rotation);
            _saveLoadScript.Save();
            Destroy(gameObject);
        }
    }
    private void SetBuiltMaterials()
    {
        for (int i = 0; i < _renderers.Count; i++)
        {
            _renderers[i].material = _unstructuredMaterial;
        }
    }
    private void ScriptInit()
    {
        _playerInventory = PlayerInventory.Script;
        _saveLoadScript = SaveLoadManager.Script;
        for (int i = 0; i < _renderers.Count; i++)
        {
            _originalMateials.Add(_renderers[i].material);
        }

        _requirementsCanvas.SetCanvasInfo(_buildState);
    }

}
