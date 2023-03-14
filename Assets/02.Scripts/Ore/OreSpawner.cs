using System;
using System.Collections.Generic;
using _02.Scripts.Ore;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

// TODO: OreSpawner를 UI부분을 삭제, OreSpriteManagerClass를 가지고 있기, OreSpawnerClass를 따로 가지고 있기 OreDebuggerClass 가지고 있기, Class이름을 OreSpawner가 아닌 OreManager로 바꾸기
public class OreSpawner : MonoBehaviour
{
    [SerializeField]
    private Bound[] _oreSpawnBounds;

    [SerializeField]
    private Ore _orePrefab;

    [SerializeField]
    private OreSpriteManager _oreSpriteManager;
    [SerializeField]
    private HoldOreSpawner _holdOreOreSpawner;

    [SerializeField]
    private float _oreSpawnDuration = 0.7f;
    [SerializeField]
    private Vector3 _oreSpawnScale = Vector3.one;
    [SerializeField]
    private int _oreSpawnCount = 3;

    private List<Ore> _oreList;

    private void Awake()
    {
        _oreList = new();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            SpawnOre();
        }

        if(Input.GetKeyDown(KeyCode.J))
        {
            _oreList.ForEach(ore => ore.TakeDamage(30f));
        }
        
        if(Input.GetKeyDown(KeyCode.L))
        {
            DestroyAllOre();
        }
    }

    public void SpawnOre()
    {
        Bound bound = Define.GetRandomBound(_oreSpawnBounds);
        for (int i = 0; i < _oreSpawnCount; ++i)
        {
            Vector2 position = bound.GetRandomPos();
            Ore ore = NetworkPoolManager.Instantiate(_orePrefab.gameObject, position, Quaternion.identity).GetComponent<Ore>();
            
            ore.Init(_holdOreOreSpawner,_oreSpriteManager);

            ore.transform.localScale = Vector3.zero;

            ore.transform.DOScale(_oreSpawnScale, _oreSpawnDuration).SetEase(Ease.InOutElastic);

            _oreList.Add(ore);
        }
    }

    public void DestroyAllOre()
    {
        foreach (Ore ore in _oreList)
        {
            ore.transform.DOScale(Vector3.zero, _oreSpawnDuration).OnComplete(() =>
            {
                NetworkPoolManager.Destroy(ore.gameObject);
            });
        }
    }
    
#if UNITY_EDITOR
    
    [SerializeField]
    private bool _isDebug = false;
    
    [SerializeField]
    private Color _debugColor = Color.cyan;
    private void OnDrawGizmos()
    {
        if(_isDebug)
        {
            Gizmos.color = _debugColor;
            foreach (var bound in _oreSpawnBounds)
            {
                Gizmos.DrawWireCube(bound.Center, bound.Size);
            }
        }
    }
    
#endif
}
