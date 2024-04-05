using UnityEngine;

/// <summary>
/// Observes changes in the camera state.
/// </summary>
public class CameraStateChangerObserver : MonoBehaviour
{
    private CameraStateController _cameraStateController;
    private CarSubject _carSubject;

    /// <summary>
    /// The animation state for the car free look.
    /// </summary>
    private readonly int CarFreeLookAnimationState = Animator.StringToHash("CarFreeLook");

    /// <summary>
    /// The animation state for going back to main.
    /// </summary>
    private readonly int BackToMainAnimationState = Animator.StringToHash("Main");

    /// <summary>
    /// Initializes the observer with the given car subject.
    /// </summary>
    /// <param name="carSubject">The car subject to observe.</param>
    public void Initialize(CarSubject carSubject)
    {
        _cameraStateController = GetComponent<CameraStateController>();
        _carSubject = carSubject;
        _carSubject.OnCarClicked += SwitchState;
    }

    private void OnDisable()
    {
        _carSubject.OnCarClicked -= SwitchState;
    }

    /// <summary>
    /// Switches the camera state based on whether the car is selected.
    /// </summary>
    private void SwitchState()
    {
        var animationState = _carSubject.IsSelected ? CarFreeLookAnimationState : BackToMainAnimationState;
        _cameraStateController.SetAnimationState(animationState);
    }
}