using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{
    public static UiManager Script;

    [SerializeField] private GameObject[] _tool;
    [SerializeField] private GameObject[] _toolOutline;
    [SerializeField] private GameObject[] _fillingsIndicatorsOutline;

    [SerializeField] private IconsSO _icons;

    [SerializeField] private Image _lifeBar;
    [SerializeField] private Image _hungerBar;
    [SerializeField] private Image _thirstBar;
    [SerializeField] private Image _energyBar;
    [SerializeField] private Image _temperaturetBar;

    [SerializeField] private ItemUI _itemUi;
    [SerializeField] private GameObject _inventoryGroup;

    [SerializeField] private TMP_Text _temperatureText;

    [SerializeField] private Color[] _temperatureColors;

    private Dictionary<int, ItemUI> _itemsUI = new Dictionary<int, ItemUI>();

    void Awake()
    {
        Script = this;
    }

    void Start()
    {
        ScriptInit();
    }


    public void ChangeToolsIcon(int index)
    {
        _tool[index].SetActive(true);
    }
    public void SelectTool(int index)
    {
        for (int i = 0; i < _toolOutline.Length; i++)
        {
            _toolOutline[i].SetActive(false);
        }

        _toolOutline[index].SetActive(true);
    }

    public void AddItemIcon(int resourceID,int count)
    {
        ItemUI itemUI = Instantiate(_itemUi, _inventoryGroup.transform);
        itemUI.SetIcon(_icons.GetIcon(resourceID));
        itemUI.SetCountText(count);
        _itemsUI.Add(resourceID, itemUI);
    }
    public void ChangeItemField(int resourceID, int count)
    {
        if (!_itemsUI.ContainsKey(resourceID)) return;

        if (_itemsUI.TryGetValue(resourceID, out ItemUI itemUI)) 
        {
            itemUI.SetCountText(count);
        }
    }

    public void ChangeLifeBar(float value)
    {
        _lifeBar.fillAmount = value / 100;
    }
    public void ChangeHungerBar(float value)
    {
        _hungerBar.fillAmount = value / 100;
    }
    public void ChangeThirstBar(float value)
    {
        _thirstBar.fillAmount = value / 100;
    }
    public void ChangeEnergyBar(float value)
    {
        _energyBar.fillAmount = value / 100;
    }
    public void ChangeTemperatureBar(float value)
    {
        float amount = value / 43f;
        Color temperatureColor = Color.Lerp(_temperatureColors[0], _temperatureColors[1], amount);
        _temperaturetBar.fillAmount = amount;

        _temperaturetBar.color = temperatureColor;
        _temperatureText.color = temperatureColor;

        _temperatureText.text = $"{value.ToString("0.0")}°C";
    }


    public void MakeFillingIndicatorActive(int index,bool state)
    {
        _fillingsIndicatorsOutline[index].SetActive(state);
    }


    void ScriptInit()
    {
    }   

}
