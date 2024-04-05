using UnityEngine;

public class UserMovement : MonoBehaviour
{
    private Vector3 _targetPosition;
    private bool _isMoving = false;
    private bool _canChangeDirection = true;
    private const float Speed = 8f;
    private UserObserver _observer;
    private SurfaceToMove _surface;
    private MousePositionListener _mousePositionListener;

    public void Initialize(UserObserver observer, SurfaceToMove surface, MousePositionListener mousePositionListener)
    {
        _observer = observer;
        _surface = surface;
        _mousePositionListener = mousePositionListener;
        _mousePositionListener.MouseDragged += CheckMovement;
    }

    private void Update()
    {
        UpdateSurfaceSelection();
        if (_isMoving && _observer.PossibleToMove)
        {
            MoveToTarget();
        }
    }

    private void UpdateSurfaceSelection()
    {
        _surface.SetSelectedObject(_observer.PossibleToMove);
    }

    private void CheckMovement()
    {
        if (_canChangeDirection && _observer.PossibleToMove)
        {
            StartMoving();
        }
    }

    private void StartMoving()
    {
        _isMoving = true;
        _targetPosition = GetTargetPosition();
    }

    private Vector3 GetTargetPosition()
    {
        return new Vector3(_surface.ClickPosition.x, transform.position.y, _surface.ClickPosition.z);
    }

    private void MoveToTarget()
    {
        float step = Speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, step);

        if (transform.position == _targetPosition)
        {
            StopMoving();
        }
        else
        {
            _canChangeDirection = false;
        }
    }

    private void StopMoving()
    {
        _isMoving = false;
        _canChangeDirection = true;
    }

    private void OnDisable()
    {
        _mousePositionListener.MouseDragged -= CheckMovement;
        _observer.Dispose();
    }
}