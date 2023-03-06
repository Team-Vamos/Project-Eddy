using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Card;

public class StoreCard
{
    private VisualElement _root;

    private VisualElement _icon;

    private Label _price;
    private CardManager _manager;
    private CardBaseSO _so;

    public StoreCard(VisualElement cardRoot, CardManager manager)
    {
        _root = cardRoot;
        _manager = manager;

        _icon = cardRoot.Q<VisualElement>("Mask").Q<VisualElement>("Icon");

        _price = _root.Q<Label>("Price");
    }

    

    public void AddToClassListAtRoot(string className)
    {
        _root.AddToClassList(className);
    }

    public void RemoveFromClassListAtRoot(string className)
    {
        _root.RemoveFromClassList(className);
    }

    public void SetCardSO(CardBaseSO so)
    {
        _so = so;
    }

    public void UpdateInfo(int price)
    {
        _icon.style.backgroundImage = new StyleBackground(_so.cardImage.icon);
        _price.text = price.ToString();
    }
}