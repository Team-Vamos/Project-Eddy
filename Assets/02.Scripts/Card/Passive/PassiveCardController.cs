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

        // TODO: 더하는 애들을 따로 스텟으로 빼놔서 곱셈 연산이 가능하게 수정해야함
        // private void Equip(Stat[] stats)
        // {
        //     foreach (Stat stat in stats)
        //     {
        //         switch (stat.valueType)
        //         {
        //             case ValueType.Add:
        //                 _cardHandler.PlayerStat.GetPlayerStat.allStat[stat.statType].statValue += stat.value;
        //                 break;
        //             case ValueType.Multiply:
        //                 _cardHandler.PlayerStat.GetPlayerStat.allStat[stat.statType].statValue *= stat.value;
        //                 break;
        //             default:
        //                 throw new System.ArgumentOutOfRangeException();
        //         }
        //     }
        // }
        // private void Remove(Stat[] stats)
        // {
        //     foreach (var stat in stats)
        //     {
        //         switch (stat.valueType)
        //         {
        //             case ValueType.Add:
        //                 _cardHandler.PlayerStat.GetPlayerStat.allStat[stat.statType].statValue -= stat.value;
        //                 break;
        //             case ValueType.Multiply:
        //                 _cardHandler.PlayerStat.GetPlayerStat.allStat[stat.statType].statValue /= stat.value;
        //                 break;
        //             default:
        //                 throw new System.ArgumentOutOfRangeException();
        //         }
        //     }
        // }

        protected virtual void OnLevelChanged(StatType type){}

        public override void ApplyMultipleValue(float multipleValue)
        {
            for (int i = 0; i < _cardBase.cardStats.Length; ++i)
            {
                _cardBase.cardStats[i].value *= multipleValue;
            }
        }

        public override void RemoveCard()
        {
            throw new System.NotImplementedException();
        }
    }

}