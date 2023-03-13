using System;
using UnityEngine;
using UnityEngine.Events;

public class PoolObject : MonoBehaviour
{
    public GameObject prefab;
    public UnityEvent onInit = new();
    public UnityEvent onReturn = new();
}