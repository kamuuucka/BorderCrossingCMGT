using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MoveRotateResizeObject : MonoBehaviour
{
    private Transform _objectTransform;
    private Vector3 _position;
    private Quaternion _rotation;
    private Vector3 _scale;
    private void OnEnable()
    {
        _objectTransform = GetComponent<Transform>();
        _position = _objectTransform.position;
        _rotation = _objectTransform.rotation;
        _scale = _objectTransform.localScale;
    }

    public void MoveX(float x)
    {
        _position = new Vector3(x, _position.y, _position.z);
        _objectTransform.position = _position;
    }
    public void MoveY(float y)
    {
        _position = new Vector3(_position.x, y, _position.z);
        _objectTransform.position = _position;
    }
    public void MoveZ(float z)
    {
        _position = new Vector3(_position.x, _position.y, z);
        _objectTransform.position = _position;
    }
    public void RotateX(float x)
    {
        _rotation = Quaternion.Euler(x, _rotation.y, _rotation.z);
        _objectTransform.rotation = _rotation;
    }
    public void RotateY(float y)
    {
        _rotation = Quaternion.Euler(_rotation.x, y, _rotation.z);
        _objectTransform.rotation = _rotation;
    }
    public void RotateZ(float z)
    {
        _rotation = Quaternion.Euler(_rotation.x, _rotation.y, z);
        _objectTransform.rotation = _rotation;
    }
    public void ScaleX(float x)
    {
        _scale = new Vector3(x, _scale.y, _scale.z);
        _objectTransform.localScale = _scale;
    }
    public void ScaleY(float y)
    {
        _scale = new Vector3(_scale.x, y, _scale.z);
        _objectTransform.localScale = _scale;
    }
    public void ScaleZ(float z)
    {
        _scale = new Vector3(_scale.x, _scale.y, z);
        _objectTransform.localScale = _scale;
    }
    
    
    
}
