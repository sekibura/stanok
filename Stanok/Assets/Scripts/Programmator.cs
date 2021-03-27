using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Programmator : MonoBehaviour
{
    [SerializeField]
    private InputManager _inputManager;
    [SerializeField]
    private TableMovementController _tableMovementController;

    private InputValues _inputValues;
    private float _stepX = 0.3f;
    private float _stepY = 0.3f;
    private float _stepZ = 0.2f;

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
                    yield return new WaitForSeconds(_inputValues.TZad*0.001f);
                }

                //RiseBlade(currentZ);
                currentZ += _stepZ;
                ReciveCode(currentX, currentY, currentZ);
                //move back
                for (int x = 0; x < _inputValues.XMax; x++)
                {
                    currentX -= _stepX;
                    ReciveCode(currentX,currentY,currentZ);
                    yield return new WaitForSeconds(_inputValues.TZad * 0.001f);
                }

                //LowerBlade(currentZ);
                currentZ -= _stepZ;
                if (y!= _inputValues.YMax-1)
                    currentY += directionY*_stepY;
                ReciveCode(currentX, currentY, currentZ);
            }

            currentZ -= _stepZ;
            directionY *= -1;
            

        }
        ReciveCode(0, 0, 0);
    }

 

    private void ReciveCode(float x, float y, float z)
    {
        Debug.Log(x + " " + y + " " + z);
        _tableMovementController.ReceiveCode(x, y, z);
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
