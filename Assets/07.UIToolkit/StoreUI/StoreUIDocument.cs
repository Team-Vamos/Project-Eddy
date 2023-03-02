using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Card;
using static Yields;
using System;

[RequireComponent(typeof(UIDocument))]
public class StoreUIDocument : MonoBehaviour
{
    [SerializeField]
    private CardManager _cardManager;

    [SerializeField]
    private StoreManager _storeManager;

    [SerializeField]
    private float _storeAnimationDuration = 0.5f;

    // [SerializeField]
    // private float _verticalDuration = 0.3f;
    // [SerializeField]
    // private float _horizontalDuration = 0.3f;
    [SerializeField]
    private float _cardAnimationDuration = 0.2f;

    private UIDocument _uiDocument;

    private VisualElement _cardContainer;
    private VisualElement _root;
    private List<StoreCard> _cardList;
    private List<StoreCardClick> _cardClickList;

    public bool IsTween { get; set; } = false;

    private void Awake() {
        _uiDocument = GetComponent<UIDocument>();
        _cardList = new List<StoreCard>();
        _cardClickList = new List<StoreCardClick>();
    }

    private void OnEnable() {
        _root = _uiDocument.rootVisualElement;
        _cardContainer = _root.Q<VisualElement>("Container");

        Button exitBtn = _cardContainer.Q<Button>("ExitBtn");

        exitBtn.RegisterCallback<ClickEvent>(ExitContainer);
        ResetCardList();
    }

    private void ExitContainer(ClickEvent evt)
    {
        StartCoroutine(RemoveContainerClass());
    }

    private IEnumerator RemoveContainerClass()
    {
        IsTween = true;
        _cardContainer.RemoveFromClassList("on");
        yield return WaitForSeconds(_storeAnimationDuration);
        // _cardContainer.RemoveFromClassList("horizontal-on");
        // yield return WaitForSeconds(_horizontalDuration);
        // _cardContainer.RemoveFromClassList("vertical-on");
        // yield return WaitForSeconds(_verticalDuration);

        for (int i = 0; i < _cardList.Count; ++i)
        {
            _cardList[i].RemoveFromClassListAtRoot("on");
        }
        
        _root.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
        IsTween = false;
        _cardManager.IsOpen = false;
    }

    private void ResetCardList()
    {
        _cardList.Clear();

        List<VisualElement> cardList = _cardContainer.Query<VisualElement>(className: "card").ToList();
        for (int i = 0; i < cardList.Count; ++i)
        {
            StoreCard card = new(cardList[i], _cardManager);
            StoreCardClick cardClick = new(cardList[i], this, _storeManager);

            _cardClickList.Add(cardClick);
            cardList[i].RegisterCallback<ClickEvent>(cardClick.SelectCard);
            _cardList.Add(card);
        }

        _root.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);

    }

    public void ShowStore()
    {
        if(_cardManager.IsOpen)return;
        _cardManager.IsOpen = true;

        for (int i = 0; i < _cardList.Count; ++i)
        {
            CardBaseSO so = _cardManager.GetRandomCardSO();

            _cardList[i].SetCardSO(so);
            _cardClickList[i].SetCardSO(so);
            int price = _storeManager.GetPrice(so.rank);
            _cardClickList[i].Price = price;
            _cardList[i].UpdateInfo(price);
        }

        _root.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);

        StartCoroutine(AddClassToCardContainer());
    }

    private IEnumerator AddClassToCardContainer()
    {
        IsTween = true;
        _cardContainer.AddToClassList("on");
        yield return WaitForSeconds(_storeAnimationDuration);
        // _cardContainer.AddToClassList("vertical-on");
        // yield return WaitForSeconds(_verticalDuration);
        // _cardContainer.AddToClassList("horizontal-on");
        // yield return WaitForSeconds(_horizontalDuration);
        for (int i = 0; i < _cardList.Count; ++i)
        {
            _cardList[i].AddToClassListAtRoot("on");
            yield return WaitForSeconds(_cardAnimationDuration);
        }
        IsTween = false;
    }
}