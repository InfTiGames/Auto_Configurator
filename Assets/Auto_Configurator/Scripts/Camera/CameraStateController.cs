using UnityEngine;

/// <summary>
/// Controls the camera state.
/// </summary>
public class CameraStateController : MonoBehaviour
{
    /// <summary>
    /// The animator component attached to the game object.
    /// </summary>
    private Animator _animator;

    /// <summary>
    /// Initializes the controller by getting the animator component.
    /// </summary>
    public void Initialize()
    {
        _animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Sets the animation state.
    /// </summary>
    /// <param name="stateName">The hash of the animation state name.</param>
    public void SetAnimationState(int stateName)
    {
        if (_animator != null)
        {
            _animator.CrossFade(stateName, 0f);
        }
        else
        {
            Debug.LogError("Animator component is not initialized.");
        }
    }
}