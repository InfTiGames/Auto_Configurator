using UnityEngine;

/// <summary>
/// Configures the car.
/// </summary>
public class Configurator : MonoBehaviour
{
    private CarSubject _carSubject;

    //Car Colors
    public Material DefaultCarColor;
    public Material RedCar;
    public Material GreenCar;
    public Material BlueCar;
    public Material YellowCar;

    //Wheel Colors  
    public Material DefaultWheelColor;
    public Material StainlessSteelWheel;
    public Material WhiteWheel;
    public Material BlueWheel;
    public Material YellowWheel;

    public Material CurrentColor { get; private set; }
    public int CurrentWheelType { get; private set; }
    public Material CurrentWheelColor { get; private set; }
    public bool SpoilerEnabled { get; private set; }

    /// <summary>
    /// Initializes the configurator with the given car subject.
    /// </summary>
    /// <param name="carSubject">The car subject to configure.</param>
    public void Initialize(CarSubject carSubject)
    {
        _carSubject = carSubject;
    }

    /// <summary>
    /// Changes the car color.
    /// </summary>
    /// <param name="colorMaterial">The material to use for the car color.</param>
    public void ChangeCarColor(Material colorMaterial)
    {
        _carSubject.ChangeCarColor(colorMaterial);
        CurrentColor = colorMaterial;
    }

    /// <summary>
    /// Changes the wheel type.
    /// </summary>
    /// <param name="index">The index of the wheel type to change to.</param>
    public void ChangeWheelType(int index)
    {
        _carSubject.ChangeWheelType(index);
        CurrentWheelType = index;
    }

    /// <summary>
    /// Changes the wheel type color.
    /// </summary>
    /// <param name="wheelTypeIndex">The index of the wheel type to change the color for.</param>
    /// <param name="colorMaterial">The material to use for the wheel color.</param>
    public void ChangeWheelTypeColor(int wheelTypeIndex, Material colorMaterial)
    {
        _carSubject.ChangeWheelTypeColor(wheelTypeIndex, colorMaterial);
        CurrentWheelColor = colorMaterial;
    }

    /// <summary>
    /// Toggles the spoiler.
    /// </summary>
    /// <param name="enable">Whether to enable the spoiler.</param>
    public void ToggleSpoiler(bool enable)
    {
        _carSubject.ToggleSpoiler(enable);
        SpoilerEnabled = enable;
    }

    /// <summary>
    /// Resets the selections to their default values.
    /// </summary>
    public void ResetSelections()
    {
        ChangeCarColor(DefaultCarColor);
        ChangeWheelType(0);
        ChangeWheelTypeColor(0, DefaultWheelColor);
        ChangeWheelTypeColor(1, DefaultWheelColor);
        ChangeWheelTypeColor(2, DefaultWheelColor);
        ToggleSpoiler(false);
    }
}