using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ButtonPanelPair
{
    public List<GameObject> associatedPanels = new();
    public Button button;
    public int index;
    public List<bool> isActiveFlags = new();
}