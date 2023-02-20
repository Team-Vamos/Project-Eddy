using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    [CreateAssetMenu(fileName = "CardGrade", menuName = "SO/Card/CardGradeSO", order = 1)]
    public class CardGrade : ScriptableObject
    {
        public Color color;
        public Color hoverColor;
        public RankType rank;
    }

}