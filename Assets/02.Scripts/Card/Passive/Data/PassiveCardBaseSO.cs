using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{

    public class PassiveCardBaseSO : CardBaseSO
    {
        public CardUpgrade[] upgrades = new CardUpgrade[1];

        private const string Passive = "패시브";

        [Range(0f, 1f)]
        public float weaponHardness;

        public List<Stat> _addStats;
        public StatController _statController;

        protected PassiveCardBaseSO(string t) : base(t) {}
        protected PassiveCardBaseSO() : base(Passive) {}

        public override CardController CreateCardController(CardHandler cardHandler)
        {
            throw new System.NotImplementedException();
        }
    }

}