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
        private List<CardGradeSO> _cardGrades;
        private Dictionary<RankType, CardGradeSO> _cardGradeDict = new Dictionary<RankType, CardGradeSO>();

        private List<CardBaseSO> _tempCardList;
        public void SetSOList(List<CardBaseSO> soList)
        {
            _cardSOList = soList;
        }

        public void SetCardGradeSOList(List<CardGradeSO> gradeList)
        {
            _cardGrades = gradeList;
        }


        private void Start()
        {
            InitCardGrade();
            ResetCardList();
        }

        private void InitCardGrade()
        {
            for (int i = 0; i < _cardGrades.Count; ++i)
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

        public CardGradeSO GetGrade(RankType type)
        {
            if (_cardGradeDict.TryGetValue(type, out CardGradeSO grade))
            {
                return grade;
            }
            throw new System.Exception("Type is Null in dict");
        }
    }
}