using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    [CreateAssetMenu(fileName = "CardGrade", menuName = "SO/Card/CardGradeSO", order = 1)]
    public class CardGradeSO : ScriptableObject
    {
        public Color color  = new Color(0f,0f,0f,1f);
        public RankType rank;
        public int price;
    }

}