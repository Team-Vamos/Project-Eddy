using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    public class CardManager : MonoBehaviour
    {
        [SerializeField]
        private List<CardBaseSO> _cardSOList;
        [SerializeField]
        private CardGrade[] _cardGrades = new CardGrade[(int)RankType.LENGTH];
        private Dictionary<RankType, CardGrade> _cardGradeDict = new Dictionary<RankType, CardGrade>();

        private List<CardBaseSO> _tempCardList;
        public void SetSOList(List<CardBaseSO> soList)
        {
            _cardSOList = soList;
        }


        private void Start()
        {
            InitCardGrade();
            ResetCardList();
        }

        private void InitCardGrade()
        {
            for (int i = 0; i < _cardGrades.Length; ++i)
            {
                if (!_cardGradeDict.TryAdd(_cardGrades[i].rank, _cardGrades[i]))
                {
                    Debug.LogError("CardRankType is overlaped Check the Grades");
                }
            }
        }

        public CardBaseSO GetRandomCardSO()
        {
            int random = Random.Range(0, _tempCardList.Count);

            CardBaseSO so = _tempCardList[random];

            _tempCardList.RemoveAt(random);
            return so;
        }

        public void ResetCardList()
        {
            _tempCardList = new List<CardBaseSO>(_cardSOList);
        }

        public CardGrade GetGrade(RankType type)
        {
            if (_cardGradeDict.TryGetValue(type, out CardGrade grade))
            {
                return grade;
            }
            throw new System.Exception("Type is Null in dict");
        }
    }
}