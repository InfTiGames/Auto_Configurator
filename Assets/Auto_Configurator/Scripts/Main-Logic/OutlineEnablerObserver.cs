using UnityEngine;

public class OutlineEnablerObserver : MonoBehaviour
{
    private CarSubject _carSubject;
    private MeshRenderer _meshRenderer;

    public void Initialize()
    {
        _carSubject = GetComponent<CarSubject>();
        _meshRenderer = GetComponentInChildren<MeshRenderer>();

        if (_carSubject != null)
        {
            _carSubject.CarHovered += UpdateCarOutline;
        }
    }

    private void UpdateCarOutline()
    {
        _meshRenderer.enabled = !_carSubject.IsSelected && _carSubject.IsHovered;
    }

    private void OnDisable()
    {
        if (_carSubject != null)
        {
            _carSubject.CarHovered -= UpdateCarOutline;
        }
    }
}