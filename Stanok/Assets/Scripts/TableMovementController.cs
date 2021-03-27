using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableMovementController : MonoBehaviour
{
    [SerializeField]
    private GameObject _blade;
    [SerializeField]
    private GameObject _bladeWay;
    private Vector3 _targetPosition;
    private float _timeOfStep = 1f;
    private float _t = 0f;
    private bool _newCode = true;
    public void ReceiveCode(float x, float y, float z)
    {
        _targetPosition = new Vector3(x,z,-y);
        _newCode = true;
    }

    private void Update()
    {
        MoveToTargetPosition();   
    }

    private void MoveToTargetPosition()
    {
        if (_newCode)
        {
            Vector3 smoothedPos = Vector3.Lerp(_blade.transform.position, _targetPosition, _timeOfStep);
            //_t += _timeOfStep * Time.deltaTime;
            _blade.transform.position = smoothedPos;
            _newCode = false;
            Instantiate(_bladeWay, _blade.transform.position, Quaternion.identity);
        }
      

    }


}
