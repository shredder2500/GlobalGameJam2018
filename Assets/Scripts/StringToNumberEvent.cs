﻿using System;
using UnityEngine;
using UnityEngine.Events;

public class StringToNumberEvent : MonoBehaviour
{
    [SerializeField]
    private FloatEvent _onEvent;

    public void Invoke(string value) => _onEvent.Invoke(float.Parse(value));
}
