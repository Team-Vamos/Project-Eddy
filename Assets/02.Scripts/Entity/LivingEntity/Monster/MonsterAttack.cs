using System;
using UnityEngine;

public abstract class MonsterAttack : MonoBehaviour
{
    public event Action OnInit;
    protected Monster Monster { get; private set; }

    public void Init(Monster monster)
    {
        Monster = monster;
        OnInit?.SafeInvoke();
    }

    public abstract void Attack(Entity target);
}