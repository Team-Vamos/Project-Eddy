using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    public class SwordCardController : WeaponCardController
    {
        private readonly SwordCardSO _cardBase;

        public SwordCardController(SwordCardSO cardBase, CardHandler cardHandler) : base(cardBase, cardHandler)
        {
            _cardBase = cardBase;
        }
        
        // TODO: 플레이어 검 공격
        public override void Attack(/*UserAttack user*/)
        {
            // IDamageTaker[] iDamageTaker = 
            //     UserAttack.GetArcTargetsAll
            //     (user.transform.position,
            //      user.AttackArc, 
            //      user.AttackRange, 
            //      user.AttackAngle, 
            //      user.EnemyLayerMask);
            // foreach (var damageTaker in iDamageTaker)
            // {
            //     damageTaker.TakeDamage(
            //         new Attack(user.AttackDamage, null));
            //     Vector3 EffectDir = userAttack.GetHitDir((damageTaker as MonoBehaviour).transform, _cardBase.weaponHardness);
            //     //EffectManager.Instance.CreateEffect((damageTaker as MonoBehaviour).transform.position, EffectDir);
            //     if(damageTaker is Enemy){
            //         EffectManager.Instance.CreateHit_1Effect((damageTaker as MonoBehaviour).transform.position, EffectDir);
            //         EffectManager.Instance.CreateHit_2Effect((damageTaker as MonoBehaviour).transform.position, EffectDir);
            //     }
            // }
            // if(iDamageTaker.Length > 0)
            //     CinemachineShake.Instance.GoShake(1.3f, 0.3f);
        }
    }

}