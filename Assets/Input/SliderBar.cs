using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SliderBar : MonoBehaviour
{
    private PlayerInputActions _inputActions;
    [SerializeField] private Slider _slider;
    
    // Start is called before the first frame update
    void Start()
    {
        _inputActions = new PlayerInputActions();

        _inputActions.Slider.Enable();
    }

    private void Update()
    {
        if (_inputActions.Slider.FillBar.IsPressed())
        {
            if (_slider.value < 1)
            {
                _slider.value += 0.001f; 
            } else if (_slider.value > 1)
            {
                _slider.value = 1;
            }
        }
        else
        {
            if (_slider.value > 0)
            {
                _slider.value -= 0.001f;
            }
        }
    }
}
