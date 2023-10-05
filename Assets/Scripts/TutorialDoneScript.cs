using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDoneScript : MonoBehaviour
{
    private SaveLoadManager _saveLoad;

    private bool _isTutorialDisabled;

    private void Start()
    {
        _saveLoad = SaveLoadManager.Script;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Values.PlayerTag))
        {
            _isTutorialDisabled = true;
            _saveLoad.Save();
            Destroy(transform.parent.gameObject);
        }
    }

    public bool IsTutorialDisabled { get { return _isTutorialDisabled; } }

}
