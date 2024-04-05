using UnityEngine;

/// <summary>
/// Abstract base class for camera input.
/// </summary>
public abstract class CameraInput : MonoBehaviour
{
    /// <summary>
    /// The input axis for the mouse X position.
    /// </summary>
    protected readonly string MouseXInputAxis = "Mouse X";

    /// <summary>
    /// The input axis for the mouse Y position.
    /// </summary>
    protected readonly string MouseYInputAxis = "Mouse Y";

    /// <summary>
    /// An empty string constant.
    /// </summary>
    protected readonly string EmptyString = "";

    /// <summary>
    /// The button index for the left mouse button.
    /// </summary>
    protected readonly int LeftMouseButtonIndex = 0;

    /// <summary>
    /// The value representing a zero input axis.
    /// </summary>
    protected readonly int ZeroInputAxisValue = 0;

    /// <summary>
    /// Initializes the camera input.
    /// </summary>
    public abstract void Initialize();

    /// <summary>
    /// Clears the input fields.
    /// </summary>
    protected abstract void ClearFields();

    /// <summary>
    /// Updates the input values.
    /// </summary>
    protected abstract void UpdateInput();

    /// <summary>
    /// Called every frame to update the input.
    /// </summary>
    protected virtual void Update()
    {
        UpdateInput();
    }
}