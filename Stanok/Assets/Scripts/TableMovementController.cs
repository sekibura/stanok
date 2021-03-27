using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableMovementController : MonoBehaviour
{
    [SerializeField]
    private GameObject _blade;
    [SerializeField]
    private GameObject _bladeWay;
    [SerializeField]
    private GameObject _ways;
    private Vector3 _targetPosition;
    private float _timeOfStep = 1f;

    public void ReceiveCode(float x, float y, float z)
    {
        _targetPosition = new Vector3(x,z,-y);
        SpawnWay();
    }

    private void Update()
    {
        MoveToTargetPosition();   
    }

    private void MoveToTargetPosition()
    {
        Vector3 smoothedPos = Vector3.Lerp(_blade.transform.position, _targetPosition, _timeOfStep);
        _blade.transform.position = smoothedPos;

    }

    private void SpawnWay()
    {
        GameObject way =(GameObject)Instantiate(_bladeWay, _targetPosition, Quaternion.identity);
        way.transform.parent = _ways.transform;
    }

    public void ClearWays(int delay)
    {
        StartCoroutine(DeleteAllChilds(delay));
    }

    IEnumerator DeleteAllChilds(int t)
    {
        yield return new WaitForSeconds(t);
        foreach (Transform way in _ways.transform)
        {
            Destroy(way.gameObject);
        }
    }

  
}
