using System;

public class Player : LivingEntity, IExpTaker
{
    private PlayerStatStorage _statStorage;

    private Player() : base(EntityType.Player)
    {
    }

    private void Awake()
    {
        _statStorage = GetComponent<PlayerStatStorage>();
    }

    public void TakeExp(int exp)
    {
        _statStorage.AddExp(exp);
    }
}