using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    public class PassiveCardController : CardController
    {
        private readonly PassiveCardBaseSO _cardBase;
        protected int _level;

        public PassiveCardController(PassiveCardBaseSO cardBase, CardHandler cardHandler) : base(cardBase, cardHandler) 
        {
            _cardBase = cardBase;
        }

        public override void ApplyCard()
        {
            // _cardHandler.PlayerStat.OnStatChanged += OnStatChanged;
            // _cardHandler.PlayerStat.OnStatChanged += OnLevelChanged;

            // // TODO : 카드 장착 애니메이션

            // Equip(_addStats);

            // Equip(_cardBase.upgrades[_level - 1].stats);
        }

        public override void ApplyMultipleValue(float value)
        {
            throw new System.NotImplementedException();
        }

        public override void RemoveCard()
        {
            throw new System.NotImplementedException();
        }
    }

}