namespace BSGO_Server
{
    internal enum ShipAbilitySide : byte
    {
        Self = 1,
        Any = 2,
        Neutral = 4,
        Friend = 8,
        Enemy = 0x10
    }
}
