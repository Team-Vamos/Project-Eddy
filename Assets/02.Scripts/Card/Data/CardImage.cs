using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    [System.Serializable]
    public class CardImage
    {
        public Sprite icon;
        public Vector2 offset = Vector2.zero;
        public Vector2 scale = Vector2.one;
    }
}