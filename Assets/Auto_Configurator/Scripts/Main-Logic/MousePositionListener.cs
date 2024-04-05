using System;
using UnityEngine;

public class MousePositionListener : MonoBehaviour
{
    private const int LeftMouseButton = 0;
    private Vector3 _startPosition;
    public event Action MouseDragged;

    private void Update()
    {
        HandleMouseInput();
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(LeftMouseButton))
        {
            RecordStartPosition();
        }

        if (Input.GetMouseButtonUp(LeftMouseButton))
        {
            CheckMousePositionAndInvokeEvent();
        }
    }

    private void RecordStartPosition()
    {
        _startPosition = Input.mousePosition;
    }

    private void CheckMousePositionAndInvokeEvent()
    {
        Vector3 endPosition = Input.mousePosition;
        if (_startPosition == endPosition)
        {
            InvokeMouseDraggedEvent();
        }
    }

    private void InvokeMouseDraggedEvent()
    {
        MouseDragged?.Invoke();
    }
}