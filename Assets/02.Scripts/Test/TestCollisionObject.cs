using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollisionObject : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;

    private void Update() {
        Move();
    }

    private void Move()
    {
        Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        transform.Translate(dir * _speed * Time.deltaTime);
    }
}