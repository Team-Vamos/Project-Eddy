using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Card;

public class BabyBottle : MonoBehaviour
{
    private CardController _cardController;

    [SerializeField]
    private SpriteRenderer _iconImage;

    public void SetValue(CardController controller)
    {
        _cardController = controller;
        _iconImage.sprite = _cardController.CardBase.cardImage.icon;
        _iconImage.transform.localPosition = _cardController.CardBase.cardImage.offset;
        _iconImage.transform.localScale = _cardController.CardBase.cardImage.scale;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")) // TODO: 테스트용으로 ComPareTag해둠
        {
            CardHandler handler = other.transform.GetComponent<CardHandler>();
            handler.AddCard(_cardController);
            // TODO: 풀링
            gameObject.SetActive(false);
        }
    }
}