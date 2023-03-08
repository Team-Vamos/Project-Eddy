using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{

    public abstract class PassiveCardBaseSO : CardBaseSO
    {
        public CardUpgrade[] upgrades = new CardUpgrade[4];

        private const string Passive = "패시브";

        //public CardStat[] cardStats;

        protected PassiveCardBaseSO(string t) : base(t) {}
        protected PassiveCardBaseSO() : base(Passive) {}

        // public override CardController CreateCardController(CardHandler cardHandler)
        // {
        //     return new PassiveCardController(this, cardHandler);
        // }
    }

}