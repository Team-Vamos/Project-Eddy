using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    public class CardManager : MonoBehaviour
    {
        [SerializeField]
        private List<CardBaseSO> _cardSOList;

        public void AddSO(List<CardBaseSO> soList)
        {
            _cardSOList = soList;
        }
    }
}