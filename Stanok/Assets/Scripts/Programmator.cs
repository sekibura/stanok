using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Programmator : MonoBehaviour
{
    [SerializeField]
    private InputManager _inputManager;
    private InputValues _inputValues;
    private float _stepX = 1f;
    private float _stepY = 2f;
    private float _stepZ = 1f;

    public void StartWorking()
    {
        InitValues();
        if (IsValuesCorrect())
        {
            CompileControlCodes();
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

    private void CompileControlCodes()
    {
        Debug.Log("start compile");
        float currentX=0;
        float currentY=0;
        float currentZ=0;

        int directionY = 1;

        for (int z = 0; z < _inputValues.ZMax; z++)
        {
            for (int y = 0; y < _inputValues.YMax; y++)
            {
                
                //move forward
                for (int x = 0; x < _inputValues.XMax; x++)
                {
                    currentX += _stepX;
                    ReciveCode(currentX, currentY, currentZ);
                    Delay();
                }

                RiseBlade(currentZ);
                //move back
                for (int x = 0; x < _inputValues.XMax; x++)
                {
                    currentX -= _stepX;
                    ReciveCode(currentX,currentY,currentZ);
                    Delay();
                }

                LowerBlade(currentZ);
                currentY += directionY*_stepY;
            }

            currentZ += _stepZ;
            directionY *= -1;
            

        }
    }

    private void Delay()
    {


    }

    private void ReciveCode(float x, float y, float z)
    {
        Debug.Log(x + " " + y + " " + z);
    }

    private void RiseBlade(float currentZ)
    {
        currentZ -= _stepZ;
    }

    private void LowerBlade(float currentZ)
    {
        currentZ += _stepZ;
    }



}
