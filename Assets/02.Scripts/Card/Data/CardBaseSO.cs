using UnityEngine;
using System;


namespace Card
{
    //[CreateAssetMenu(fileName = "CardGrade", menuName = "SO/Card/CardSO", order = 0)]
    public abstract class CardBaseSO : ScriptableObject
    {
        public CardImage cardImage;
        [Space]
        public new string name;
        public RankType rank;


        public string Type{ get; protected set; }

        [Multiline] public string description;
        [Multiline] public string effectInfo;

        protected CardBaseSO(string t) { Type = t; }

        public abstract CardController CreateCardController(CardHandler cardHandler);
    }
}