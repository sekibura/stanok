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
    private float _stepY = 1f;
    private float _stepZ = 1f;

    public void StartWorking()
    {
        InitValues();
        if (IsValuesCorrect())
        {
            //CompileControlCodes();
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
        Debug.Log("start compile");
        float currentX=0;
        float currentY=0;
        float currentZ=0;

        int directionY = 1;
        ReciveCode(0, 0, 0);
        for (int z = 0; z < _inputValues.ZMax; z++)
        {
            ReciveCode(currentX, currentY, currentZ);
            for (int y = 0; y < _inputValues.YMax; y++)
            {
                
                //move forward
                for (int x = 0; x < _inputValues.XMax; x++)
                {
                    currentX += _stepX;
                    ReciveCode(currentX, currentY, currentZ);
                    yield return new WaitForSeconds(_inputValues.TZad);
                }

                //RiseBlade(currentZ);
                currentZ += _stepZ;
                ReciveCode(currentX, currentY, currentZ);
                //move back
                for (int x = 0; x < _inputValues.XMax; x++)
                {
                    currentX -= _stepX;
                    ReciveCode(currentX,currentY,currentZ);
                    yield return new WaitForSeconds(_inputValues.TZad);
                }

                //LowerBlade(currentZ);
                currentZ -= _stepZ;
                currentY += directionY*_stepY;
                ReciveCode(currentX, currentY, currentZ);
            }

            currentZ -= _stepZ;
            directionY *= -1;
            

        }
        ReciveCode(0, 0, 0);
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
