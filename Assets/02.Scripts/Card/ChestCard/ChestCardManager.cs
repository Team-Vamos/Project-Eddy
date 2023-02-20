using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    public class ChestCardManager : MonoBehaviour
    {
        [SerializeField]
        private CardGrade[] _cardGrades;

        [SerializeField]
        private int _cardCount;
        public int CardCount => _cardCount;

        private Dictionary<RankType, CardGrade> _cardGradeDict = new Dictionary<RankType, CardGrade>();

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