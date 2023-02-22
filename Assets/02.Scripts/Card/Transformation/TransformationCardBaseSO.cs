using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    public abstract class TransformationCardBaseSO : PassiveCardBaseSO
    {
        private const string TRANSFORMATION = "변신";

        // public override CardController CreateCardController(CardHandler cardHandler)
        // {
        //     return new TransformationCardController(this, cardHandler);
        // }

        protected TransformationCardBaseSO() : base(TRANSFORMATION) { }

    }
}
