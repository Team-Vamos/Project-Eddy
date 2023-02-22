using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Card
{


    [RequireComponent(typeof(UIDocument))]
    public class ChestUIDocument : MonoBehaviour
    {
        [SerializeField]
        private VisualTreeAsset _card;
        [SerializeField]
        private ChestCardManager _manager;

        private UIDocument _document;
        private VisualElement _cardContainer;
        private void Awake()
        {
            _document = GetComponent<UIDocument>();
        }

        private void OnEnable()
        {
            VisualElement root = _document.rootVisualElement;
            _cardContainer = root.Q<VisualElement>("CardContainer");


        }

        public void SpawnCard()
        {
            VisualElement cardRoot = _card.Instantiate().Q<VisualElement>("Card");

            //ChestCard card = new ChestCard(cardRoot, _manager, _manager.GetGrade());
        }
    }

}