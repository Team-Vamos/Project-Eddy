using System.Collections;
using System.Collections.Generic;
using Card;
using UnityEngine;
using UnityEngine.UIElements;

public class StoreInfoBox
{
    public VisualElement Root { get; init; }

    private Label _title;
    private Label _description;
    private Label _effectInfo;

    public StoreInfoBox(VisualElement root)
    {
        Root = root;

        _title = root.Q<Label>("Title");
        _description = root.Q<Label>("Description");
        _effectInfo = root.Q<Label>("EffectInfo");
    }

    public void UpdateInfo(CardBaseSO so)
    {
        _title.text = so.name;
        _description.text = so.description;
        _effectInfo.text = so.effectInfo;
    }
}