public class StatModifierMultiplicative : StatModifier
{
    private readonly float _value;
    
    public StatModifierMultiplicative(object source, float value, StatType statType, StatOrderType order = StatOrderType.Normal_Additive) : base(source, statType, order)
    {
        _value = value;
    }

    public override void CalculateStat(ref UpgradableStat stat)
    {
        stat.Value *= _value;
    }
}