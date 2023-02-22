using System;
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
        private CardManager _cardManager;
        [SerializeField]
        private ChestCardManager _chestCardManager;

        [SerializeField]
        private List<TimeValue> _transitionDuration;

        private UIDocument _document;
        private VisualElement _cardContainer;

        private bool _isDisplayed = false;

        private bool _isTweening = false;

        private List<ChestCard> _cardList;
        private void Awake()
        {
            _document = GetComponent<UIDocument>();
            _cardList = new List<ChestCard>();
        }

        private void OnEnable()
        {
            VisualElement root = _document.rootVisualElement;
            _cardContainer = root.Q<VisualElement>("CardContainer");

            InitCard();
        }

        private void OnDisable() {
            _cardList.Clear();
        }

        private void InitCard()
        {
            for (int i = 0; i < _chestCardManager.CardCount; ++i)
            {
                AddCard();
            }
        }

        /// TODO: 카드 갯수가 늘어나면 AddCard 호출해주기
        /// <summary>
        /// 카드 갯수 늘려주는 함수
        /// </summary>
        public void AddCard()
        {
            VisualElement cardRoot = _card.Instantiate().Q<VisualElement>("CardTemplate");
            ChestCard card = new ChestCard(cardRoot);

            _cardList.Add(card);
            _cardContainer.Add(cardRoot);

            cardRoot.RegisterCallback<ClickEvent>(SelectCard);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && !_isDisplayed && !_isTweening)
            {
                _isDisplayed = true;
                ShowCard();
            }
        }

        public void ShowCard()
        {
            _cardContainer.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
            _cardContainer.style.opacity = new StyleFloat(1f);

            // TODO: 카드manger 에서 카드 가져와서 카드의 값들 넣어주기
            _cardList.ForEach(card => card.AddToClassList("on"));
        }

        private void SelectCard(ClickEvent evt)
        {
            if(_isTweening)return;
            
            Debug.Log("클릭됨");
            StartCoroutine(DisableUI());
        }

        private IEnumerator DisableUI()
        {
            _isTweening = true;
            _isDisplayed = false;

            _cardContainer.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
            _cardContainer.style.opacity = new StyleFloat(0f);

            yield return Yields.WaitForSeconds(0.5f);

            _cardList.ForEach(card => card.RemoveFromClassList("on"));

            _isTweening = false;
        }

        private IEnumerator DelayCoroutine(System.Action Callback)
        {
            yield return null;
            Callback?.Invoke();
        }
    }

}