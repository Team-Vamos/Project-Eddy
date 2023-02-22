using UnityEngine;

namespace Card
{
    public abstract class WeaponCardBaseSO : PassiveCardBaseSO
    {
        private const string WEAPON = "무기";

        public WeaponCardBaseSO() : base(WEAPON) {}

        // public override CardController CreateCardController(CardHandler cardHandler)
        // {
        //     return new WeaponCardController(this, cardHandler);
        // }

    }
}