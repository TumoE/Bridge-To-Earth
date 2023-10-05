using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFillings : MonoBehaviour
{
    public static PlayerFillings Script;

    [SerializeField] private PostProductionManager _postProduction;
    [SerializeField] private Animator _animator;

    [SerializeField] private float _lifeReduceStep;
    [SerializeField] private float _time;

    private UiManager _ui;
    private GameManager _manager;
    private PlayerMovement _playerMovement;

    private float _thirst = 100;
    private float _hunger = 100;
    private float _temperature = 36.6f;
    private float _energy = 100;
    private float _life = 100;

    private float _timer;

    private int _thirstPermission = 1;
    private int _hungerPermission = 1;
    private int _temperaturePermission = 1;
    private int _energyPermission = 1;

    private bool _isHeating;
    private bool _isSleeping;

    private bool _isWeak;
    private bool _isStart = true;

    private void Awake()
    {
        Script = this;
    }

    private void Start()
    {
        ScriptInit();
    }

    public void GetData(out float thirst, out float hunger, out float temperature, out float energy,out float life)
    {
        thirst = _thirst;
        hunger = _hunger;
        temperature = _temperature;
        energy = _energy;
        life = _life;
    }
    public void LoadData(float thirst, float hunger, float temperature, float energy, float life)
    {
        _thirst = thirst;
        _hunger = hunger;
        _temperature = temperature;
        _energy = energy;
        _life = life;
    }

    private void Update()
    {
        Live();
    }

    private void Live()
    {
        if (!_isStart || !Values.CanPlay) return;

        _timer += Time.deltaTime;

        if(_timer >= _time)
        {
            ReduceFillings();
            CheckFillings();

            _ui.ChangeLifeBar(_life);
            _ui.ChangeHungerBar(_hunger);
            _ui.ChangeThirstBar(_thirst);
            _ui.ChangeEnergyBar(_energy);
            _ui.ChangeTemperatureBar(_temperature);

            _timer = 0;
        }
    }

    public void Eat(float value)
    {
        _hungerPermission = 0;

        _hunger += value;
        _ui.ChangeHungerBar(_hunger);
        _animator.SetTrigger(Values.GetBerriesTag);

        if (_hunger >= 100)
        {
            _hunger = 100;
        }

        _hungerPermission = 1;
    }
    public void Drink(float value)
    {
        _animator.SetTrigger(Values.DrinkTag);
        _thirstPermission = 0;

        _thirst += value;
        _ui.ChangeThirstBar(_thirst);

        if (_thirst >= 100)
        {
            _thirst = 100;
        }
        _thirstPermission = 1;
    }
    public void Heat(float value)
    {
        _isHeating = !_isHeating;

        if (!_isHeating)
        {
            _temperaturePermission = 1;
            _animator.SetBool(Values.HeatTag, false);
            Values.CanMove = true;
            StopCoroutine(nameof(Heating));
        }
        else
        {
            _temperaturePermission = 0;
            StartCoroutine(nameof(Heating), value);
        }
    }
    public void Sleep(float value , Transform cot)
    {
        _isSleeping = !_isSleeping;

        if (!_isSleeping)
        {
            _energyPermission = 1;
            _animator.SetBool(Values.SleepTag, false);
            Values.CanMove = true;
            Values.CanRotation = true;
            StopCoroutine(nameof(Sleeping));
        }
        else
        {
            _energyPermission = 0;
            transform.rotation = Quaternion.Euler(transform.rotation.x, cot.rotation.y, transform.rotation.z);
            transform.position = new Vector3(cot.position.x, transform.position.y, cot.position.z);
            StartCoroutine(nameof(Sleeping), value);
        }
    }
   
    private IEnumerator Heating(float value)
    {
        Values.CanMove = false;

        if (Values.CanPlay)  _temperature += value;

        _animator.SetBool(Values.HeatTag, true);

        if (_temperature >= 43)
        {
            _temperature = 43f;
        }

        _ui.ChangeTemperatureBar(_temperature);

        yield return new WaitForSeconds(0.05f);
        StartCoroutine(nameof(Heating), value);
    }
    private IEnumerator Sleeping(float value)
    {
        if (!Values.CanPlay) StopCoroutine(nameof(Heating));

        Values.CanMove = false;
        Values.CanRotation = false;

        _energy += value;
        _animator.SetBool(Values.SleepTag, true);

        if (_energy >= 100)
        {
            _energy = 100f;
        }

        _ui.ChangeEnergyBar(_energy);

        if (!BreakSleeping())
        {
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(nameof(Sleeping), value);
        }
    }

    private bool BreakSleeping()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _isSleeping = false;

            _animator.SetBool(Values.SleepTag, false);

            Values.CanMove = true;
            Values.CanRotation = true;

            _energyPermission = 1;

            StopCoroutine(nameof(Sleeping));
            return true;
        }
        return false;
    }
    private void ReduceFillings()
    {
        _energy -= Random.Range(0.1f, 0.22f) * _energyPermission;
        _thirst -= Random.Range(0.05f, 0.15f) * _thirstPermission;
        _hunger -= Random.Range(0.05f, 0.15f) * _hungerPermission;
        _temperature -= Random.Range(0.01f, 0.03f) * _temperaturePermission;
    }

    private void CheckFillings()
    {
        if (_energy < 50 || _thirst < 50 || _hunger < 50 || _temperature < 26f || _temperature > 38f)
        {
            _life -= _lifeReduceStep;

            if(_life <= 0)
            {
                _manager.Lose();
            }

            _isWeak = true;
        }
        else _isWeak = false;

        _playerMovement.SlowDown(_isWeak);
        _postProduction.ChangeColorToRed(_isWeak);
        _ui.MakeFillingIndicatorActive(0, _hunger < 50);
        _ui.MakeFillingIndicatorActive(1, _thirst < 50);
        _ui.MakeFillingIndicatorActive(2, _temperature < 26f || _temperature > 38f);
        _ui.MakeFillingIndicatorActive(3, _energy < 50);
    }

    private void ScriptInit()
    {
        _ui = UiManager.Script;
        _manager = GameManager.Script;
        _playerMovement = PlayerMovement.Script;
    }


}
