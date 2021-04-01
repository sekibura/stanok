using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public  class InputManager: MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _xMaxField, _yMaxField, _zMaxField, _tZadField;
    [SerializeField]
    private ToggleGroup _toggleGroup;

    [SerializeField]
    private Button _stepButton;
    

     public InputValues GetInputValues()
    {
        if (!IsFieldsEmpty())
        {
            InputValues inputValues = new InputValues();
            inputValues.XMax = Convert.ToInt32(_xMaxField.text);
            inputValues.YMax = Convert.ToInt32(_yMaxField.text);
            inputValues.ZMax = Convert.ToInt32(_zMaxField.text);
            inputValues.TZad = Convert.ToInt32(_tZadField.text);
            if (_toggleGroup.AnyTogglesOn())
            {
                Debug.Log(GetActiveToggle().name);
                if (GetActiveToggle().name.Equals("AutomaticToggle"))
                    inputValues.IsAutomatic = true;
                else
                    inputValues.IsAutomatic = false;
            }
            return inputValues;
        }
        else
            return null;

    }

    private Toggle GetActiveToggle()
    {
        return _toggleGroup.ActiveToggles().FirstOrDefault();
    }
    private bool IsFieldsEmpty()
    {
        return String.IsNullOrEmpty(_xMaxField.text) || String.IsNullOrEmpty(_yMaxField.text) || String.IsNullOrEmpty(_zMaxField.text) || String.IsNullOrEmpty(_tZadField.text);
    }

    public void OnStartButton()
    {
        if (_toggleGroup.AnyTogglesOn())
        {
            Debug.Log(GetActiveToggle().name);
            if (GetActiveToggle().name.Equals("AutomaticToggle"))
                _stepButton.interactable = false;
            else
                _stepButton.interactable = true;
        }
    }
}
