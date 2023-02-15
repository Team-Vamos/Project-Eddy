using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Entity : MonoBehaviour
{
    protected virtual void OnEnable()
    {
        this.Register();
    }
    
    protected virtual void OnDisable()
    {
        this.Unregister();
    }
}