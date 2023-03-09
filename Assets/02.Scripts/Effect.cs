using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    private float effectDelay;
    private void Awake() {
        effectDelay = GetComponent<ParticleSystem>().main.duration;
        StartCoroutine("SpandDeley");
    }
    private IEnumerator SpandDeley()
    {
        yield return new WaitForSeconds(effectDelay);
        PoolManager.Destroy(gameObject);
    }
}
