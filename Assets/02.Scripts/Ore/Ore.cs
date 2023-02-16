using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ore : Entity, IDamageTaker
{
    [SerializeField]
    private float _oreGauge = 100;

    [SerializeField]
    private int _oreHp = 5;

    private SpriteRenderer _spriteRenderer;
    private float _currentOreGauge;
    private int _currentOreHp;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (_spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer is Null");
        }
    }

    private void Start()
    {
        _currentOreGauge = _oreGauge;
        _currentOreHp = _oreHp;

        //ChangeSprite();
    }

    public void TakeDamage(float damage)
    {
        _currentOreGauge -= damage;

        if (_currentOreGauge <= 0)
        {
            _currentOreHp--;

            // 원석 소환
            //OreManager.Instance.SpawnHoldOre(transform.position);
            if (_currentOreHp <= 0)
            {
                // TODO: 풀링
                gameObject.SetActive(false);
            }
            else
            {
                // 스프라이트 변경
                //ChangeSprite();

                _currentOreGauge = _oreGauge;
            }
        }
    }

    // private void ChangeSprite()
    // {
    //     _spriteRenderer.sprite = OreManager.Instance.OreSprites[_currentOreHp - 1];

    // }

    // public void SetHealth(Attack attack)
    // {
    // }

    // [SerializeField]
    // private float _oreGauge;
}
