using System;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    [System.Serializable]
    public class CardUpgrade
    {
        public CardImage cardImage;

        public CardStat[] cardStats = {
            new CardStat(StatType.Hp), new CardStat(StatType.Armor), new CardStat(StatType.Atk),
            new CardStat(StatType.AtkSpd), new CardStat(StatType.Mine), new CardStat(StatType.Spd)
            };

        public List<StatModifier> GetStatModifiers(CardBaseSO cardBase)
        {
            List<StatModifier> modifierList = new List<StatModifier>();
            for (int i = 0; i < cardStats.Length; ++i)
            {
                StatModifier modifier = cardStats[i].valueType switch
                {
                    ValueType.Add => new StatModifierAdditive(cardBase, cardStats[i].value, cardStats[i].statType),
                    ValueType.Multiply => new StatModifierMultiplicative(cardBase, cardStats[i].value, cardStats[i].statType, StatOrderType.Normal_Multiplicative),
                    _ => throw new ArgumentOutOfRangeException()
                };
                modifierList.Add(modifier);
            }

            return modifierList;
        }

        // TODO : 스텟클래스가지고 있기
    }
}