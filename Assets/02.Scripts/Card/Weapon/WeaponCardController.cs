using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    public abstract class WeaponCardController : PassiveCardController
    {
        // TODO: 플레이어 Attack을 가지고 있어야 함

        protected PlayerAttack _playerAttack;
        private readonly WeaponCardBaseSO _cardBase;
        // //private PlayerAttack _playerAttack;
        public WeaponCardController(WeaponCardBaseSO cardBase, CardHandler cardHandler) : base(cardBase, cardHandler)
        {
            _cardBase = cardBase;
        }

        public override void ApplyCard()
        {
            base.ApplyCard();
            //_playerAttack = CardHandler.GetComponent<PlayerAttack>();
            // TODO 무기 프리팹 장착
        }

        public override void RemoveCard()
        {
            base.RemoveCard();
            //_playerAttack.Reset();
        }

        public abstract void Attack(/*UserAttack user*/);

        public void SetUserAttack(PlayerAttack playerAttack)
        {
            _playerAttack = playerAttack;
        }
    }
}