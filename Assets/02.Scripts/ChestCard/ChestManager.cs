using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Card;
using System;

public class ChestManager : MonoBehaviour
{
    public int ChestCount
    {
        get => _chestCount;
        set
        {
            _chestCount = value;
            ChestCountUpdateAction?.SafeInvoke<int>(_chestCount);
        }
    }

    public event Action<int> ChestCountUpdateAction;
    [SerializeField]
    private int _chestCount;

    [SerializeField]
    private DayWorker _dayWorker;

    //[Tooltip("한명당 어느정도로 추가 스텟을 줄 것인가")]
    [field: SerializeField]
    public float VoteMultipleValue { get; set; } = 0.1f;

    [field: SerializeField]
    public BabyBottleSpawner BottleSpawner { get; set; }

    // TODO: 나중에 애기의 인원수로 바꾸기
    [SerializeField]
    private int _votePersonMaxCount = 4;

    [SerializeField]
    private ChestUIDocument _uiDocument;

    public int VotePersonMaxCount => _votePersonMaxCount;
    public int CurrentVotePersonCount { get; private set; } = 0;

    [field: SerializeField]
    public bool IsVote { get; private set; } = true;

    private void Start() {
        ChestCountUpdateAction?.SafeInvoke<int>(_chestCount);
    }

    public void ResetVotePerson()
    {
        CurrentVotePersonCount = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _uiDocument.ShowCard();
        }
    }

    public void AddVotePerson()
    {
        CurrentVotePersonCount++;
    }

    public void UpdateIsVote()
    {
        IsVote = _dayWorker.DayState.Equals(DayState.Day);
    }
}