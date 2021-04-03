using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OutputManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _xField;
    [SerializeField]
    private TMP_Text _yField;
    [SerializeField]
    private TMP_Text _zField;
    [SerializeField]
    private GameObject _errorPanel;

    public void UpdateValues(Vector3 coordinates)
    {
        _xField.text = coordinates.x.ToString();
        _yField.text = coordinates.y.ToString();
        _zField.text = coordinates.z.ToString();
    }

    public void InputErrorScreen()
    {
        _errorPanel.SetActive(true);
    }
}
