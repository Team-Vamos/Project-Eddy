using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    public class PassiveCardController : CardController
    {
        private readonly PassiveCardBaseSO _cardBase;
        protected int _level = 1;
        
        // 투표을 위해 빼둔 스텟
        protected CardUpgrade _currentCardStat;

        public PassiveCardController(PassiveCardBaseSO cardBase, CardHandler cardHandler) : base(cardBase, cardHandler) 
        {
            _cardBase = cardBase;
        }

        public override void ApplyCard()
        {
            _currentCardStat = _cardBase.upgrades[_level - 1];
            // TODO : 플레이어에서 CardHandler의 cardImage의 sprite로 sprite를 변경하는거로 코드가 필요 nullable하는게 좋을 듯
            
            List<StatModifier> modifierList = _currentCardStat.GetStatModifiers(_cardBase);

            modifierList.ForEach(modifier => _cardHandler.PlayerStat.AddStatModifier(modifier));

            // // TODO : 카드 장착 애니메이션
        }

        public override void RemoveCard()
        {
            _cardHandler.PlayerStat.RemoveAllStatModifiersFromSource(_cardBase);
        }


        public virtual void OnLevelChanged(){
            _level++;

            RemoveCard();

            ApplyCard();
        }

        public override void ApplyMultipleValue(float multipleValue)
        {
            for (int i = 0; i < _currentCardStat.cardStats.Length; ++i)
            {
                _currentCardStat.cardStats[i].value += _currentCardStat.cardStats[i].value * multipleValue;
            }
        }

        
    }

}