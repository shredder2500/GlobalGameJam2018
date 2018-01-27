using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class AngleInput : MonoBehaviour
{
    private InputField _input;

    private void Start() => _input = GetComponent<InputField>();

    private void Update ()
    {
        ForceFocus();
        if(!string.IsNullOrEmpty(_input.text))
        {
            _input.text = Mathf.Clamp(float.Parse(_input.text), 0, 180).ToString();
        }
    }

    private void ForceFocus()
    {
        if (!_input.isFocused)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
            _input.ActivateInputField();
        }
    }
}
