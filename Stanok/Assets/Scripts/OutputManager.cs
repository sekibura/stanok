using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OutputManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _xField;
    [SerializeField]
    private TMP_Text _yField;
    [SerializeField]
    private TMP_Text _zField;

    public void UpdateValues(Vector3 coordinates)
    {
        _xField.text = coordinates.x.ToString();
        _yField.text = coordinates.y.ToString();
        _zField.text = coordinates.z.ToString();
    }
}
