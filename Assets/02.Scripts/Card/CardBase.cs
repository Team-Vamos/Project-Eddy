using UnityEngine;
using System;


public enum CardRank
{
    S,
    A,
    B,
    C

}


namespace Card
{
    [Serializable]
    public class Stat
    {
        // public StatType statType;
        public ValueType valueType;
        public float value;
    }

    [Serializable]
    public class Upgrade
    {
        public Sprite weaponImage;
        public Stat[] stats;
    }

    public enum ValueType
    {
        Add,
        Multiply
    }

    [Serializable]
    public class CardImage
    {
        public Vector2 offset;
        public Vector2 scale = Vector2.one;
        public Sprite icon;
    }

    public abstract class CardBase : ScriptableObject
    {
        public Color color;
        public CardImage cardImage;
        [Space]
        public new string name;
        public CardRank rank;


        public string Type{ get; protected set; }

        [Multiline] public string description;

        protected CardBase(string t) { Type = t; }

        //public abstract CardController CreateCardController(CardHandler cardHandler);
    }
}