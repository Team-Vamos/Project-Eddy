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
        public override void Attack()
        {
            IDamageTaker[] iDamageTaker =
                PlayerAttack.GetArcTargetsAll
                (_playerAttack.transform.position,
                 _playerAttack.AttackArc,
                 _playerAttack.AttackRange,
                 _playerAttack.AttackAngle,
                 _playerAttack.EnemyLayerMask);
            foreach (var damageTaker in iDamageTaker)
            {
                damageTaker.TakeDamage(_cardHandler.PlayerStat.Stat.Atk.Value);
                Vector3 EffectDir = _playerAttack.GetHitDir((damageTaker as MonoBehaviour).transform, _cardBase.weaponHardness);
                EffectManager.Instance.CreateEffect((damageTaker as MonoBehaviour).transform.position, EffectDir);
                EffectManager.Instance.CreateHit_1Effect((damageTaker as MonoBehaviour).transform.position, EffectDir);
                EffectManager.Instance.CreateHit_2Effect((damageTaker as MonoBehaviour).transform.position, EffectDir);
            }
            //if (iDamageTaker.Length > 0)
                //CinemachineShake.Instance.GoShake(1.3f, 0.3f);
        }
    }

}