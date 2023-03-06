using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ResourceInfo
{
    private VisualElement _root;
    private VisualElement _icon;
    private Label _text;

    public ResourceInfo(VisualElement root)
    {
        _root = root;
        _icon = _root.Q<VisualElement>("Icon");
        _text = _root.Q<Label>("Count");
    }

    public void ChangeString(int value)
    {
        _text.text = $": {value.ToString()}";
    }
}
