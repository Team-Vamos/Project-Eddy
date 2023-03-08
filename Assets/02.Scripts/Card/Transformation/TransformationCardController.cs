using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    public abstract class TransformationCardController : PassiveCardController
    {
        private readonly TransformationCardBaseSO _cardBase;
        public TransformationCardController(TransformationCardBaseSO cardBase, CardHandler cardHandler) : base(cardBase, cardHandler)
        {
            _cardBase = cardBase;
        }

        public override void OnLevelChanged()
        {
            if (_level == 1)
            {
                Transformation();
            }
        }

        public abstract void Transformation();
    }
}