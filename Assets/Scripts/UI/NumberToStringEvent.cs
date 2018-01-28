using System;
using UnityEngine;
using UnityEngine.Events;

public class NumberToStringEvent : MonoBehaviour
{
    [SerializeField] private StringEvent _onEvent;

    public void Invoke(float value)
    {
        _onEvent.Invoke(value.ToString());
    }
}