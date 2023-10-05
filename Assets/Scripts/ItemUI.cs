using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMPro.TMP_Text _countText;

    public void SetIcon(Sprite sprite)
    {
        _icon.sprite = sprite;
    }
    public void SetCountText(int count)
    {
        _countText.text = $"{count}";
    }

}
