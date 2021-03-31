using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Programmator : MonoBehaviour
{
    [SerializeField]
    private InputManager _inputManager;
    [SerializeField]
    private TableMovementController _tableMovementController;
    [SerializeField]
    private OutputManager _outputManager;


    private InputValues _inputValues;
    private float _stepX = 0.3f;
    private float _stepY = 0.35f;
    private float _stepZ = 0.2f;

    private int _currentXStep = 0;
    private int _currentYStep = 0;
    private int _currentZStep = 0;

    private float _currentX = 0;
    private float _currentY = 0;
    private float _currentZ = 0;


    private bool _isStoped = true;

    public void StartWorking()
    {
        InitValues();
        if (IsValuesCorrect())
        {
            StartCoroutine(CompileControlCodes());
        }
    }

    public void Step()
    {
        if (IsValuesCorrect())
        {
            StartCoroutine(CompileControlCodes());
        }
    }

    IEnumerator CompileControlCodes()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("start compile");             
        _isStoped = false;
  

        int directionY = 1;
        //SendCode(0, 0, 0);
        for (int z = _currentZStep; z < _inputValues.ZMax; z++)
        {
            if (_isStoped)
                break;
            //SendCode(currentX, currentY, currentZ);
            for (int y = _currentYStep; y < _inputValues.YMax; y++)
            {
                if (_isStoped)
                    break;
                //move forward
                for (int x = _currentXStep; x < _inputValues.XMax; x++)
                {
                    if (_isStoped)
                        break;

                    _currentX += _stepX;
                    SendCode(_currentX, _currentY, _currentZ);
                    if (_inputValues.IsAutomatic)
                        break;
                    yield return new WaitForSeconds(_inputValues.TZad*0.01f);
                }

                //RiseBlade(currentZ);
                _currentZ += _stepZ;

                SendCode(_currentX, _currentY, _currentZ);
                //move back
                for (int x = 0; x < _inputValues.XMax; x++)
                {
                    if (_isStoped)
                        break;
                    _currentX -= _stepX;
                    SendCode(_currentX,_currentY,_currentZ);
                    if (_inputValues.IsAutomatic)
                        break;
                    yield return new WaitForSeconds(_inputValues.TZad * 0.01f);
                }

                //LowerBlade(currentZ);
                _currentZ -= _stepZ;

                if (y!= _inputValues.YMax-1)
                    _currentY += directionY*_stepY;
                SendCode(_currentX, _currentY, _currentZ);
            }

            _currentZ -= _stepZ;
            directionY *= -1;
            

        }
        SendCode(0, 0, 0);
    }
 
 

    private void SendCode(float x, float y, float z)
    {
        _outputManager.UpdateValues(FloatToSteps(x, y, z));
        _tableMovementController.ReceiveCode(x, y, z);
    }

   
    private Vector3 FloatToSteps(float x, float y, float z)
    {
        return new Vector3(Convert.ToInt32(x / _stepX), Convert.ToInt32(y / _stepY), Convert.ToInt32(z / _stepZ));
    }

    public void Stop()
    {
        _isStoped = true;
        SendCode(0, 0, 0);
    }


    private void InitValues()
    {
        _inputValues = _inputManager.GetInputValues();

        _currentXStep = 0;
        _currentYStep = 0;
        _currentZStep = 0;

        _currentX = 0;
        _currentY = 0;
        _currentZ = 0;

}
    private bool IsValuesCorrect()
    {
        return _inputValues != null;
    }

    

}
