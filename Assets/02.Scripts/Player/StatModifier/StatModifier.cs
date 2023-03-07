public abstract class StatModifier
{
    public object Source { get; }
    public StatType StatType { get; }
    public StatOrderType Order { get; }

    protected StatModifier(object source, StatType statType, StatOrderType order = StatOrderType.Normal_Additive)
    {
        Source = source;
        StatType = statType;
        Order = order;
    }

    public abstract void CalculateStat(ref UpgradableStat stat);
}