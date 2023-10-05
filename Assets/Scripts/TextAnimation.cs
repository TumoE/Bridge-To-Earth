using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAnimation : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _textField;

    [SerializeField] private string _text;

    [SerializeField] private float _animationTime;

    [SerializeField] private bool _isAnimationRun;

    private int _pointer;
    private int _tempCharacterCount;

    private float _animationTimer;

    private void Start()
    {
        _tempCharacterCount = _text.Length;
    }

    private void Update()
    {
        Animate();
    }

    private void Animate()
    {
        if (!_isAnimationRun) return;

        _animationTimer += Time.deltaTime;

        if(_animationTimer >= _animationTime)
        {
            if (_pointer < _tempCharacterCount)
            {
                _textField.text += _text[_pointer++];
            }
            else
            {
                _isAnimationRun = false;
            }
            _animationTimer = 0;
        }
    }
}
