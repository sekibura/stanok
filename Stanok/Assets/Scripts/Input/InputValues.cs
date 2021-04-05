using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class InputValues
{   [SerializeField]
    private int _xMax;
    [SerializeField]
    private int _yMax;
    [SerializeField]
    private int _zMax;
    [SerializeField]
    private int _tZad;
    [SerializeField]
    private bool _isAutomatic = true;

    public bool IsAutomatic
    {
        get { return _isAutomatic; }
        set { _isAutomatic = value; }
    }



    public InputValues()
    {
        _xMax = 0;
        _yMax = 0;
        _zMax = 0;
        _tZad = 0;
    }
    public InputValues(int XMax, int YMax, int ZMax, int TZad)
    {
        this.XMax = XMax;
        this.YMax = YMax;
        this.ZMax = ZMax;
        this.TZad = TZad;
    }

    public int TZad
    {
        get { return _tZad; }
        set { _tZad = value; }
    }
    public int ZMax
    {
        get { return _zMax; }
        set { _zMax = value; }
    }
    public int YMax
    {
        get { return _yMax; }
        set { _yMax = value; }
    }
    public int XMax
    {
        get { return _xMax; }
        set { _xMax = value; }
    }

    public override string ToString()
    {
        return XMax + " " + YMax + " " + ZMax + " " + TZad+" "+IsAutomatic;
    }
}
