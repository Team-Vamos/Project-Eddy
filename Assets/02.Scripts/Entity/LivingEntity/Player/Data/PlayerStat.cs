using System;

[Serializable]
public class PlayerStat
{
    public float atk;
    public float spd;
    public float atkSpd;
    public float hp;
    public float armor;
    public float mine;

    public int expToNextLevel;
    
    public float reviveTime;

    public void Add(StatType statType, float value)
    {
        var _ = statType switch
        {
            StatType.Atk => atk += value,
            StatType.Spd => spd += value,
            StatType.AtkSpd => atkSpd += value,
            StatType.Hp => hp += value,
            StatType.Armor => armor += value,
            StatType.Mine => mine += value,
            _ => throw new ArgumentOutOfRangeException(nameof(statType), statType, null)
        };
    }
    
    public void Multiply(StatType statType, float value)
    {
        var _ = statType switch
        {
            StatType.Atk => atk *= value,
            StatType.Spd => spd *= value,
            StatType.AtkSpd => atkSpd *= value,
            StatType.Hp => hp *= value,
            StatType.Armor => armor *= value,
            StatType.Mine => mine *= value,
            _ => throw new ArgumentOutOfRangeException(nameof(statType), statType, null)
        };
    }
}