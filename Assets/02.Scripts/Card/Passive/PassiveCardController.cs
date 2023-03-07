using System;
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
            for (int i = 0; i < _cardBase.cardStats.Length; ++i)
            {
                StatModifier modifier = _cardBase.cardStats[i].valueType switch 
                {
                    ValueType.Add => new StatModifierAdditive(_cardBase, _cardBase.cardStats[i].value, _cardBase.cardStats[i].statType),
                    ValueType.Multiply => new StatModifierMultiplicative(_cardBase, _cardBase.cardStats[i].value, _cardBase.cardStats[i].statType, StatOrderType.Normal_Multiplicative),
                    _ => throw new ArgumentOutOfRangeException()
                };
                _cardHandler.PlayerStat.AddStatModifier(modifier);
            }

            // // TODO : 카드 장착 애니메이션
        }

        public override void RemoveCard()
        {
            _cardHandler.PlayerStat.RemoveAllStatModifiersFromSource(_cardBase);
        }

        // TODO: 더하는 애들을 따로 스텟으로 빼놔서 곱셈 연산이 가능하게 수정해야함

        protected virtual void OnLevelChanged(StatType type){}

        public override void ApplyMultipleValue(float multipleValue)
        {
            for (int i = 0; i < _cardBase.cardStats.Length; ++i)
            {
                _cardBase.cardStats[i].value += _cardBase.cardStats[i].value * multipleValue;
            }
        }

        
    }

}