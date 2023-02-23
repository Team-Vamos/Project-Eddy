using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    public class ChestCardManager : MonoBehaviour
    {
        [SerializeField]
        private int _cardCount;
        public int CardCount => _cardCount;

        // TODO: DayWorker 연동하기
        [SerializeField]
        private DayWorker _dayWorker;

        [Header("한명당 어느정도로 추가 스텟을 줄 것인가")]
        [SerializeField]
        private float _voteMultipleValue = 0.1f;
        public float VoteMultipleValue => _voteMultipleValue;

        [SerializeField]
        private BabyBottleSpawner _bottleSpawner;

        public BabyBottleSpawner BottleSpawner => _bottleSpawner;

        // TODO: 나중에 애기의 인원수로 바꾸기
        [SerializeField]
        private int _votePersonMaxCount = 4;

        public int VotePersonMaxCount => _votePersonMaxCount;
        public int CurrentVotePersonCount { get; private set; } = 0;

        [field:SerializeField]
        public bool IsVote { get; private set; } = true;

        public void ResetVotePerson()
        {
            CurrentVotePersonCount = 0;
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

}