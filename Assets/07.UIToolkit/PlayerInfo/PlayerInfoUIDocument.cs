using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInfoUIDocument : UIDocumentMono
{
    public PlayerStatContainer _playerStat { get; private set; }
    public PlayerCardContainer _playerCard { get; private set; }
    public PlayerEquipmentContainer _playerEquipment { get; private set; }

    protected override void OnEnable()
    {
        base.OnEnable();

        VisualElement container = _root.Q<VisualElement>("Container");

        VisualElement infoContainer = container.Q<VisualElement>("InfoContainer");
        VisualElement equipmentContainer = container.Q<VisualElement>("EquipmentContainer");

        _playerStat = new PlayerStatContainer(infoContainer.Q<VisualElement>("StatCotainer"));
        _playerCard = new PlayerCardContainer(infoContainer.Q<VisualElement>("CardContainer"));

        _playerEquipment = new PlayerEquipmentContainer(equipmentContainer);
    }



}
