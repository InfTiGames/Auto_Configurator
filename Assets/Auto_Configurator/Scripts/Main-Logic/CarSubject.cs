using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CarSubject : MonoBehaviour
{
    [SerializeField] private MeshRenderer _carBody;
    [SerializeField] private List<GameObject> _wheelTypes;
    [SerializeField] private List<GameObject> _wheelLocations;
    [SerializeField] private GameObject _spoiler;

    public Action OnCarClicked;
    public Action CarHovered;
    private Vector3 _startMousePosition;

    public bool IsSelected { get; private set; } = false;
    public bool IsHovered { get; private set; } = false;

    public void ChangeCarColor(Material colorMaterial)
    {
        foreach (var material in _carBody.materials)
            material.color = colorMaterial.color;
    }

    public void ChangeWheelType(int index)
    {
        if (index < 0 || index >= _wheelTypes.Count) return;

        foreach (var location in _wheelLocations)
        {
            for (int i = 0; i < location.transform.childCount; i++)
                location.transform.GetChild(i).gameObject.SetActive(false);

            location.transform.GetChild(index).gameObject.SetActive(true);
        }
    }

    public void ChangeWheelTypeColor(int wheelTypeIndex, Material colorMaterial)
    {
        if (wheelTypeIndex < 0 || wheelTypeIndex >= _wheelTypes.Count) return;

        foreach (var location in _wheelLocations)
            location.GetComponentInChildren<MeshRenderer>().material = colorMaterial;
    }

    public void ToggleSpoiler(bool enable)
    {
        if (enable)
            _spoiler.SetActive(true);
        else
            _spoiler.SetActive(false);
    }

    private void OnMouseDown()
    {
        _startMousePosition = Input.mousePosition;
    }

    private void OnMouseUp()
    {
        Vector3 endPosition = Input.mousePosition;
        if (_startMousePosition == endPosition)
        {
            IsSelected = true;
            OnCarClicked?.Invoke();
            CarHovered?.Invoke();
        }
    }

    public void Deselect()
    {
        IsSelected = false;
        OnCarClicked?.Invoke();
    }

    private void OnMouseEnter()
    {
        if (!IsHovered)
        {
            IsHovered = true;
            CarHovered?.Invoke();
        }
    }

    private void OnMouseExit()
    {
        if (IsHovered)
        {
            IsHovered = false;
            CarHovered?.Invoke();
        }
    }
}