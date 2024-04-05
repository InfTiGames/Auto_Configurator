using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Controls the UI animations.
/// </summary>
public class UIAnimation : MonoBehaviour
{
    public Vector2 DefaultUIElementPositions = new(75f, 200f);

    private const float AnimationDuration = 1.2f;

    private readonly Dictionary<Button, RectTransform> _buttonRectTransforms = new();
    private readonly Dictionary<GameObject, RectTransform> _panelRectTransforms = new();

    /// <summary>
    /// Switches the positions of the back and configurations buttons.
    /// </summary>
    /// <param name="back">The back button.</param>
    /// <param name="configurations">The configurations button.</param>
    /// <param name="carSelected">Whether a car is selected.</param>
    public void SwitchButtonsPosition(Button back, Button configurations, bool carSelected)
    {
        float positionX = carSelected ? -DefaultUIElementPositions.y : DefaultUIElementPositions.x;
        float positionY = carSelected ? DefaultUIElementPositions.y : -DefaultUIElementPositions.x;

        MoveButton(back, positionX, AnimationDuration);
        MoveButton(configurations, positionY, AnimationDuration);
    }

    /// <summary>
    /// Moves a button to a new position.
    /// </summary>
    /// <param name="button">The button to move.</param>
    /// <param name="position">The new position.</param>
    /// <param name="animationDuration">The duration of the animation.</param>
    public void MoveButton(Button button, float position, float animationDuration)
    {
        if (!_buttonRectTransforms.TryGetValue(button, out var rectTransform))
        {
            rectTransform = button.GetComponent<RectTransform>();
            _buttonRectTransforms[button] = rectTransform;
        }
        rectTransform.DOAnchorPosX(position, animationDuration);
    }

    /// <summary>
    /// Opens a panel on the Y axis.
    /// </summary>
    /// <param name="panel">The panel to open.</param>
    /// <param name="scale">The scale of the panel.</param>
    /// <param name="animationDuration">The duration of the animation.</param>
    public void OpenYAxesPanel(GameObject panel, float scale, float animationDuration)
    {
        if (!_panelRectTransforms.TryGetValue(panel, out var rectTransform))
        {
            rectTransform = panel.GetComponent<RectTransform>();
            _panelRectTransforms[panel] = rectTransform;
        }
        rectTransform.DOScaleY(scale, animationDuration);
    }
}