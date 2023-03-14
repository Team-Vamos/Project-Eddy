using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _02.Scripts.Ore
{

    public class OreSpriteManager : MonoBehaviour
    {
        [SerializeField]
        private List<Sprite> _oreSprites;

        public IReadOnlyList<Sprite> OreSprites => _oreSprites;

        [Header("오름차순 정렬 함수 에디터에서 꼭 실행시켜주기")]
        [SerializeField]
        private List<OreBreakSprite> _oreBreakSprites = new List<OreBreakSprite>(5);

        public IReadOnlyList<OreBreakSprite> OreOreBreakSprites => _oreBreakSprites;

        [ContextMenu("OrderedOreBreakSprites")]
        private void OrderOreBreakSprites()
        {
            _oreBreakSprites = _oreBreakSprites.OrderByDescending(sprite => sprite.breakDamagePercent).ToList();
        }
    }
}