using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TribeToSurvive.Model;

public class CameraInput : MonoBehaviour
{
    [SerializeField] private TouchHandler _touchHandler;

    private CameraMovement _model;
    private bool _cameraIsMoving = false;
    private Vector2 _previousTouchPosition;

    private void OnStartTouch()
    {
        _cameraIsMoving = true;
        _previousTouchPosition = _touchHandler.TouchPosition;
        _model.StartMoving();
    }
    private void OnEndTouch()
    {
        _cameraIsMoving = false;
        _model.EndMoving();
    }

    public void Initialize(CameraMovement model)
    {
        _model = model;
    }

    private void OnEnable()
    {
        _touchHandler.TouchIsStarted += OnStartTouch;
        _touchHandler.TouchIsEnded += OnEndTouch;
    }

    private void OnDisable()
    {
        _touchHandler.TouchIsStarted -= OnStartTouch;
        _touchHandler.TouchIsEnded -= OnEndTouch;
    }

    private void Update()
    {
        if(_cameraIsMoving == true)
        {
            Vector2 currentTouchPosition = _touchHandler.TouchPosition;
            _model.Accelerate(currentTouchPosition - _previousTouchPosition);
            _previousTouchPosition = currentTouchPosition;
        }
    }
}
