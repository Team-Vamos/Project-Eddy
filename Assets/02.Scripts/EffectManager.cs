using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoSingleton<EffectManager>
{
    [SerializeField] private GameObject EffectPrefab;
    [SerializeField] private GameObject[] hit_1;
    [SerializeField] private GameObject[] hit_2;
    private void Awake() {
        hit_1 = Resources.LoadAll<GameObject>("Prefab/hit_1");
        hit_2 = Resources.LoadAll<GameObject>("Prefab/hit_2");
        EffectPrefab = Resources.Load<GameObject>("Prefab/hit1");
    }
    public Effect CreateHit_1Effect(Vector3 position, Vector3 dir)
    {
        GameObject effect = PoolManager.Instantiate(hit_1[Random.Range(0, hit_1.Length)]);
        effect.transform.position = position;
        effect.transform.rotation = Quaternion.LookRotation(dir) * Quaternion.AngleAxis(-90f, effect.transform.up);
        Effect _effect = effect.GetComponent<Effect>();
        _effect.StartCoroutine("SpandDeley");
        return _effect;
    }
    public Effect CreateHit_2Effect(Vector3 position, Vector3 dir)
    {
        GameObject effect = PoolManager.Instantiate(hit_2[Random.Range(0, hit_2.Length)]);
        effect.transform.position = position;
        effect.transform.rotation = Quaternion.LookRotation(dir) * Quaternion.AngleAxis(-90f, effect.transform.up);
        Effect _effect = effect.GetComponent<Effect>();
        _effect.StartCoroutine("SpandDeley");
        return _effect;
    }
    public Effect CreateEffect(Vector3 position, Vector3 dir)
    {
        GameObject effect = PoolManager.Instantiate(EffectPrefab);
        effect.transform.position = position;
        effect.transform.rotation = Quaternion.LookRotation(dir);
        Effect _effect = effect.GetComponent<Effect>();
        _effect.StartCoroutine("SpandDeley");
        return _effect;
    }

}
