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

    private VisualElement _root;
    private VisualElement _cardContainer;
    private List<StoreCard> _cardList;
    private List<StoreCardEvent> _cardClickList;

    public bool IsTween { get; set; } = false;

    private Button _exitBtn;

    private StoreInfoBox _storeInfoBox;

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
        _cardList = new List<StoreCard>();
        _cardClickList = new List<StoreCardEvent>();
    }

    private void OnEnable()
    {
        _root = _uiDocument.rootVisualElement;
        _cardContainer = _root.Q<VisualElement>("Container");

        _exitBtn = _cardContainer.Q<Button>("ExitBtn");

        _storeInfoBox = new StoreInfoBox(_root.Q<VisualElement>("InfoBox"));


        _exitBtn.RegisterCallback<ClickEvent>(ExitContainer);

        _root.RegisterCallback<MouseMoveEvent>(InfoBoxMove);
        ResetCardList();
    }

    private void InfoBoxMove(MouseMoveEvent evt)
    {
        if (_storeInfoBox.Root == null) return;
        if (_root == null) return;

        Vector3 uiPos = RuntimePanelUtils.ScreenToPanel(_root.panel, Input.mousePosition);

        _storeInfoBox.Root.style.left = uiPos.x - _storeInfoBox.Root.layout.width;
        _storeInfoBox.Root.style.top = -uiPos.y + _root.layout.height - _storeInfoBox.Root.layout.height * 0.5f;
    }

    private void ExitContainer(ClickEvent evt)
    {
        StartCoroutine(RemoveContainerClass());
    }

    private void OnDisable()
    {
        _exitBtn.UnregisterCallback<ClickEvent>(ExitContainer);
    }

    private IEnumerator RemoveContainerClass()
    {
        IsTween = true;
        _cardContainer.RemoveFromClassList("on");
        yield return WaitForSeconds(_storeAnimationDuration);

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
            StoreCardEvent cardEvent = new(cardList[i], this, _storeManager, _storeInfoBox);

            _cardClickList.Add(cardEvent);
            cardList[i].RegisterCallback<ClickEvent>(cardEvent.SelectCard);
            cardList[i].RegisterCallback<MouseEnterEvent>(cardEvent.EnterEvent);
            cardList[i].RegisterCallback<MouseLeaveEvent>(cardEvent.LeaveEvent);
            _cardList.Add(card);
        }

        _root.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);

    }

    public void ShowStore()
    {
        if (_cardManager.IsOpen) return;
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

        for (int i = 0; i < _cardList.Count; ++i)
        {
            _cardList[i].AddToClassListAtRoot("on");
            yield return WaitForSeconds(_cardAnimationDuration);
        }
        IsTween = false;
    }
}