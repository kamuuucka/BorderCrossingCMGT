using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(LineRenderer))]
public class MapGenerator : MonoBehaviour
{
    [SerializeField] private BoundaryData boundaryData;
    [SerializeField] private StringData promptsData;
    [SerializeField] private GameObject testPrefab;

    [SerializeField] private float radius = 2f;

    private readonly List<GameObject> _pointsSpawned = new();
    private readonly List<Vector3> _pointsPositions = new();
    private LineRenderer _lineRenderer;
    
    private void OnEnable()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        for (var i = 0; i < promptsData.data.Count; i++)
        {
            var angle = -i * Mathf.PI * 2 / promptsData.data.Count + Mathf.PI / 2;
            var xPosition = Mathf.Cos(angle) * radius;
            var yPosition = Mathf.Sin(angle) * radius;
            var originalPosition = transform.position;
            var spawnPosition = new Vector3(xPosition + originalPosition.x, yPosition + originalPosition.y, originalPosition.z) + Vector3.zero; // Adjust y-axis if needed

            // Instantiate the object at the calculated position
            var point = Instantiate(testPrefab, spawnPosition, Quaternion.identity, transform);
            _pointsSpawned.Add(point);
        }

        for (var i = 0; i < _pointsSpawned.Count; i++)
        {
            MoveAccordingToCenter(_pointsSpawned[i].transform, transform.position, boundaryData.data[i] * radius);
        }

        _lineRenderer.positionCount = _pointsSpawned.Count;
        _lineRenderer.SetPositions(_pointsPositions.ToArray());
    }

    private void MoveAccordingToCenter(Transform pointTransform, Vector3 centerPosition, float moveDistance)
    {
        var position = pointTransform.position;
        var directionTowardsCenter = (position - centerPosition).normalized;
        position += directionTowardsCenter * moveDistance;
        pointTransform.position = position;
        _pointsPositions.Add(position);
    }

}
