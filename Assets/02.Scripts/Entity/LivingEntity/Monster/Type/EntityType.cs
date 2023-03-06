using System;

[Flags]
public enum EntityType
{
    Structure = 1 << 0,
    Daddy = 1 << 1,
    Eggy = 1 << 2,
    Monster = 1 << 3,
    Player = Daddy | Eggy,
    All = Structure | Monster | Player,
}