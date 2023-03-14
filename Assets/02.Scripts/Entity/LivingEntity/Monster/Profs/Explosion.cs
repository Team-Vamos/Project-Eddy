using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float _time;

    private void Awake()
    {
        _time = 0;
    }

    private void Update()
    {
        _time += Time.deltaTime;
        if (_time > 2)
            NetworkPoolManager.Destroy(gameObject);
    }
}