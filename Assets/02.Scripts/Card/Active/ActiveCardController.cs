using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    public class ActiveCardController : CardController
    {
        public ActiveCardController(ActiveCardBaseSO cardBase, CardHandler cardHandler) : base(cardBase, cardHandler)
        {
            
        }

        public override void ApplyCard()
        {
            throw new System.NotImplementedException();
        }

        public override void ApplyMultipleValue(float value)
        {
            throw new System.NotImplementedException();
        }

        public override void RemoveCard()
        {
            throw new System.NotImplementedException();
        }
    }

}