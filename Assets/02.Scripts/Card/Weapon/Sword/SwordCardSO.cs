using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    [CreateAssetMenu(fileName ="Sword", menuName ="SO/Card/Weapon/Sword")]
    public class SwordCardSO : WeaponCardBaseSO
    {
        public override CardController CreateCardController(CardHandler cardHandler)
        {
            return new SwordCardController(this, cardHandler);
        }
    }
}
