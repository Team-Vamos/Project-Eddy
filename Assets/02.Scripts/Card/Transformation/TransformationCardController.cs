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

        protected override void OnLevelChanged(StatType type)
        {
            if (_level == 1)
            {
                Transformation();
            }
        }

        public abstract void Transformation();
    }
}
