using Cinemachine;
using UnityEngine;
using System;

/// <summary>
/// Controls the zoom of the free look camera.
/// </summary>
public class FreeLookZoom : MonoBehaviour
{
    private CinemachineFreeLook _freeLook;
    private CinemachineFreeLook.Orbit[] _orbits;

    [SerializeField, Range(0.01f, 1f)] private float _minScale = 0.5f;
    [SerializeField, Range(1f, 5f)] private float _maxScale = 1f;

    /// <summary>
    /// The input axis for the mouse scroll wheel.
    /// </summary>
    private readonly string MouseScrollWheelInputAxis = "Mouse ScrollWheel";
    private AxisState _zAxis;

    private void OnValidate()
    {
        _minScale = Mathf.Max(0.01f, _minScale);
        _maxScale = Mathf.Max(_minScale, _maxScale);
    }

    public void Initialize()
    {
        if (TryGetComponent(out _freeLook))
        {
            InitializeOrbits();
            _zAxis = new AxisState(0f, 1f, false, true, 60f, 0.1f, 0.1f, MouseScrollWheelInputAxis, true);
        }
    }

    private void InitializeOrbits()
    {
        _orbits = new CinemachineFreeLook.Orbit[_freeLook.m_Orbits.Length];
        Array.Copy(_freeLook.m_Orbits, _orbits, _freeLook.m_Orbits.Length);
    }

    private void Update()
    {
        if (_freeLook != null)
        {
            _zAxis.Update(Time.deltaTime);
            UpdateOrbits();
        }
    }

    private void UpdateOrbits()
    {
        if (_orbits.Length != _freeLook.m_Orbits.Length)
            InitializeOrbits();

        var scale = Mathf.Lerp(_minScale, _maxScale, _zAxis.Value);

        for (var i = 0; i < _orbits.Length && i < _freeLook.m_Orbits.Length; i++)
        {
            _freeLook.m_Orbits[i].m_Height = _orbits[i].m_Height * scale;
            _freeLook.m_Orbits[i].m_Radius = _orbits[i].m_Radius * scale;
        }
    }
}