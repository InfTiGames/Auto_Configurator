using Cinemachine;
using UnityEngine;

/// <summary>
/// Controls the free look camera.
/// </summary>
[RequireComponent(typeof(CinemachineFreeLook))]
public class FreeLook : CameraInput
{
    /// <summary>
    /// The free look camera component.
    /// </summary>
    private CinemachineFreeLook _freeLookCamera;

    public override void Initialize()
    {
        _freeLookCamera = GetComponent<CinemachineFreeLook>();
        ClearFields();
    }

    protected override void ClearFields()
    {
        _freeLookCamera.m_XAxis.m_InputAxisName = EmptyString;
        _freeLookCamera.m_YAxis.m_InputAxisName = EmptyString;
    }

    /// <summary>
    /// Updates the input for the free look camera.
    /// </summary>
    protected override void UpdateInput()
    {
        var isLeftMouseButtonPressed = Input.GetMouseButton(LeftMouseButtonIndex);
        _freeLookCamera.m_XAxis.m_InputAxisValue = isLeftMouseButtonPressed ? Input.GetAxis(MouseXInputAxis) : ZeroInputAxisValue;
        _freeLookCamera.m_YAxis.m_InputAxisValue = isLeftMouseButtonPressed ? Input.GetAxis(MouseYInputAxis) : ZeroInputAxisValue;
    }
}