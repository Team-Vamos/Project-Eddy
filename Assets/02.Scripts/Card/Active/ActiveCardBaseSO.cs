using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    public class ActiveCardBaseSO : CardBaseSO
    {
        private const string ACTIVE = "액티브";

        public ActiveCardBaseSO() : base(ACTIVE) {}

        public ActiveCardBaseSO(string t) : base(t) {}

        public override CardController CreateCardController(CardHandler cardHandler)
        {
            throw new System.NotImplementedException();
        }
    }
}