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

    private bool _isStoped = true;

    public void StartWorking()
    {
        InitValues();
        if (IsValuesCorrect())
        {
            StartCoroutine(CompileControlCodes());
        }
        


    }
    private void InitValues()
    {
        
        _inputValues = _inputManager.GetInputValues();

        if (_inputValues != null)
        {
            Debug.Log("not empty " + _inputValues.ToString());
           
        }
        else
        {
            Debug.Log("Empty");
        }
    }
    private bool IsValuesCorrect()
    {
        return _inputValues != null;
    }

    IEnumerator CompileControlCodes()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("start compile");             
        _isStoped = false;
        float currentX=0;
        float currentY=0;
        float currentZ=0;

        int directionY = 1;
        SendCode(0, 0, 0);
        for (int z = 0; z < _inputValues.ZMax; z++)
        {
            if (_isStoped)
                break;
            SendCode(currentX, currentY, currentZ);
            for (int y = 0; y < _inputValues.YMax; y++)
            {
                if (_isStoped)
                    break;
                //move forward
                for (int x = 0; x < _inputValues.XMax; x++)
                {
                    if (_isStoped)
                        break;

                    currentX += _stepX;
                    SendCode(currentX, currentY, currentZ);
                    yield return new WaitForSeconds(_inputValues.TZad*0.001f);
                }

                //RiseBlade(currentZ);
                currentZ += _stepZ;
                SendCode(currentX, currentY, currentZ);
                //move back
                for (int x = 0; x < _inputValues.XMax; x++)
                {
                    if (_isStoped)
                        break;
                    currentX -= _stepX;
                    SendCode(currentX,currentY,currentZ);
                    yield return new WaitForSeconds(_inputValues.TZad * 0.001f);
                }

                //LowerBlade(currentZ);
                currentZ -= _stepZ;
                if (y!= _inputValues.YMax-1)
                    currentY += directionY*_stepY;
                SendCode(currentX, currentY, currentZ);
            }

            currentZ -= _stepZ;
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
        _tableMovementController.ClearWays(1);
    }

   
    
    

}
