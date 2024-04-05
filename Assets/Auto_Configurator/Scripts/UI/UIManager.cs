using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<ButtonPanelPair> _buttonPanelPairs = new();
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _hideButton;

    private bool _isCarSelected = true;
    private const float _animationDuration = 0.3f;

    private CarSubject _carSubject;
    private UIAnimation _animation;

    public void Initialize(CarSubject carSubject, UIAnimation animation)
    {
        _carSubject = carSubject;
        _animation = animation;
        _carSubject.OnCarClicked += UpdateUI;
        _backButton.onClick.AddListener(() =>
        {
            _carSubject.Deselect();
            HideAllPanels();
            _buttonPanelPairs[0].isActiveFlags[0] = false;
        });
        _hideButton.onClick.AddListener(HideAllPanels);

        for (int i = 0; i < _buttonPanelPairs.Count; i++)
        {
            var pair = _buttonPanelPairs[i];
            pair.index = i;
            if (pair.isActiveFlags.Count == 0)
            {
                pair.isActiveFlags = new List<bool>(new bool[pair.associatedPanels.Count]);
            }
            pair.button.onClick.AddListener(() =>
            {
                TogglePanels(pair);
            });
        }
    }

    private void UpdateUI()
    {
        _isCarSelected = !_carSubject.IsSelected;
        _backButton.interactable = !_isCarSelected;
        _animation.SwitchButtonsPosition(_backButton, _buttonPanelPairs[0].button, _isCarSelected);
    }

    private void TogglePanels(ButtonPanelPair pair)
    {
        TogglePanel(pair);
        CloseOtherPanels(pair);
    }

    private void TogglePanel(ButtonPanelPair pair)
    {
        pair.isActiveFlags[0] = !pair.isActiveFlags[0];
        bool isActive = pair.isActiveFlags[0];
        float scale = isActive ? 1 : 0;
        _animation.OpenYAxesPanel(pair.associatedPanels[0], scale, _animationDuration);
    }

    private void CloseOtherPanels(ButtonPanelPair currentPair)
    {
        int startIndex = 1;
        int endIndex = _buttonPanelPairs.Count;

        if (currentPair.index == 0 || currentPair.index == 1)
            startIndex = 2;
        else if (currentPair.index == 2)
            startIndex = 1;
        else if (currentPair.index < 6)
            startIndex = 3;

        for (int i = startIndex; i < endIndex; i++)
        {
            if (i == currentPair.index) continue;
            var pair = _buttonPanelPairs[i];
            for (int j = 0; j < pair.associatedPanels.Count; j++)
            {
                if (pair.isActiveFlags[j])
                {
                    _animation.OpenYAxesPanel(pair.associatedPanels[j], 0, _animationDuration);
                    pair.isActiveFlags[j] = false;
                }
            }
        }
    }

    private void HideAllPanels()
    {
        foreach (var pair in _buttonPanelPairs)
        {
            for (int i = 0; i < pair.associatedPanels.Count; i++)
            {
                _animation.OpenYAxesPanel(pair.associatedPanels[i], 0, _animationDuration);
                pair.isActiveFlags[i] = false;
            }
        }
    }

    private void OnDestroy()
    {
        _carSubject.OnCarClicked -= UpdateUI;
        _backButton.onClick.RemoveAllListeners();
        foreach (var pair in _buttonPanelPairs)
            pair.button.onClick.RemoveAllListeners();
    }
}