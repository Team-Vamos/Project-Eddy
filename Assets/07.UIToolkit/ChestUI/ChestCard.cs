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
        private VisualElement _voteContainer;
        private VisualElement _vote;

        private VisualElement _icon;

        private Label _nameText;
        private Label _descText;

        private Label _effectInfo;

        private CardBaseSO _so;

        private ChestManager _manager;
        public ChestCard(VisualElement root, ChestManager manager, VisualTreeAsset vote)
        {
            _manager = manager;

            _cardTemplate = root;
            _card = root.Q<VisualElement>("Card");
            _voteContainer = _cardTemplate.Q<VisualElement>("VoteContainer");
            _vote = _voteContainer.Q<VisualElement>("Vote");

            _icon = _card.Q<VisualElement>("Icon");

            _nameText = _card.Q<Label>("Name");
            _descText = _card.Q<Label>("Description");
            _effectInfo = _card.Q<Label>("Effect");

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
            _effectInfo.text = _so.effectInfo;
            _icon.style.backgroundImage = new StyleBackground(_so.cardImage.icon);
            _card.style.unityBackgroundImageTintColor = grade.color;
        }

    }

}