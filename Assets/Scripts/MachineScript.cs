using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineScript : MonoBehaviour
{
    [SerializeField] private OffetScript _offer;
    [SerializeField] private MachineMakingAnimationEvent _animationGameObject;

    public void Give()
    {
        if (_offer.TakeResource())
        {
            if (_animationGameObject)
            {
                _animationGameObject.Animate(this);
            }
        }
    }
    public void AnimationDone()
    {
        _offer.Drop();
    }
   
}

