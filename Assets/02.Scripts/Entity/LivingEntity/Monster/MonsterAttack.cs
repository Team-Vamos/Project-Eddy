using UnityEngine;

public abstract class MonsterAttack : MonoBehaviour
{
    protected Monster Monster { get; private set; }

    public void Init(Monster monster)
    {
        Monster = monster;
    }

    public abstract void Attack(Entity target);
}