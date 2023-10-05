using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField] private GameObject _fadeUi;
    
    private GameManager _manager;

    private void Start()
    {
        _manager = GameManager.Script;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag(Values.FinishTag))
        {
            Values.CanPlay = false;
            _fadeUi.SetActive(true);
            AudioListener.volume = 1; 
        }
        else if(hit.gameObject.CompareTag(Values.TrapTag))
        {
            _manager.Lose();
        }
        else if (hit.gameObject.CompareTag(Values.AbyssTag))
        {
            _manager.Lose();
        }

    }
}
