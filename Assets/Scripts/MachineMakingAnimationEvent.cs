using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineMakingAnimationEvent : MonoBehaviour
{
    private MachineScript _myMachine;
    public void Animate(MachineScript machine)
    {
        _myMachine = machine;
        gameObject.SetActive(true);
    }
    public void Finsih()
    {
        gameObject.SetActive(false);
        _myMachine.AnimationDone();
    }
}
