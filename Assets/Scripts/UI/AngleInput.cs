using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class AngleInput : MonoBehaviour
{
    private InputField _input;
    private void Start() => _input = GetComponent<InputField>();

    [SerializeField]
    private FloatEvent _onSubmit;

    public void Invoke(string value)
    {
        ForceFocus();
        if (string.IsNullOrEmpty(value)) return;
        _onSubmit.Invoke(Clamp(value));
    }

    private float Clamp (string value)
    {
        ForceFocus();
        return Mathf.Clamp(float.Parse(value), 15, 165);
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
