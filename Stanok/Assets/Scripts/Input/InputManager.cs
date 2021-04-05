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
    private Toggle _automaticToggle;

    [SerializeField]
    private Toggle _manualToggle;


    [SerializeField]
    private Button _startButton, _stopButton, _stepButton , _clearButton;

    [SerializeField]
    private DataManager _dataManager;

    [SerializeField]
    private OutputManager _outputManager;

    private (int, int) _valuesXMax = (2, 20);
    private (int, int) _valuesYMax = (1, 4);
    private (int, int) _valuesZMax = (1, 2);
    private (int, int) _valuesTZad = (1, 30000);






    public InputValues GetInputValues()
    {
        return GetInputFieldsValues();
    }

    private bool ValidateInput(int value, (int, int) range)
    {
        if (value >= range.Item1 && range.Item2 >= value)
            return true;
        else
            return false;
    }



    private Toggle GetActiveToggle()
    {
        return _toggleGroup.ActiveToggles().FirstOrDefault();
    }
    private bool IsFieldsEmpty()
    {
        return String.IsNullOrEmpty(_xMaxField.text) || String.IsNullOrEmpty(_yMaxField.text) || String.IsNullOrEmpty(_zMaxField.text) || String.IsNullOrEmpty(_tZadField.text);
    }

    public void ActivateStepButton()
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

    public void SetInputStartedState()
    {
        _startButton.interactable = false;
        _stopButton.interactable = true;

        _xMaxField.interactable = false;
        _yMaxField.interactable = false;
        _zMaxField.interactable = false;
        _tZadField.interactable = false;

        _automaticToggle.interactable = false;
        _manualToggle.interactable = false;


        ActivateStepButton();

    }
    public void SetInputStopedState()
    {
        _startButton.interactable = true;
        _stopButton.interactable = false;
        _stepButton.interactable = false;

        _xMaxField.interactable = true;
        _yMaxField.interactable = true;
        _zMaxField.interactable = true;
        _tZadField.interactable = true;

        _automaticToggle.interactable = true;
        _manualToggle.interactable = true;


    }

    public void LoadInputValues()
    {
        _dataManager.Load();
        //SetInputFieldsValues(_values);
    }
    public void SaveInputValues()
    {
        InputValues _values = GetInputFieldsValues();
        if (_values!= null)
        {
            _dataManager.Save(_values);
        }
        else
        {
            _outputManager.InputErrorScreen();
        }
    }

    public void SetInputFieldsValues(InputValues inputValues)
    {
        _xMaxField.text = inputValues.XMax.ToString();
        _yMaxField.text = inputValues.YMax.ToString();
        _zMaxField.text = inputValues.ZMax.ToString();
        _tZadField.text = inputValues.TZad.ToString();

        if (inputValues.IsAutomatic)
        {
            _automaticToggle.isOn = true;
            _manualToggle.isOn = false;
        }
        else
        {
            _automaticToggle.isOn = false;
            _manualToggle.isOn = true;
        }

    }
    private InputValues GetInputFieldsValues()
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
            
            if (ValidateInput(inputValues.XMax, _valuesXMax) && ValidateInput(inputValues.YMax, _valuesYMax) && ValidateInput(inputValues.ZMax, _valuesZMax) && ValidateInput(inputValues.TZad, _valuesTZad))
            {
                return inputValues;
            }
            else
                return null;
        }
        else
            return null;
    }
}
