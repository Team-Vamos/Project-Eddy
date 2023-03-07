using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Card;
using UnityEngine.UIElements;
using System;

public class StoreCardEvent
{
    private CardBaseSO _so;
    private StoreUIDocument _uiDocument;
    private StoreManager _manager;
    private VisualElement _cardRoot;

    public int Price { get; set; }

    private StoreInfoBox _infoBox{ get; init; }
    public StoreCardEvent(VisualElement cardRoot, StoreUIDocument document, StoreManager manager, StoreInfoBox infoBox)
    {
        _cardRoot = cardRoot;
        _uiDocument = document;
        _manager = manager;
        _infoBox = infoBox;
    }

    public void SetCardSO(CardBaseSO so)
    {
        _so = so;
    }

    public void SelectCard(ClickEvent evt)
    {
        if(_uiDocument.IsTween) return;
        if(_manager.Cash < Price)return;
        Debug.Log("눌림");

        _manager.Cash -= Price;

        _cardRoot.RemoveFromClassList("on");
        CardController controller = _so.CreateCardController(null);
        _manager.BottleSpawner.SpawnBabyBottle(controller);
    }

    public void EnterEvent(MouseEnterEvent evt)
    {
        _infoBox.Root.AddToClassList("on");
        _infoBox.UpdateInfo(_so);
    }

    public void LeaveEvent(MouseLeaveEvent evt)
    {
        _infoBox.Root.RemoveFromClassList("on");
        
    }
}