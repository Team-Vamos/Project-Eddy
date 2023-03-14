using _02.Scripts.Ore;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

[DisallowMultipleComponent]
// TODO: Monobehaviour -> Entity
public class Ore : StructureEntity
{
    [SerializeField]
    private float _oregauge = 100f;

    [SerializeField]
    private int _oreHp = 5;

    private OreSpriteManager _oreSpriteManager;
    private HoldOreSpawner _holdOreSpawner;

    private int _currentOreSpriteIndex;

    private NavMeshObstacle _navMeshObstacle;
    
    [SerializeField]
    private SpriteRenderer _oreSprite;

    [SerializeField]
    private SpriteRenderer _oreBreakSprite;

    private SpriteMask _spriteMask;
    
    protected Ore() : base(100) {}
    protected Ore(float health) : base(health) {}

    private int _currentHp;

    private bool _initialize = false;
    private void Awake()
    {
        _navMeshObstacle = GetComponentInChildren<NavMeshObstacle>();
        _spriteMask = GetComponentInChildren<SpriteMask>();
        Health = _oregauge;
        _currentHp = _oreHp;
    }

    public void Init(HoldOreSpawner holdOreSpawner, OreSpriteManager oreSpriteManager)
    {
        if(_initialize) return;
        _initialize = true;
        _oreSpriteManager = oreSpriteManager;
        _holdOreSpawner = holdOreSpawner;
    }
    

    protected override void OnEnable()
    {
        base.OnEnable();
        _navMeshObstacle.enabled = true;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _navMeshObstacle.enabled = false;
    }

    private void Start()
    {
        // TODO: OnDestroyed에 사라지는 것 넣어두기
        OnDestroyed += DestroyObject;        
    }
    
    private void DestroyObject()
    {
        NetworkPoolManager.Destroy(gameObject);
    }

    public override void TakeDamage(float damage)
    {
        Health -= damage;

        Debug.Log(Health);
        Debug.Log(_oregauge * _oreSpriteManager.OreOreBreakSprites[_currentOreSpriteIndex].breakDamagePercent / 100);

        if(Health <= _oregauge * _oreSpriteManager.OreOreBreakSprites[_currentOreSpriteIndex].breakDamagePercent / 100 && Health > 0)
        {
            _currentOreSpriteIndex++;
            ChangeSpriteByHealth();
        }
        else if(Health <= 0)
        {
            _currentOreSpriteIndex = 0;
            
            Health = _oregauge;
            
            _currentHp--;
            
            _holdOreSpawner.SpawnHoldOre(transform.position);
        }
        
        OnDamageTaken?.SafeInvoke(damage);
        if(_currentHp <= 0)
            OnDestroyed?.SafeInvoke();
        else
        {
            _oreSprite.sprite = _oreSpriteManager.OreSprites[^_currentHp];
            _spriteMask.sprite = _oreSprite.sprite;
        }
    }

    private void ChangeSpriteByHealth()
    {
        _oreBreakSprite.sprite = _oreSpriteManager.OreOreBreakSprites[_currentOreSpriteIndex].breakSprite;
    }
}
 