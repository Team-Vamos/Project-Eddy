using UnityEngine;

namespace Card
{
    public abstract class WeaponCardBaseSO : PassiveCardBaseSO
    {
        private const string WEAPON = "무기";

        [Range(0f, 1f)] public float weaponHardness = 1.0f;

        public WeaponCardBaseSO() : base(WEAPON) {}

        // public override CardController CreateCardController(CardHandler cardHandler)
        // {
        //     return new WeaponCardController(this, cardHandler);
        // }

    }
}