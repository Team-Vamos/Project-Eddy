public abstract class StatModifier
{
    public object Source { get; }
    public StatType StatType { get; }
    public int Order { get; }

    protected StatModifier(object source, StatType statType, int order = 0)
    {
        Source = source;
        StatType = statType;
        Order = order;
    }

    public abstract void CalculateStat(ref UpgradableStat stat);
}