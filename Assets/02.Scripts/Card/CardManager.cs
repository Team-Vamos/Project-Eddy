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
        private CardGrade[] _cardGrades;
        private Dictionary<RankType, CardGrade> _cardGradeDict = new Dictionary<RankType, CardGrade>();

        public void AddSO(List<CardBaseSO> soList)
        {
            _cardSOList = soList;
        }
        
        
        private void Start() {
            InitCardGrade();
        }

        private void InitCardGrade()
        {
            for (int i = 0; i < _cardGrades.Length; ++i)
            {
                if(!_cardGradeDict.TryAdd(_cardGrades[i].rank, _cardGrades[i]))
                {
                    Debug.LogError("CardRankType is overlaped Check the Grades");
                }
            }
        }

        public CardGrade GetGrade(RankType type)
        {
            if(_cardGradeDict.TryGetValue(type, out CardGrade grade))
            {
                return grade;
            }
            throw new System.Exception("Type is Null in dict");
        }
    }
}