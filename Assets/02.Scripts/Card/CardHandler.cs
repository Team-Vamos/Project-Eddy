using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    public class CardHandler : MonoBehaviour
    {
        //[NonSerialized] public PlayerStat PlayerStat;

        public StatController StatController { get; set; }
        public Transform WeaponHolder { get; set; }
        private readonly List<CardController> _cards = new();

        // private void Awake()
        // {
        //     //PlayerStat = GetComponent<PlayerStat>();
        // }

        public void AddCard(CardBaseSO cardBase)
        {
            var cardController = cardBase.CreateCardController(this);
            cardController.ApplyCard();
            _cards.Add(cardController);
        }


        public void AddCard(CardController controller)
        {
            controller.SetCardHandler(this);
            controller.ApplyCard();
            _cards.Add(controller);
        }

        public void RemoveCard(CardBaseSO cardBase)
        {
            var cardController = _cards.Find(c => c.CardBase == cardBase);
            cardController.RemoveCard();
            _cards.Remove(cardController);
        }
    }
}