using UnityEngine;
using System;


namespace Card
{

    public abstract class CardSO : ScriptableObject
    {
        public Color color;
        public CardImageData cardImage;
        [Space]
        public new string name;
        public RankType rank;


        public string Type{ get; protected set; }

        [Multiline] public string description;

        protected CardSO(string t) { Type = t; }

        public abstract CardController CreateCardController(CardHandler cardHandler);
    }
}