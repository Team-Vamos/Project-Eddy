using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCardContainer
{
    private List<VisualElement> _cards;

    public PlayerCardContainer(VisualElement container)
    {
        _cards = container.Query<VisualElement>(className: "info-card").ToList();
    }
}
