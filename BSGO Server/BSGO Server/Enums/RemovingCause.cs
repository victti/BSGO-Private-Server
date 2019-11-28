namespace BSGO_Server
{
    internal enum RemovingCause : byte
    {
        Disconnection = 1,
        Death,
        JumpOut,
        TTL,
        Dock,
        Hit,
        JustRemoved,
        Collected
    }
}
