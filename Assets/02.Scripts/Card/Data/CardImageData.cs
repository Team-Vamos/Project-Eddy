using UnityEngine;

namespace Card
{
    [System.Serializable]
    public class CardImageData
    {
        public Vector2 offset;
        public Vector2 scale = Vector2.one;
        public Sprite icon;
    }
}