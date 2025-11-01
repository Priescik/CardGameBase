using System;

public enum SideSelect
{
    A, B,
    All, None,
    Friendly, Enemy
}

public static class SideSelector
{
    public static Side[] Select(SideSelect selection, Side sourceSide) => selection switch
    {
        SideSelect.A => new[] { Side.A },
        SideSelect.B => new[] { Side.B },
        SideSelect.All => new[] { Side.A, Side.B },
        SideSelect.None => Array.Empty<Side>(),
        SideSelect.Friendly => new[] { sourceSide },
        SideSelect.Enemy => new[] { sourceSide==Side.A ? Side.B : Side.A },
        _ => Array.Empty<Side>()
    };
}