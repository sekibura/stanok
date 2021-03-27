using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Slider _xAngle;
    
    [SerializeField]
    private Slider _yAngle;

    [SerializeField]
    private GameObject _cameraRotator;
    [SerializeField]
    private Camera _camera;

    private void Start()
    {
        Debug.Log(_cameraRotator.transform.eulerAngles);
        _xAngle.value = _camera.transform.eulerAngles.x;
        _yAngle.value = _cameraRotator.transform.eulerAngles.y;
    }

    public void UpdateAxisX(float angle)
    {
        _cameraRotator.transform.eulerAngles = new Vector3(angle, _cameraRotator.transform.eulerAngles.y, _cameraRotator.transform.eulerAngles.z);

    }
    public void UpdateAxisY(float angle)
    {
        _cameraRotator.transform.eulerAngles = new Vector3(_cameraRotator.transform.eulerAngles.x, angle, _cameraRotator.transform.eulerAngles.z);
    }
}
