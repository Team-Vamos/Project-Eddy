using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Card
{
    public class ChestCardClick
    {
        private CardBaseSO _so;
        private ChestUIDocument _document{ get; init; }

        private ChestCardManager _manager{ get; init; }

        private float _voteMultipleValue;

        private ChestCard _card;

        public ChestCardClick(ChestUIDocument document, ChestCardManager manager, ChestCard card)
        {
            _document = document;
            _manager = manager;
            _card = card;
        }

        public void SetCardSO(CardBaseSO so)
        {
            _so = so;
        }

        public CardController SetCardController(float? voteMultipleValue)
        {
            CardController controller = _so.CreateCardController(null);

            if(voteMultipleValue != null)
            {
                controller.ApplyMultipleValue(voteMultipleValue.Value);
            }

            return controller;

        }

        public void SelectCard(ClickEvent evt)
        {
            if(_document.IsTweening)return;

            bool isDaddyTurn = _manager.CurrentVotePersonCount >= _manager.VotePersonMaxCount;

            CardController controller = _so.CreateCardController(null);

            if(isDaddyTurn)
            {
                controller.ApplyMultipleValue(_voteMultipleValue);

                _manager.ResetVotePerson();

                // TODO: 타임스케일 되돌리기
            }
            else if(_manager.IsVote)
            {
                _card.AddVotePerson();
                
                _manager.AddVotePerson();
                _voteMultipleValue += _manager.VoteMultipleValue;

            }

            _manager.BottleSpawner.SpawnBabyBottle(controller);
            _document.DisableContainer();
        }
    }
}