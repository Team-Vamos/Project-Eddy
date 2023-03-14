using UnityEngine;
using DG.Tweening;

public class HoldOreSpawner : MonoBehaviour
{
    [SerializeField]
    private HoldOre _holdOrePrefab;

    [SerializeField]
    private float _holdOreSpawnDuration = 0.7f;
    [SerializeField]
    private float _holdOreSpawnInnerRadius = 1f;
    [SerializeField]
    private float _holdOreSpawnOuterRadius = 1.5f;

    [SerializeField]
    private int _oreMin;
    [SerializeField]
    private int _oreMax;
    
    [SerializeField]
    private Vector3 _holdOreSpawnScale = Vector3.one;
    
    public void SpawnHoldOre(Vector2 pos)
    {
        HoldOre holdOre = Instantiate(_holdOrePrefab);

        Vector2 dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        float distance = _holdOreSpawnInnerRadius + Random.Range(0f, _holdOreSpawnOuterRadius);
        
        holdOre.transform.position =  pos+ dir * distance;

        holdOre.gameObject.SetActive(true);

        holdOre.Price = Random.Range(_oreMin, _oreMax);

        holdOre.transform.DOScale(_holdOreSpawnScale, _holdOreSpawnDuration);
    }
}
