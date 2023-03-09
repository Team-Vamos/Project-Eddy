using UnityEngine;

namespace Card
{
    [System.Serializable]
    public class CardStat
    {
        public CardStat(StatType statType, ValueType valueType = ValueType.Add, float value = 1)
        {
            this.statType = statType;
            this.valueType = valueType;
            this.value = value;
        }
        public StatType statType;
        public ValueType valueType;
        public float value;
    }
}