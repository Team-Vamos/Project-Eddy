using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Card
{
    public class ChestCard
    {
        private VisualElement _cardRoot;
        private CardBaseSO _cardSO;


        private VisualElement _icon;

        private Label _nameText;
        private Label _descText;

        private Label _effect;

        private CardGrade _grade;
        public ChestCard(VisualElement root, CardBaseSO so, CardGrade grade)
        {
            _cardSO = so;
            _cardRoot = root;

            _icon = _cardRoot.Q<VisualElement>("Icon");

            _nameText = _cardRoot.Q<Label>("Name");
            _descText = _cardRoot.Q<Label>("Description");
            _effect = _cardRoot.Q<Label>("Effect");

            _grade = grade;
        }

        public void UpdateInfo()
        {
            _nameText.text = _cardSO.name;
            _descText.text = _cardSO.description;
            _icon.style.backgroundImage = new StyleBackground(_cardSO.cardIcon);
            _cardRoot.style.backgroundColor = _grade.color;
        }

    }

}