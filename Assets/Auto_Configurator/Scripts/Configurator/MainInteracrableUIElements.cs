using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class MainInteractableUIElements : MonoBehaviour
{
    [SerializeField] private Button _resetButton;
    [SerializeField] private Button[] _changeColorButton;
    [SerializeField] private Button[] _changeWheelType;
    [SerializeField] private Button[] _changeWheelTypeAColor;
    [SerializeField] private Button[] _changeWheelTypeBColor;
    [SerializeField] private Button[] _changeWheelTypeCColor;
    [SerializeField] private Toggle _spoilerCheckbox;
    [SerializeField] private TextMeshProUGUI _totalCost;

    private int _totalCostValue = 0;
    private int _previousColorCost = 0;
    private int _previousWheelTypeCost = 0;
    private int _previousWheelTypeAColorCost = 0;
    private int _previousWheelTypeBColorCost = 0;
    private int _previousWheelTypeCColorCost = 0;
    private int _previousSpoilerCost = 0;
    private int _currentWheelType = -1;

    private Configurator _configurator;
    private CarConfig _config;

    public void Initialize(Configurator configurator)
    {
        _configurator = configurator;
        LoadConfig();
        _resetButton.onClick.AddListener(ResetButtonClicked);
        _spoilerCheckbox.isOn = _config.spoiler;
        if (_changeColorButton.Length >= 4)
        {
            _changeColorButton[0].onClick.AddListener(() => ChangeColorButtonClicked(_configurator.RedCar, _changeColorButton[0], ref _previousColorCost));
            _changeColorButton[1].onClick.AddListener(() => ChangeColorButtonClicked(_configurator.GreenCar, _changeColorButton[1], ref _previousColorCost));
            _changeColorButton[2].onClick.AddListener(() => ChangeColorButtonClicked(_configurator.BlueCar, _changeColorButton[2], ref _previousColorCost));
            _changeColorButton[3].onClick.AddListener(() => ChangeColorButtonClicked(_configurator.YellowCar, _changeColorButton[3], ref _previousColorCost));
        }

        if (_changeWheelType.Length >= 3)
        {
            _changeWheelType[0].onClick.AddListener(() => ChangeWheelTypeButtonClicked(0, _configurator.StainlessSteelWheel, _changeWheelType[0], ref _previousWheelTypeCost));
            _changeWheelType[1].onClick.AddListener(() => ChangeWheelTypeButtonClicked(1, _configurator.StainlessSteelWheel, _changeWheelType[1], ref _previousWheelTypeCost));
            _changeWheelType[2].onClick.AddListener(() => ChangeWheelTypeButtonClicked(2, _configurator.StainlessSteelWheel, _changeWheelType[2], ref _previousWheelTypeCost));
        }

        if (_changeWheelTypeAColor.Length >= 4)
        {
            _changeWheelTypeAColor[0].onClick.AddListener(() => ChangeWheelTypeColorButtonClicked(0, _configurator.StainlessSteelWheel, _changeWheelTypeAColor[0], ref _previousWheelTypeAColorCost));
            _changeWheelTypeAColor[1].onClick.AddListener(() => ChangeWheelTypeColorButtonClicked(0, _configurator.WhiteWheel, _changeWheelTypeAColor[1], ref _previousWheelTypeAColorCost));
            _changeWheelTypeAColor[2].onClick.AddListener(() => ChangeWheelTypeColorButtonClicked(0, _configurator.BlueWheel, _changeWheelTypeAColor[2], ref _previousWheelTypeAColorCost));
            _changeWheelTypeAColor[3].onClick.AddListener(() => ChangeWheelTypeColorButtonClicked(0, _configurator.YellowWheel, _changeWheelTypeAColor[3], ref _previousWheelTypeAColorCost));
        }

        if (_changeWheelTypeBColor.Length >= 2)
        {
            _changeWheelTypeBColor[0].onClick.AddListener(() => ChangeWheelTypeColorButtonClicked(1, _configurator.StainlessSteelWheel, _changeWheelTypeBColor[0], ref _previousWheelTypeBColorCost));
            _changeWheelTypeBColor[1].onClick.AddListener(() => ChangeWheelTypeColorButtonClicked(1, _configurator.BlueWheel, _changeWheelTypeBColor[1], ref _previousWheelTypeBColorCost));
        }

        if (_changeWheelTypeCColor.Length >= 1)
        {
            _changeWheelTypeCColor[0].onClick.AddListener(() => ChangeWheelTypeColorButtonClicked(2, _configurator.YellowWheel, _changeWheelTypeCColor[0], ref _previousWheelTypeCColorCost));
        }

        _spoilerCheckbox.onValueChanged.AddListener(SpoilerCheckboxValueChanged);
    }

    private void ResetButtonClicked()
    {
        _configurator.ResetSelections();

        _previousColorCost = 0;
        _previousWheelTypeCost = 0;
        _previousWheelTypeAColorCost = 0;
        _previousWheelTypeBColorCost = 0;
        _previousWheelTypeCColorCost = 0;
        _previousSpoilerCost = 0;

        _totalCostValue = 0;
        _totalCost.text = _totalCostValue.ToString();
        SaveConfig();
    }

    private void ChangeColorButtonClicked(Material color, Button button, ref int previousCost)
    {
        _configurator.ChangeCarColor(color);
        UpdateTotalCost(button, ref previousCost);
        SaveConfig();
    }

    private void ChangeWheelTypeButtonClicked(int wheelType, Material wheelColor, Button button, ref int previousCost)
    {
        if (_currentWheelType != wheelType)
        {
            _currentWheelType = wheelType;
            ResetWheelColorCost();
            _configurator.ChangeWheelTypeColor(wheelType, wheelColor);
            SaveConfig();
        }
        _configurator.ChangeWheelType(wheelType);
        UpdateTotalCost(button, ref previousCost);
        SaveConfig();
    }

    private void ChangeWheelTypeColorButtonClicked(int wheelType, Material wheelColor, Button button, ref int previousCost)
    {
        _configurator.ChangeWheelTypeColor(wheelType, wheelColor);
        UpdateTotalCost(button, ref previousCost);
        SaveConfig();
    }

    private void SpoilerCheckboxValueChanged(bool value)
    {
        _configurator.ToggleSpoiler(value);
        UpdateTotalCost(_spoilerCheckbox, ref _previousSpoilerCost);
        SaveConfig();
    }

    private void UpdateTotalCost(Selectable selectable, ref int previousCost)
    {
        string text = selectable.GetComponentInChildren<TextMeshProUGUI>().text;
        string digits = Regex.Replace(text, @"\D", "");

        if (int.TryParse(digits, out int number))
        {
            if (selectable is Toggle toggle && !toggle.isOn)
            {
                number = 0;
            }

            _totalCostValue = _totalCostValue - previousCost + number;
            _totalCost.text = _totalCostValue.ToString();
            previousCost = number;
        }
    }

    private void ResetWheelColorCost()
    {
        _totalCostValue -= _previousWheelTypeAColorCost;
        _totalCostValue -= _previousWheelTypeBColorCost;
        _totalCostValue -= _previousWheelTypeCColorCost;

        _previousWheelTypeAColorCost = 0;
        _previousWheelTypeBColorCost = 0;
        _previousWheelTypeCColorCost = 0;

        _totalCost.text = _totalCostValue.ToString();
    }

    private void SaveConfig()
    {
        _config.color = _configurator.CurrentColor;
        _config.wheelType = _configurator.CurrentWheelType;
        _config.wheelColor = _configurator.CurrentWheelColor;
        _config.spoiler = _configurator.SpoilerEnabled;

        string json = JsonUtility.ToJson(_config);
        File.WriteAllText(Application.persistentDataPath + "/car-config.json", json);
    }

    private void LoadConfig()
    {
        string path = Application.persistentDataPath + "/car-config.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            _config = JsonUtility.FromJson<CarConfig>(json);

            _configurator.ChangeCarColor(_config.color);
            _configurator.ChangeWheelType(_config.wheelType);
            _configurator.ChangeWheelTypeColor(_config.wheelType, _config.wheelColor);
            _configurator.ToggleSpoiler(_config.spoiler);
            _totalCost.text = _totalCostValue.ToString();
        }
        else
        {
            _config = new CarConfig();
            _configurator.ResetSelections();
        }
    }
}