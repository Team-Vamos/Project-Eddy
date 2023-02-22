using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Card
{
    public class ChestCard
    {
        private VisualElement _cardTemplate;
        private VisualElement _card;
        private VisualElement _icon;

        private Label _nameText;
        private Label _descText;

        private Label _effect;

        public ChestCard(VisualElement root)
        {
            _cardTemplate = root;
            _card = root.Q<VisualElement>("Card");

            _icon = _card.Q<VisualElement>("Icon");

            _nameText = _card.Q<Label>("Name");
            _descText = _card.Q<Label>("Description");
            _effect = _card.Q<Label>("Effect");

        }

        public void AddToClassList(string className)
        {
            _cardTemplate.AddToClassList(className);
        }

        public void RemoveFromClassList(string className)
        {
            _cardTemplate.RemoveFromClassList(className);
        }

        public void UpdateInfo(CardBaseSO so, CardGrade grade)
        {
            _nameText.text = so.name;
            _descText.text = so.description;
            _icon.style.backgroundImage = new StyleBackground(so.cardIcon);
            _cardTemplate.style.backgroundColor = grade.color;
        }

    }

}