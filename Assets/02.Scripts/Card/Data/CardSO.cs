using UnityEngine;
using System;


namespace Card
{
    [CreateAssetMenu(fileName = "CardGrade", menuName = "SO/Card/CardSO", order = 0)]
    public abstract class CardSO : ScriptableObject
    {
        public Sprite cardImage;
        [Space]
        public new string name;
        public RankType rank;


        public string Type{ get; protected set; }

        [Multiline] public string description;

        protected CardSO(string t) { Type = t; }

        public abstract CardController CreateCardController(CardHandler cardHandler);
    }
}