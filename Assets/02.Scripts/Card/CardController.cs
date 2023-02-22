using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    public abstract class CardController
    {
        public readonly CardBaseSO CardBase;

        protected CardHandler _cardHandler { get; private set; }


        protected CardController(CardBaseSO cardBase, CardHandler cardHandler)
        {
            CardBase = cardBase;
            _cardHandler = cardHandler;
        }

        public void SetCardHandler(CardHandler cardHandler)
        {
            _cardHandler = cardHandler;
        }


        public abstract void ApplyMultipleValue(float value);

        public abstract void ApplyCard();

        public abstract void RemoveCard();
    }
}