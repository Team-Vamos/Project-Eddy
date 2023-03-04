using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Card;

public class ChestCardClick
{
    private CardBaseSO _so;
    private ChestUIDocument _document { get; init; }

    private ChestManager _manager { get; init; }

    private float _voteMultipleValue;

    private ChestCard _card;

    public ChestCardClick(ChestUIDocument document, ChestManager manager, ChestCard card)
    {
        _document = document;
        _manager = manager;
        _card = card;
    }

    public void SetCardSO(CardBaseSO so)
    {
        _so = so;
    }

    public void SelectCard(ClickEvent evt)
    {
        if (_document.IsTweening) return;

        bool isDaddyTurn = _manager.CurrentVotePersonCount >= _manager.VotePersonMaxCount;

        CardController controller = _so.CreateCardController(null);

        if (isDaddyTurn)
        {
            controller.ApplyMultipleValue(_voteMultipleValue);

            _manager.ResetVotePerson();
            Time.timeScale = 1f;
        }
        else if (_manager.IsVote)
        {
            _card.AddVotePerson();

            _manager.AddVotePerson();
            _voteMultipleValue += _manager.VoteMultipleValue;
            return;
        }

        _manager.BottleSpawner.SpawnBabyBottle(controller);
        _document.DisableContainer();
    }
}
