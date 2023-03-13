using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerStatContainer
{
    public readonly Fillbar HpBar;
    public readonly Fillbar ExpBar;

    public PlayerStatContainer(VisualElement container)
    {
        HpBar = container.Q<Fillbar>("HpBar");
        ExpBar = container.Q<Fillbar>("Fillbar");
    }
}
