using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandMover : MonoBehaviour
{
    [SerializeField] private GameObject _pressed;
    [SerializeField] private Image _hand;

    private void Update()
    {
        bool isTouchHeld = Input.GetMouseButton(0);
        
        if (isTouchHeld)
        {
            _hand.transform.rotation = Quaternion.RotateTowards(_hand.transform.rotation, Quaternion.Euler(35f, 0f, 0f), 8f * Time.deltaTime * 60);
        }
        else
        {
            _hand.transform.rotation = Quaternion.RotateTowards(_hand.transform.rotation, Quaternion.identity, 8f * Time.deltaTime * 60);
        }

        _pressed.SetActive(isTouchHeld);
        _hand.enabled = (!isTouchHeld);

        Vector3 newPosition = Input.mousePosition;
        transform.position = newPosition;
    }
}
