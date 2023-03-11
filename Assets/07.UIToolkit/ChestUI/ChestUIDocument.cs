using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Card;

public class ChestUIDocument : UIDocumentMono
{
    [SerializeField]
    private VisualTreeAsset _card;
    [SerializeField]
    private VisualTreeAsset _vote;

    [SerializeField]
    private CardManager _cardManager;
    [SerializeField]
    private ChestManager _chestCardManager;

    private UIDocument _document;
    private VisualElement _cardContainer;

    public bool IsTweening { get; private set; } = false;

    private List<ChestCard> _cardList;
    private List<ChestCardClick> _cardClickList;
    protected override void Awake()
    {
        base.Awake();
        _cardList = new List<ChestCard>();
        _cardClickList = new List<ChestCardClick>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _cardContainer = _root.Q<VisualElement>("CardContainer");
        InitCard();
    }

    private void OnDisable()
    {
        _cardList.Clear();
        _cardContainer.Clear();
        _cardClickList.Clear();
    }

    private void InitCard()
    {
        for (int i = 0; i < _chestCardManager.ChestCount; ++i)
        {
            AddCard();
        }
        DisappearRootVisualElement();

    }

    /// TODO: 카드 갯수가 늘어나면 AddCard 호출해주기
    /// <summary>
    /// 카드 갯수 늘려주는 함수
    /// </summary>
    public void AddCard()
    {
        VisualElement cardRoot = _card.Instantiate().Q<VisualElement>("CardTemplate");
        ChestCard card = new ChestCard(cardRoot, _chestCardManager, _vote);
        ChestCardClick click = new ChestCardClick(this, _chestCardManager, card);

        _cardList.Add(card);
        _cardClickList.Add(click);
        _cardContainer.Add(cardRoot);

        cardRoot.RegisterCallback<ClickEvent>(click.SelectCard);
    }

    public void ShowCard()
    {

        if (_cardManager.IsOpen || IsTweening) return;


        _chestCardManager.UpdateIsVote();

        if (_chestCardManager.IsVote)
            Time.timeScale = 0f;

        _chestCardManager.ResetVotePerson();

        _cardManager.IsOpen = true;

        _cardContainer.AddToClassList("on");
        AppearRootVisualElement();

        // _cardContainer.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
        // _cardContainer.style.opacity = new StyleFloat(1f);

        for (int i = 0; i < _cardList.Count; ++i)
        {
            _cardList[i].AddToClassList("on");
            CardBaseSO so = _cardManager.GetRandomCardSO();
            _cardList[i].SetCardSO(so);
            _cardList[i].UpdateInfo(_cardManager.GetGrade(so.rank));

            _cardClickList[i].SetCardSO(so);
        }
    }

    public void DisableContainer()
    {
        StartCoroutine(DisableUI());
    }

    private IEnumerator DisableUI()
    {
        IsTweening = true;
        _cardManager.IsOpen = false;

        _cardContainer.RemoveFromClassList("on");

        yield return Yields.WaitForSeconds(0.5f);
        IsTweening = false;

        _cardList.ForEach(card =>
        {
            card.RemoveFromClassList("on");
            card.DisableVotePerson();
        });

        DisappearRootVisualElement();

        _cardManager.ResetCardList();
    }

}