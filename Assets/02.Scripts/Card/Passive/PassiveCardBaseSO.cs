using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{

    public abstract class PassiveCardBaseSO : CardBaseSO
    {
        public CardUpgrade[] upgrades = new CardUpgrade[1];

        private const string Passive = "패시브";

        [Range(0f, 1f)]
        public float weaponHardness;

        public CardStat[] cardStats;

        protected PassiveCardBaseSO(string t) : base(t) {}
        protected PassiveCardBaseSO() : base(Passive) {}

        // public override CardController CreateCardController(CardHandler cardHandler)
        // {
        //     return new PassiveCardController(this, cardHandler);
        // }
    }

}