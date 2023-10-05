using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillingResourceScript : MonoBehaviour
{
    public enum Type
    {
        Water = 0,
        FoodBerries = 1,
        Heat = 2,
        Sleep = 3,
    };

    [SerializeField] private Type _fillingType;

    [SerializeField] private VegitableScript _vegitable;

    private PlayerFillings _playerFillings;

    private void Start()
    {
        ScriptInit();
    }

    private void ScriptInit()
    {
        _playerFillings = PlayerFillings.Script;
    }

    public void Do()
    {
        switch (_fillingType)
        {
            case Type.Water:
                _playerFillings.Drink(1.25f);
                break;
            case Type.FoodBerries:
                
                if (_vegitable.Pull())
                {
                    _playerFillings.Eat(_vegitable.Calorie);
                }

                break;
            case Type.Heat:
                _playerFillings.Heat(0.02f);
                break;
            case Type.Sleep:
                _playerFillings.Sleep(0.65f , transform);
                break;
            default:
                break;
        }
    }

}
