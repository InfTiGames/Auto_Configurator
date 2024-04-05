using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Controls the surface to move.
/// </summary>
public class SurfaceToMove : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _obstacleLayer;
    private Camera _mainCamera;
    private readonly float _maxRaycastDistance = 10f;
    private MousePositionListener _mousePositionListener;

    public bool IsObjectSelected { get; private set; } = false;

    public Vector3 ClickPosition { get; private set; }

    /// <summary>
    /// Initializes the surface with the given mouse position listener.
    /// </summary>
    /// <param name="mousePositionListener">The mouse position listener to use.</param>
    public void Initialize(MousePositionListener mousePositionListener)
    {
        _mainCamera = Camera.main;
        _mousePositionListener = mousePositionListener;
        _mousePositionListener.MouseDragged += GetMousePosition;
    }

    private void GetMousePosition()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out _, _maxRaycastDistance, _obstacleLayer))
                return;

            if (Physics.Raycast(ray, out RaycastHit hit, _maxRaycastDistance, _groundLayer))
            {
                if (IsObjectSelected)
                {
                    Vector3 clickPos = hit.point;
                    ClickPosition = clickPos;
                }
            }
        }
    }

    /// <summary>
    /// Sets whether an object is selected.
    /// </summary>
    /// <param name="isSelected">Whether an object is selected.</param>
    public void SetSelectedObject(bool isSelected)
    {
        IsObjectSelected = isSelected;
    }

    private void OnDisable()
    {
        _mousePositionListener.MouseDragged -= GetMousePosition;
    }
}