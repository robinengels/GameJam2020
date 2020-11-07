using System;
using UnityEngine;

public class PointsPerUnitGiver : MonoBehaviour
{
    [SerializeField] private float pointsPerUnit;
    
    private Transform _transform;
    private Vector3 _previousPos;
    
    private void Awake()
    {
        _transform = transform;
        _previousPos = _transform.position;
    }

    private void Update()
    {
        var currentPos = _transform.position;
        var metersRan = currentPos.x - _previousPos.x;
        var points = pointsPerUnit * metersRan;
        GameManager.Instance.ScoreManager.IncreaseScore(points);
        _previousPos = currentPos;
    }
}