using Cinemachine;
using UnityEngine;

/// <summary>
/// Controls the point of view camera.
/// </summary>
[RequireComponent(typeof(CinemachineVirtualCamera))]
public class POV : CameraInput
{
    private CinemachineVirtualCamera _POVCamera;
    private CinemachinePOV _POVComponent;

    public override void Initialize()
    {
        _POVCamera = GetComponent<CinemachineVirtualCamera>();
        _POVComponent = _POVCamera.GetCinemachineComponent<CinemachinePOV>();
        ClearFields();
    }

    protected override void ClearFields()
    {
        _POVComponent.m_HorizontalAxis.m_InputAxisName = EmptyString;
        _POVComponent.m_VerticalAxis.m_InputAxisName = EmptyString;
    }

    /// <summary>
    /// Updates the input for the point of view camera.
    /// </summary>
    protected override void UpdateInput()
    {
        var isLeftMouseButtonPressed = Input.GetMouseButton(LeftMouseButtonIndex);
        _POVComponent.m_HorizontalAxis.m_InputAxisValue = isLeftMouseButtonPressed ? Input.GetAxis(MouseXInputAxis) : ZeroInputAxisValue;
        _POVComponent.m_VerticalAxis.m_InputAxisValue = isLeftMouseButtonPressed ? Input.GetAxis(MouseYInputAxis) : ZeroInputAxisValue;
    }
}