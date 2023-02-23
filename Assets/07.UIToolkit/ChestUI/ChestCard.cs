using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Card
{
    // TODO: 나중에 ShopCard하고 모양이 겹칠경우 부모 클래스 만들기
    public class ChestCard
    {
        private VisualElement _cardTemplate;
        private VisualElement _card;
        private VisualElement _voteContainer;
        private VisualElement _vote;

        private VisualElement _icon;

        private Label _nameText;
        private Label _descText;

        private Label _effect;

        private CardBaseSO _so;

        private ChestCardManager _manager;
        public ChestCard(VisualElement root, ChestCardManager manager, VisualTreeAsset vote)
        {
            _manager = manager;

            _cardTemplate = root;
            _card = root.Q<VisualElement>("Card");
            _voteContainer = _cardTemplate.Q<VisualElement>("VoteContainer");
            _vote = _voteContainer.Q<VisualElement>("Vote");

            _icon = _card.Q<VisualElement>("Icon");

            _nameText = _card.Q<Label>("Name");
            _descText = _card.Q<Label>("Description");
            _effect = _card.Q<Label>("Effect");

            _voteContainer.Clear();

            for (int i = 0; i < manager.VotePersonMaxCount; ++i)
            {
                VisualElement element = vote.Instantiate().Q<VisualElement>("Vote");
                _voteContainer.Add(element);
            }
        }
        public void SetCardSO(CardBaseSO so)
        {
            _so = so;
        }

        public void AddVotePerson()
        {
            _voteContainer[_manager.CurrentVotePersonCount].style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
        }

        public void DisableVotePerson()
        {
            foreach(VisualElement child in _voteContainer.Children())
            {
                child.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
            }
        }

        public void AddToClassList(string className)
        {
            _cardTemplate.AddToClassList(className);
        }

        public void RemoveFromClassList(string className)
        {
            _cardTemplate.RemoveFromClassList(className);
        }

        public void UpdateInfo(CardGradeSO grade)
        {
            _nameText.text = _so.name;
            _descText.text = _so.description;
            _icon.style.backgroundImage = new StyleBackground(_so.cardImage.icon);
            _card.style.unityBackgroundImageTintColor = grade.color;
        }

    }

}